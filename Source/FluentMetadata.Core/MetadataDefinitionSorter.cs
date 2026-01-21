using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace FluentMetadata
{
    class MetadataDefinitionSorter
    {
        readonly IDictionary<Type, ICollection<Type>> dependencies = new Dictionary<Type, ICollection<Type>>();
        readonly List<MetadataDefinition> metadataDefinitions = new List<MetadataDefinition>();
        bool hasBuiltGenericDefinitions;

        internal MetadataDefinitionSorter() { }

        internal void AddMetadataDefinitionsToSort(IEnumerable<Type> metadataDefinitions)
        {
            this.metadataDefinitions.AddRange(metadataDefinitions
                .Select(type => new MetadataDefinition(type)));
            hasBuiltGenericDefinitions = false;
        }

        internal void Register(Type dependencyModel, Type dependerModel)
        {
            Type dependency = metadataDefinitions
                    .SingleOrDefault(mdd => mdd.Model == dependencyModel)
                    .Metadata,
                depender = metadataDefinitions
                    .Single(mdd => mdd.Model == dependerModel)
                    .Metadata;

            if (dependency == null)
            {
                throw new NoMetadataDefinedException(dependencyModel, depender);
            }

            if (dependencies.ContainsKey(depender))
            {
                if (!dependencies[depender].Contains(dependency))
                {
                    dependencies[depender].Add(dependency);
                }
            }
            else
            {
                dependencies[depender] = new List<Type> { dependency };
            }
        }

        List<Type> GetIncorrectlyBuiltDefinitions(List<Type> alreadyBuiltDefinitions)
        {
            var metadataDefinitionDependencies = dependencies
                .Select(dependency => new MetadataDefinitionDependency
                {
                    Depender = dependency.Key,
                    Dependencies = dependency.Value
                })
                .ToArray();

            var incorrectlyBuiltDefinitions = metadataDefinitionDependencies
                .Where(mddd => !mddd.IsCorrectlyBuilt(alreadyBuiltDefinitions));

            return incorrectlyBuiltDefinitions
                .Concat(incorrectlyBuiltDefinitions
                    .SelectMany(mddd => mddd.GetDependentDefinitions(metadataDefinitionDependencies))
                    .Distinct())
                .OrderBy(mddd => mddd, new MetadataDefinitionDependencyComparer())
                .Select(mddd => mddd.Depender)
                .ToList();
        }

        internal List<Type> GetNextUnbuiltDefinitions(List<Type> alreadyBuiltDefinitions)
        {
            if (!hasBuiltGenericDefinitions)
            {
                hasBuiltGenericDefinitions = true;

                var genericDefinitions = metadataDefinitions
                    .Where(mdd => mdd.IsGeneric)
                    .Select(mdd => mdd.Metadata)
                    .Except(alreadyBuiltDefinitions)
                    .ToList();
                if (genericDefinitions.Count > 0)
                {
                    return genericDefinitions;
                }
            }

            var incorrectlyBuiltDefinitions = GetIncorrectlyBuiltDefinitions(alreadyBuiltDefinitions);
            return incorrectlyBuiltDefinitions.Count > 0 ?
                incorrectlyBuiltDefinitions :
                metadataDefinitions
                    .Select(mdd => mdd.Metadata)
                    .Except(alreadyBuiltDefinitions)
                    .ToList();
        }

        internal void Clear()
        {
            dependencies.Clear();
            metadataDefinitions.Clear();
        }

        struct MetadataDefinition
        {
            internal readonly Type Model;
            internal readonly Type Metadata;

            internal bool IsGeneric
            {
                get
                {
                    return Model.FullName == null;
                }
            }

            internal MetadataDefinition(Type type)
            {
                Metadata = type;
                Model = GetGenericClassMetadataParameter(type);
            }

            static Type GetGenericClassMetadataParameter(Type type)
            {
                if (type.IsGenericType)
                {
                    return type.GetGenericArguments()[0];
                }
                else
                {
                    return GetGenericClassMetadataParameter(type.BaseType);
                }
            }
        }

        internal class MetadataDefinitionDependency
        {
            internal Type Depender;
            internal ICollection<Type> Dependencies;

            internal bool IsCorrectlyBuilt(List<Type> alreadyBuiltDefinitions)
            {
                return Dependencies
                    .All(dependency =>
                        alreadyBuiltDefinitions.LastIndexOf(dependency) <
                        alreadyBuiltDefinitions.LastIndexOf(Depender));
            }

            internal bool DependsOn(MetadataDefinitionDependency other)
            {
                return Dependencies.Contains(other.Depender);
            }

            internal IEnumerable<MetadataDefinitionDependency> GetDependentDefinitions(
                IEnumerable<MetadataDefinitionDependency> allDependencies)
            {
                return GetDependentDefinitions(allDependencies, Enumerable.Empty<MetadataDefinitionDependency>());
            }

            IEnumerable<MetadataDefinitionDependency> GetDependentDefinitions(
                IEnumerable<MetadataDefinitionDependency> allDependencies,
                IEnumerable<MetadataDefinitionDependency> myDependencies)
            {
                if (myDependencies.Contains(this))
                {
                    throw new CircularRefenceException(myDependencies);
                }
                else
                {
                    var myDependersDependencies = new List<MetadataDefinitionDependency>(myDependencies);
                    myDependersDependencies.Add(this);
                    return allDependencies
                        .Where(mddd => mddd.DependsOn(this))
                        .SelectMany(mddd => mddd.GetDependentDefinitions(allDependencies, myDependersDependencies));
                }
            }
        }

        class MetadataDefinitionDependencyComparer : IComparer<MetadataDefinitionDependency>
        {
            public int Compare(MetadataDefinitionDependency x, MetadataDefinitionDependency y)
            {
                if (x.DependsOn(y))
                {
                    return 1;
                }
                else if (y.DependsOn(x))
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        [Serializable]
        public class CircularRefenceException : Exception
        {
            protected CircularRefenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            internal CircularRefenceException(IEnumerable<MetadataDefinitionDependency> circularDependencies)
                : base("The configuration contains a circular reference created by the following metadata definitions copying from each other:" +
                        circularDependencies.Aggregate(
                            string.Empty,
                            (acc, d) => acc += Environment.NewLine + d.Depender.FullName)) { }
        }

        [Serializable]
        public class NoMetadataDefinedException : Exception
        {
            protected NoMetadataDefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

            internal NoMetadataDefinedException(Type dependencyModelType, Type dependerMetadataType)
                : base(
                string.Format(
                    CultureInfo.CurrentCulture,
@"The metadata for type '{0}' is either not defined or was not passed to the {1} for building.
Therefore '{2}' cannot copy from it.",
                    dependencyModelType.FullName,
                    typeof(FluentMetadataBuilder).Name,
                    dependerMetadataType.FullName)) { }
        }
    }
}