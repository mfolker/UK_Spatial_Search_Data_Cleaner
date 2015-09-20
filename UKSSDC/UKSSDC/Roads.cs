using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace UKSSDC
{
    public class Roads : RecordsCommon, IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork));

        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;

        public Roads(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();

            _progressReporter = progressReporter;
            _csvReader = placeReader;
            _unitOfWork = unitOfWork;
        }

        public bool Run()
        {
            bool success = true;

            var inCompleteFiles = _progressReporter.Report(RecordType.Road);

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
            string unknownCountry = inCompleteFile.FileName;

            Country country = DetermineCountry(unknownCountry);

            while (inCompleteFile.ProcessedRecords < inCompleteFile.TotalRecords)
            {
                //TODO: Hook up the last parameter in the function that is invoked below so that a choice can be made about chunks to read. 

                if (inCompleteFile.ProcessedRecords == 0)
                    inCompleteFile.ProcessedRecords++; //Plus one due to heading line in places csvs

                var rawRecords = _csvReader.Read(inCompleteFile.FileName, (inCompleteFile.ProcessedRecords), true);

                List<Road> roads = new List<Road>();

                foreach (var rawRecord in rawRecords)
                {
                    Console.WriteLine("Processing Record: {0}", rawRecord);

                    string[] x = SplitCsvLinePipe(rawRecord);
                    try
                    {
                        int MaxSpeed = 0;

                        Int32.TryParse(x[8], out MaxSpeed);

                        Road road = new Road
                        {
                            Country = country,
                            Path = DbGeography.LineFromText(x[0], 4326),  //PointFromText(x[0], 4326),
                            OsmId = Int64.Parse(x[1]),
                            Name = x[2],
                            ReferenceNumber = x[3],
                            Type = x[4],
                            MaxSpeed = MaxSpeed,
                        };

                        roads.Add(road);
                        inCompleteFile.ProcessedRecords++; 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following record could not be added as a road:");
                        Console.WriteLine(rawRecord);
                        Logger.Error("The following record could not be added as a road. The exception produced is logged below.");
                        Logger.Error(rawRecord);
                        Logger.Error(ex);
                        inCompleteFile.ProcessedRecords++;
                    }
                }

                _unitOfWork.Roads.AddRange(roads.AsEnumerable());
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);  
                _unitOfWork.Save();

            }

            if (inCompleteFile.ProcessedRecords == inCompleteFile.TotalRecords)
            {
                inCompleteFile.Complete = true;
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);
                _unitOfWork.Save();
            }

            int records = _unitOfWork.Roads.Count(x => x.Country == country);

            var completeFiles = _unitOfWork.ImportProgress.Where(x => x.Complete);

            int totalProcessed = 0;

            foreach (var completeFile in completeFiles)
            {
                string noCountry = completeFile.FileName;

                Country fileCountry = DetermineCountry(noCountry);

                if (fileCountry == country)
                    totalProcessed = totalProcessed + completeFile.ProcessedRecords;
            }

            return ((records / totalProcessed) * 100);
        }
    }
}