using System.Reflection;
using Autofac;
using UKSSDC.Services.Autofac;

namespace UKSSDC
{
    public static class AutofacConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IDependency>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<Places>().InstancePerLifetimeScope();
            builder.RegisterType<Postcodes>().InstancePerLifetimeScope();
            builder.RegisterType<PostcodePerimeters>().InstancePerLifetimeScope();
            builder.RegisterType<Regions>().InstancePerLifetimeScope();
            builder.RegisterType<Roads>().InstancePerLifetimeScope();
            builder.RegisterType<SearchCollectionBuilder>().InstancePerLifetimeScope(); 

            IContainer container = builder.Build();

            return container;
        }
    }
}