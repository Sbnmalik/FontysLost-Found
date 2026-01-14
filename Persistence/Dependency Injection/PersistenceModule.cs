using Autofac;
using BusinessLogicLayer.Abstractions;
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
                .As<IPostRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository> ()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

        }
    }
}
