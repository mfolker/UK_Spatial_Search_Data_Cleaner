using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using log4net.Config;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC
{
    public class Regions : RecordsCommon, IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork));

        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;

        public Regions(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();

            _progressReporter = progressReporter;
            _csvReader = placeReader;
            _unitOfWork = unitOfWork;
        }

        public bool Run()
        {
            bool success = true;

            var inCompleteFiles = _progressReporter.Report(RecordType.Region);

            if (inCompleteFiles == null)
                return true;

            foreach (ImportProgress inCompleteFile in inCompleteFiles)
            {
                string fileName = inCompleteFile.FileName;
                int importSuccess = Import(inCompleteFile);
                if (importSuccess != 100)
                {
                    success = false;
                    string formatLog = string.Format("The following file was not correctly stored: {0}", fileName);
                    Logger.Fatal(formatLog);
                    Console.WriteLine("Problems encountered importing the following file:");
                    Console.WriteLine(fileName);
                    Console.WriteLine("This file is only {0}% complete", importSuccess);
                }
            }

            return success;
        }

        private int Import(ImportProgress inCompleteFile)
        {
            string unknownRegion = inCompleteFile.FileName;

            RegionType regionType = DetermineRegionType(unknownRegion); 

            while (inCompleteFile.ProcessedRecords < inCompleteFile.TotalRecords)
            {
                //TODO: Hook up the last parameter in the function that is invoked below so that a choice can be made about chunks to read. 
                
                if (inCompleteFile.ProcessedRecords == 0)
                    inCompleteFile.ProcessedRecords++; //Plus one due to heading line in places csvs

                var rawRecords = _csvReader.Read(inCompleteFile.FileName, (inCompleteFile.ProcessedRecords), true);

                List<Region> regions = new List<Region>();

                foreach (var rawRecord in rawRecords)
                {
                    Console.WriteLine("Processing Record: {0}", rawRecord);

                    string[] x = SplitCsvLineRegion(rawRecord);
                    try
                    {
                        Region region = new Region
                        {
                            Perimeter = DbGeography.MultiPolygonFromText(x[0], 4326),
                            Name = x[1],
                            Type = regionType
                        };

                        regions.Add(region);
                        inCompleteFile.ProcessedRecords++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following record could not be added as a region:");
                        Console.WriteLine(rawRecord);
                        Logger.Error("The following record could not be added as a region. The exception produced is logged below.");
                        Logger.Error(rawRecord);
                        Logger.Error(ex);
                        inCompleteFile.ProcessedRecords++;
                    }
                }

                _unitOfWork.Regions.AddRange(regions.AsEnumerable());
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);
                _unitOfWork.Save();

            }

            if (inCompleteFile.ProcessedRecords == inCompleteFile.TotalRecords)
            {
                inCompleteFile.Complete = true;
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);
                _unitOfWork.Save();
            }

            int records = _unitOfWork.Regions.Count(x => x.Type == regionType);
            return ((records / inCompleteFile.ProcessedRecords) * 100); 

        }

        private string[] SplitCsvLineRegion(string rawRecord)
        {
            
            
            
            throw new NotImplementedException();
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