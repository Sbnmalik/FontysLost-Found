using Autofac;
using BusinessLogicLayer.Abstractions;
using Persistence.Repositories;
using System.Reflection;


namespace Persistence
{
    public class PersistenceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register all classes in the Persistence assembly that implement interfaces
            var persistenceAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(persistenceAssembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            // Explicitly register PostRepository as IPostRepository
            builder.RegisterType<postRepository>().As<IPostRepository>().InstancePerLifetimeScope();


        }
    }
}
