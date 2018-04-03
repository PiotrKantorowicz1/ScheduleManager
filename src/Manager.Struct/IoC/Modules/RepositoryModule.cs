using System.Reflection;
using Autofac;
using Manager.Core.Repositories;

namespace Manager.Struct.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();
        }
    }
}
