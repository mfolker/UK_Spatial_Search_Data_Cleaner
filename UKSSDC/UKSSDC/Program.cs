using System;
using System.Reflection;
using Autofac;
using log4net;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

//TODO: Consider restructuring so that each record handled can be recorded as successful or not.

//TODO: Version that strips out excess data

namespace UKSSDC
{
    class Program
    {
        private static IContainer Container { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger("Test");

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Program starting...");

            Console.WriteLine(GlobalVar.ProjectRootPath);
            
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).As<IDependency>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<Places>().InstancePerLifetimeScope();
            builder.RegisterType<Postcodes>().InstancePerLifetimeScope();
            builder.RegisterType<PostcodePerimeters>().InstancePerLifetimeScope();
            builder.RegisterType<Regions>().InstancePerLifetimeScope();
            builder.RegisterType<Roads>().InstancePerLifetimeScope();

            Container = builder.Build();

            //Empty the database?
            Console.WriteLine("Would you like to truncate the database? Y / N");
            string x = Console.ReadLine();

            if (x == "Y" || x == "y")
            {
                Console.WriteLine("Emptying the database");
                var uow = new UnitOfWork();
                uow.Database.ExecuteSqlCommand("DELETE FROM ImportProgresses");
                uow.Database.ExecuteSqlCommand("DELETE FROM Places");
                uow.Database.ExecuteSqlCommand("DELETE FROM PostCodes");
                uow.Database.ExecuteSqlCommand("DELETE FROM PostCodePerimeters");
                uow.Database.ExecuteSqlCommand("DELETE FROM Regions");
                uow.Database.ExecuteSqlCommand("DELETE FROM Roads");
            }

            using (var scope = Container.BeginLifetimeScope())
            {
                var reporter = scope.Resolve<IProgressReporter>();

                Console.WriteLine("Please enter the directory where you location data is stored, or press enter to run on default:");

                string directory = Console.ReadLine(); 

                reporter.Initialise(directory); 

                //Places
                //var places = scope.Resolve<Places>();
                //bool placesComplete = places.Run();

                //Postcodes
                //var postcodes = scope.Resolve<Postcodes>();
                //bool postcodesComplete = postcodes.Run();

                //if (postcodesComplete)
                //{
                //    var postcodeperimeter = scope.Resolve<PostcodePerimeters>();
                //    bool postcodeperimieterComplete = postcodeperimeter.Run();
                //}
                //else
                //{
                //    Console.WriteLine("Post code perimeters could not be built as the postcode import did not succeed.");
                //}

                //Regions
                var regions = scope.Resolve<Regions>();
                bool regionsComplete = regions.Run();

                //Roads
                //var roads = scope.Resolve<Roads>();
                //bool roadsComplete = roads.Run();

                //TODO: Success messages for each?
                
                Console.WriteLine("UKSSDC has finished running");
            }

            Console.WriteLine("Press Enter to close the program");
            Console.Read();
        }

    }

    public static class GlobalVar
    {
        public static readonly string RunPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static readonly string ProjectRootPath = RunPath.Remove(RunPath.Length - 9);


    }
}