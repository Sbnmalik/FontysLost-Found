using Autofac;
using Persistence.Repositories;
using System.Reflection;


namespace Persistence
{
    public class PersistenceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //Explicitly register PostRepository as IPostRepository
            builder.RegisterType<PostRepository>()
                .AsSelf()
                .InstancePerLifetimeScope();



        }
    }
}
