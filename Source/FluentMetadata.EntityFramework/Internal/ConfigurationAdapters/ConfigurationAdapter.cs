﻿using System;
using System.Data.Entity.ModelConfiguration;

namespace FluentMetadata.EntityFramework.Internal.ConfigurationAdapters
{
    internal abstract class ConfigurationAdapter
    {
        private readonly ConfigurationAdapter adapter;

        protected ConfigurationAdapter(ConfigurationAdapter adapter)
        {
            this.adapter = adapter;
        }

        public void Convert(Metadata data, PropertyConfiguration configuration)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (configuration == null) throw new ArgumentNullException("configuration");

            Configuration = configuration;
            ConvertToConfiguration(data);
            if (adapter != null)
            {
                adapter.Convert(data, configuration);
            }
        }

        protected abstract void ConvertToConfiguration(Metadata data);

        public abstract Type ConfigurationType { get; }

        protected PropertyConfiguration Configuration { get; private set; }
    }

    internal abstract class ConfigurationAdapter<T> : ConfigurationAdapter where T : PropertyConfiguration
    {
        protected ConfigurationAdapter(ConfigurationAdapter adapter) : base(adapter)
        {
        }

        protected new T Configuration
        {
            get { return (T) base.Configuration; }
        }

        public override Type ConfigurationType
        {
            get { return typeof (T); }
        }
    }
}