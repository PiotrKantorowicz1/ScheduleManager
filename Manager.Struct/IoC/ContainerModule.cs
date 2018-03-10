using Autofac;
using Manager.Struct.IoC.Modules;
using Manager.Struct.Mappers;
using Microsoft.Extensions.Configuration;

namespace Manager.Struct.IoC
{
    public class ContainerModule : Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                   .SingleInstance();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<SqlModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}
