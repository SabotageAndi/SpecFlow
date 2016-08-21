﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Generator.Interfaces;
using TechTalk.SpecFlow.Generator.Project;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.PlatformSpecific;
using TechTalk.SpecFlow.Plugins;

namespace TechTalk.SpecFlow.Generator.Configuration
{
    public class GeneratorConfigurationProvider : IGeneratorConfigurationProvider
    {
        private readonly IRuntimeConfigurationLoader _runtimeConfigurationLoader;

        public GeneratorConfigurationProvider(IRuntimeConfigurationLoader runtimeConfigurationLoader)
        {
            _runtimeConfigurationLoader = runtimeConfigurationLoader;
        }

        public virtual void LoadConfiguration(SpecFlowConfigurationHolder configurationHolder, SpecFlowProjectConfiguration configuration)
        {
            try
            {
                if (configurationHolder != null && configurationHolder.HasConfiguration)
                {
                    ConfigurationSectionHandler specFlowConfigSection =
                        ConfigurationSectionHandler.CreateFromXml(configurationHolder.XmlString);
                    if (specFlowConfigSection != null)
                    {
                        UpdateConfiguration(configuration, specFlowConfigSection);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new ConfigurationErrorsException("SpecFlow configuration error", ex);
            }
        }

        public IEnumerable<PluginDescriptor> GetPlugins(SpecFlowConfigurationHolder configurationHolder)
        {
            try
            {
                if (configurationHolder != null && configurationHolder.HasConfiguration)
                {
                    ConfigurationSectionHandler section = ConfigurationSectionHandler.CreateFromXml(configurationHolder.XmlString);
                    if (section != null && section.Plugins != null)
                    {
                        return section.Plugins.Select(pce => pce.ToPluginDescriptor());
                    }
                }

                return Enumerable.Empty<PluginDescriptor>();
            }
            catch(Exception ex)
            {
                throw new ConfigurationErrorsException("SpecFlow configuration error", ex);
            }
        }

        internal virtual void UpdateConfiguration(SpecFlowProjectConfiguration configuration, ConfigurationSectionHandler specFlowConfigSection)
        {
            configuration.RuntimeConfiguration = _runtimeConfigurationLoader.Update(configuration.RuntimeConfiguration, specFlowConfigSection);
        }
        
    }
}