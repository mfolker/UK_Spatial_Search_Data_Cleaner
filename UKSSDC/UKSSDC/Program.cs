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


//TODO: Consider restructuring so that each record can handled and recorded as successful or not.

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

                var postcodes = scope.Resolve<PostCodes>();

                bool postcodesComplete = postcodes.Run();

                if (postcodesComplete)
                {
                    var postcodeperimeter = scope.Resolve<PostCodePerimeters>();

                    bool postcodeperimieterComplete = postcodeperimeter.Run();
                }

                var regions = scope.Resolve<Regions>();

                bool regionsComplete = regions.Run();

                var roads = scope.Resolve<Roads>();

                bool roadsComplete = roads.Run();

                //TODO: Success messages for each?
                
                Console.WriteLine("UKSSDC has finished running");
            }

            Console.WriteLine("Press Enter to close the program");
            Console.Read();
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