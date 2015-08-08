using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;
using log4net;
using log4net.Config;

namespace UKSSDC
{
    public class Places : IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork)); 

        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private IUnitOfWork _unitOfWork;
        

        public Places(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();

            _progressReporter = progressReporter;
            _csvReader = placeReader;
            _unitOfWork = unitOfWork;
        }

        public bool Run()
        {
            bool success = true; 

            var inCompleteFiles = _progressReporter.Report(RecordType.Place);

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

            Country country = determineCountry(unknownCountry);

            while (inCompleteFile.ProcessedRecords < inCompleteFile.TotalRecords)
            {
                //TODO: Hook up the last parameter in the function that is invoked below so that a choice can be made about chunks to read. 
                
                if (inCompleteFile.ProcessedRecords == 0)
                    inCompleteFile.ProcessedRecords++; //Plus one due to heading line in places csvs
                
                var rawRecords = _csvReader.Read(inCompleteFile.FileName, (inCompleteFile.ProcessedRecords), true);
                
                List<Place> places = new List<Place>();

                foreach (var rawRecord in rawRecords)
                {
                    Console.WriteLine("Processing Record: {0}", rawRecord);
                    string[] x = SplitCsvLine(rawRecord);
                    try
                    {
                        Place place = new Place
                        {
                            Country = country,
                            Location = DbGeography.PointFromText(x[0], 4326),
                            OsmId = Int64.Parse(x[1]),
                            Name = x[2],
                            Created = DateTime.UtcNow
                        };

                        places.Add(place);
                        inCompleteFile.ProcessedRecords++; 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following record could not be added as a place:");
                        Console.WriteLine(ex);
                        Console.WriteLine(rawRecord);
                        Logger.Error(rawRecord);
                        Logger.Error("The following record could not be added as a place. The exceptin produced is logged below.");
                        inCompleteFile.ProcessedRecords++;
                    }
                }

                _unitOfWork.Places.AddRange(places.AsEnumerable());
                _unitOfWork.ImportProgress.Add(inCompleteFile);
                _unitOfWork.Save();
            }

            if (inCompleteFile.ProcessedRecords == inCompleteFile.TotalRecords)
            {
                inCompleteFile.Complete = true;
                _unitOfWork.ImportProgress.Add(inCompleteFile);
                _unitOfWork.Save();
            }

            int records = _unitOfWork.Places.Count(x => x.Country == country);
            return ((records/inCompleteFile.TotalRecords)*100);
        }

        //TODO: Add to common records class? 
        private string[] SplitCsvLine(string csvLine)
        {
            string[] attributes = csvLine.Split(',');
            attributes[0] = StripEscapeCharacters(attributes[0]); 
            return attributes; 
        }

        private Country determineCountry(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            if (fileName.Contains("England"))
            {
                return Country.England;
            }
            else if (fileName.Contains("Scotland"))
            {
                return Country.Scotland;
            }
            else
            {
                return Country.Wales;
            }
        }

        private string StripEscapeCharacters(string wktEscaped)
        {
            string result = wktEscaped.Replace("\"", "");
            return result;
        }

    }
}