//using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;
using Autofac;
using Autofac.Features.AttributeFilters;

namespace BusinessLogicLayer
{
    public class BusinessLogicModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>()
                   .AsSelf()
                   .InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
