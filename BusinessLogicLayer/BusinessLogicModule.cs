using BusinessLogicLayer.Services;
using Autofac;
using Autofac.Features.AttributeFilters;
using BusinessLogicLayer.Abstractions;

namespace BusinessLogicLayer
{
    public class BusinessLogicModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>()
                   .As<IPostService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .InstancePerLifetimeScope();
        }
    }
}
