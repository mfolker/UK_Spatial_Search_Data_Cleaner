using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using log4net;
using log4net.Config;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;
using GIQ60Lib;

namespace UKSSDC
{
    public class Postcodes : RecordsCommon, IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork)); 

        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;
        private OSTransformationClass _converter; 
        
        public Postcodes(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            XmlConfigurator.Configure();

            _progressReporter = progressReporter;
            _csvReader = placeReader;
            _unitOfWork = unitOfWork;
            
            #region Co-ordinate system conversion

            _converter = new OSTransformationClass();

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = System.IO.Path.Combine(dir, "GridInQuest");

            //* Set the data file path
            if (_converter.SetDataFilesPath(dir) != eErrorCode.eSuccess)
            {
                Logger.Fatal("Could not find Grid InQuest data file.");
                throw new FileNotFoundException("Could not find Grid InQuest data file.");
            }
            //* Set the area to Great Britain
            if (_converter.SetArea(eArea.eAreaGreatBritain) != eErrorCode.eSuccess)
            {
                Logger.Fatal("Failed to set Grid InQuest area to Great Britain.");
                throw new InvalidOperationException("Failed to set Grid InQuest area to Great Britain.");
            }
            //* Initialise
            if (_converter.Initialise("GIQ.6.0") != eErrorCode.eSuccess)
            {
                Logger.Fatal("Failed to initialize Grid InQuest.");
                throw new InvalidOperationException("Failed to initialize Grid InQuest.");
            }

            #endregion
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
            string area = Path.GetFileNameWithoutExtension(inCompleteFile.FileName);

            while (inCompleteFile.ProcessedRecords < inCompleteFile.TotalRecords)
            {
                var rawRecords = _csvReader.Read(inCompleteFile.FileName, inCompleteFile.ProcessedRecords, true);

                List<Postcode> postcodes = new List<Postcode>();

                foreach (var rawRecord in rawRecords)
                {
                    Console.WriteLine("Processing Record: {0}", rawRecord);
                    string[] x = SplitCsvLineComma(rawRecord);
                    try
                    {
                        Postcode postcode = new Postcode
                        {
                            Area = area,
                            FullPostcode = x[0],
                            PositionalQualityIndicator = x[1],
                            Easting = Int32.Parse(x[2]),
                            Northing = Int32.Parse(x[3]),
                            Location = PostcodeWkt(Int32.Parse(x[2]), Int32.Parse(x[3]))
                        };

                        postcodes.Add(postcode);
                        inCompleteFile.ProcessedRecords++; 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The following record could not be added as a place:");
                        Console.WriteLine(ex);
                        Console.WriteLine(rawRecord);
                        Logger.Error("The following record could not be added as a place. The exception produced is logged below.");
                        Logger.Error(rawRecord);
                        inCompleteFile.ProcessedRecords++;
                    }

                }

                _unitOfWork.Postcodes.AddRange(postcodes.AsEnumerable());
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);
                _unitOfWork.Save();
                
            }

            if (inCompleteFile.ProcessedRecords == inCompleteFile.TotalRecords)
            {
                inCompleteFile.Complete = true;
                _unitOfWork.ImportProgress.AddOrUpdate(inCompleteFile);
                _unitOfWork.Save();
            }

            int records = _unitOfWork.Postcodes.Count(x => x.Area == area);

            return ((records / inCompleteFile.TotalRecords) * 100);
        }

        private DbGeography PostcodeWkt(int easting, int northing)
        {
            eVertDatum output;

            int inputHeight = 0;

            eErrorCode result = _converter.SetOSGB36(easting, northing, inputHeight, out output);

            double outputLatitude;
            double outputLongitude;
            double outputHeight;
            _converter.GetETRS89Geodetic(out outputLatitude, out outputLongitude, out outputHeight);


            
            throw new NotImplementedException();
        }
    }
}