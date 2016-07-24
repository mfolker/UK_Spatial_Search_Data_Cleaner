using System;
using System.IO;
using System.Reflection;
using Autofac;
using log4net.Config;
using UKSSDC.Services.Data;
using UKSSDC.Services.ProgressReporter;

[assembly: XmlConfigurator(Watch = true)]

namespace UKSSDC
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = AutofacConfig.GetContainer();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Program starting...");
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

            using (var scope = container.BeginLifetimeScope())
            {
                var reporter = scope.Resolve<IProgressReporter>();
                Console.WriteLine("Please enter the directory where you location data is stored, or press enter to run on default:");
                string directory = Console.ReadLine();
                Console.WriteLine("Processing...");
                reporter.Initialise(directory);

                //var places = scope.Resolve<Places>();
                //places.Run();

                //var postcodes = scope.Resolve<Postcodes>();
                //postcodes.Run();

                //var postcodePerimeters = scope.Resolve<PostcodePerimeters>();
                //postcodePerimeters.Run();

                //var regions = scope.Resolve<Regions>();
                //regions.Run();

                Console.WriteLine("Building the Spatial Search Table");
                var searchCollectionBuilder = scope.Resolve<SearchCollectionBuilder>();
                searchCollectionBuilder.Run();
            }

        }
    }

    public static class GlobalVar
    {
        public static readonly string RunPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static readonly string ProjectRootPath = RunPath.Remove(RunPath.Length - 9);


    }
}


