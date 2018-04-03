using System.Reflection;
using Autofac;
using Manager.Core.Types;

namespace Manager.Struct.IoC.Modules
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(DomainModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IQuery>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}