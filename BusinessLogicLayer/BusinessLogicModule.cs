using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;
using Autofac;

namespace BusinessLogicLayer
{
    public class BusinessLogicModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>()
                   .As<IPostService>()
                   .InstancePerLifetimeScope();
        }
    }
}
