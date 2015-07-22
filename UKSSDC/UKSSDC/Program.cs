using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private static IContainer Container { get; set; }
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Program starting...");
            
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IDependency>().AsImplementedInterfaces().InstancePerDependency();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var reporter = scope.Resolve<IProgressReporter>();

                Console.WriteLine("Please enter the directory where you location data is stored, or press enter to run on default:");

                //TODO: 1 - Handle the console input. Set string to null or input

                reporter.Initialise(null); 

                var places = scope.Resolve<Places>();

                bool placesComplete = places.Run();

                //var postcodes = scope.Resolve<PostCodes>();

                //bool postcodesComplete = 
            }

        }

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