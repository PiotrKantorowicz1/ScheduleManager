using Autofac;
using Manager.Struct.Extensions;
using Manager.Struct.Settings;
using Microsoft.Extensions.Configuration;

namespace Manager.Struct.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                   .SingleInstance();
        }
    }
}