using Autofac;
using BusinessLogicLayer.Repositories;
using System.Reflection;


namespace BusinessLogicLayer
{
    public class PersistenceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //Explicitly register PostRepository as IPostRepository
            builder.RegisterType<PostRepository>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository> ()
                .AsSelf()
                .InstancePerLifetimeScope();

        }
    }
}
