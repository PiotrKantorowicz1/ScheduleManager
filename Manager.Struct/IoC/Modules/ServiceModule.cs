using System.Reflection;
using Autofac;
using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Struct.Services;
using Microsoft.AspNetCore.Identity;

namespace Manager.Struct.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();

            builder.RegisterType<PasswordHasher<User>>()
                .As<IPasswordHasher<User>>()
                .SingleInstance();

            builder.RegisterType<ManagerDbContext>()
                .As<IUnitOfWork>()
                .SingleInstance();
        }
    }
}