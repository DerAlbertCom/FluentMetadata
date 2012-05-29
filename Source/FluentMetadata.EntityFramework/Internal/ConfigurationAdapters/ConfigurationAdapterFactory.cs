using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal class ConfigurationAdapterFactory
    {
        private readonly Dictionary<Type, Type> adapters = new Dictionary<Type, Type>();

        public ConfigurationAdapterFactory()
        {
            AddAllAdapters();
        }

        private void AddAllAdapters()
        {
            var result = from t in GetType().Assembly.GetTypes()
                         where t.Is<ConfigurationAdapter>() && !t.IsAbstract
                         select t;
            foreach (var type in result)
            {
                var configurationAdapter = (ConfigurationAdapter)Activator.CreateInstance(type);
                adapters.Add(configurationAdapter.ConfigurationType, configurationAdapter.GetType());
            }
        }

        public ConfigurationAdapter Create(PropertyConfiguration configuration)
        {
            Type type = configuration.GetType();
            do
            {
                if (adapters.ContainsKey(type))
                {
                    return (ConfigurationAdapter)Activator.CreateInstance(adapters[type]);
                }
                type = type.BaseType;
            } while (type != null && type != typeof(object));

            throw new InvalidOperationException("unknown configuration " + configuration.GetType().Name);
        }
    }
}