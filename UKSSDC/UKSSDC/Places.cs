using System;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.CsvReader;
using UKSSDC.Services.Data;
using UKSSDC.Services.ProgressReporter;

namespace UKSSDC
{
    class Places : RecordsCommon
    {
        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;

        public Places(ICsvReader csvReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            _csvReader = csvReader;
            _progressReporter = progressReporter;
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            Console.WriteLine("Places");

            var inCompleteFiles = _progressReporter.Report(RecordType.Place);

            if (inCompleteFiles != null)
            {
                foreach (ImportProgress inCompleteFile in inCompleteFiles)
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    double percentComplete = Import(inCompleteFile);
                    timer.Stop();
                    string result = String.Format("{0}% Complete in {1}", percentComplete.ToString(CultureInfo.InvariantCulture), timer.Elapsed);
                    Console.WriteLine(result);
                }

            }
                
        }

        public double Import(ImportProgress inCompleteFile)
        {
            string unknownCountry = inCompleteFile.FileName;
            Country country = DetermineCountry(unknownCountry);
            Console.WriteLine(country.ToString());
            
            var rawRecords = _csvReader.Read(inCompleteFile.FileName, 1, false);

            Parallel.ForEach(rawRecords, (rawRecord) =>
            {
                string[] x = SplitCsvLineComma(rawRecord);
                try
                {
                    Place place = new Place
                    {
                        Country = country,
                        Location = DbGeography.PointFromText(x[0], 4326),
                        OsmId = Int64.Parse(x[1]),
                        Name = x[2]
                    };

                    lock (_unitOfWork)
                    {
                        _unitOfWork.Places.Add(place); 
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following record could not be added as a place:");
                    Console.WriteLine(rawRecord);
                    //TODO: Log
                }
            });

            _unitOfWork.SaveChanges();
            
            return (_unitOfWork.Places.Count(x => x.Country == country) / inCompleteFile.TotalRecords) * 100;

        }
    }
}
