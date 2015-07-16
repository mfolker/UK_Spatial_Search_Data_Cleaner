using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;


namespace UKSSDC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Program starting...");
            //TODO: Start Autofac. 
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IDependency>().AsImplementedInterfaces().InstancePerDependency();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var reporter = scope.Resolve<IProgressReporter>();

                Console.Write("Please enter the directory where you location data is stored, or press enter to run on default:");
                reporter.Initialise(null); 
            }

            
            





        }

        private static IContainer Container { get; set; }
    }
}


            //Console.WriteLine("Starting to build your UK Spatial Search Database");

            //Places places = new Places();

            //if (!places.CheckComplete())
            //{
            //    //Start or carry on importing places

            //    places.Start();
            //}
            //else
            //{
            //    //TODO: Output total number of places imported.

            //    places.Complete();

            //}