using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.CsvReader;
using UKSSDC.Services.Data;
using UKSSDC.Services.ProgressReporter;

namespace UKSSDC
{
    class Regions : RecordsCommon
    {
        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;

        public Regions(ICsvReader csvReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            _csvReader = csvReader;
            _progressReporter = progressReporter;
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            Console.WriteLine("Regions");

            var inCompleteFiles = _progressReporter.Report(RecordType.Region);

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
            string unknownRegion = inCompleteFile.FileName;
            RegionType regionType = DetermineRegionType(unknownRegion); 
            Console.WriteLine(regionType.ToString());

            var rawRecords = _csvReader.Read(inCompleteFile.FileName, 1, false);

            Parallel.ForEach(rawRecords, (rawRecord) =>
            {
                List<string> x = SplitCsvLineRegion(rawRecord);
                try
                {
                    Region region = new Region
                    {
                        Perimeter = DbGeography.MultiPolygonFromText(x[0], 4326),
                        Name = x[1],
                        Type = regionType
                    };

                    lock (_unitOfWork)
                    {
                        _unitOfWork.Regions.Add(region);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following record could not be added as a region:");
                    Console.WriteLine(rawRecord);
                }

            });

            _unitOfWork.SaveChanges();

            return (_unitOfWork.Regions.Count(x => x.Type == regionType) / inCompleteFile.TotalRecords) * 100;
        }

        private List<string> SplitCsvLineRegion(string rawRecord)
        {
            //rawRecord = StripEscapeCharacters(rawRecord);

            string[] firstSplit = rawRecord.Split('"');

            string[] secondSplit = firstSplit[2].Split(',');

            List<string> result = new List<string> { firstSplit[1], secondSplit[1] };

            return result;
        }

        private RegionType DetermineRegionType(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            if (fileName.Contains("County"))
            {
                return RegionType.County;
            }
            else if (fileName.Contains("DistrictBoroughUnitaryWard"))
            {
                return RegionType.DistrictBoroughUnitaryWard;
            }
            else if (fileName.Contains("GreaterLondonConstituency"))
            {
                return RegionType.GreaterLondonConstituency;
            }
            else
            {
                return RegionType.DistrictBoroughUnitary;
            }
        }
    }
}
