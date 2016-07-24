using System;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GIQ60Lib;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.CsvReader;
using UKSSDC.Services.Data;
using UKSSDC.Services.ProgressReporter;

namespace UKSSDC
{
    class Postcodes : RecordsCommon
    {
        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly OSTransformationClass _converter; 
        

        public Postcodes(ICsvReader csvReader, IProgressReporter progressReporter)
        {
            _progressReporter = progressReporter;
            _csvReader = csvReader;

            #region Co-ordinate system conversion

            _converter = new OSTransformationClass();

            //TODO: Change to use globals
            string dir = "C:\\Documents and Settings\\All Users\\Application Data\\GridInQuest\\GIQ60";
            //GlobalVar.ProjectRootPath; //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //dir = System.IO.Path.Combine(dir, "GridInQuest");

            //* Set the data file path
            if (_converter.SetDataFilesPath(dir) != eErrorCode.eSuccess)
            {
                //Logger.Fatal("Could not find Grid InQuest data file.");
                throw new FileNotFoundException("Could not find Grid InQuest data file.");
            }
            //* Set the area to Great Britain
            if (_converter.SetArea(eArea.eAreaGreatBritain) != eErrorCode.eSuccess)
            {
                //Logger.Fatal("Failed to set Grid InQuest area to Great Britain.");
                throw new InvalidOperationException("Failed to set Grid InQuest area to Great Britain.");
            }
            //* Initialise
            if (_converter.Initialise("GIQ.6.0") != eErrorCode.eSuccess)
            {
                //Logger.Fatal("Failed to initialize Grid InQuest.");
                throw new InvalidOperationException("Failed to initialize Grid InQuest.");
            }

            #endregion
        }

        public void Run()
        {
            Console.WriteLine("Postcodes");

            var inCompleteFiles = _progressReporter.Report(RecordType.Postcode);

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
            string area = Path.GetFileNameWithoutExtension(inCompleteFile.FileName);

            Console.WriteLine(area);

            var rawRecords = _csvReader.Read(inCompleteFile.FileName, 0, false);

            var unitOfWork = new UnitOfWork();

            //new ParallelOptions { MaxDegreeOfParallelism = 100 },

            Parallel.ForEach(rawRecords, (rawRecord) => {

                string[] x = SplitCsvLineComma(rawRecord);

                string fullPostcode = x[0];

                string outwardCode = getOutwardCode(fullPostcode);

                PostCodeWkt postCodeWkt = new PostCodeWkt();

                try
                {
                    Postcode postcode = new Postcode
                    {
                        Area = area,
                        FullPostcode = x[0],
                        PositionalQualityIndicator = x[1],
                        Easting = Int32.Parse(x[2]),
                        Northing = Int32.Parse(x[3]),
                        Location = postCodeWkt.PostcodeWkt(Int32.Parse(x[2]), Int32.Parse(x[3]), _converter),
                        OutwardCode = outwardCode
                    };

                    lock (unitOfWork)
                    {
                        unitOfWork.Postcodes.Add(postcode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following record could not be added as a postcode:");
                    Console.WriteLine(rawRecord);
                }

            });

            unitOfWork.SaveChanges();

            double result = (unitOfWork.Postcodes.Count(x => x.Area == area) / inCompleteFile.TotalRecords) * 100;

            unitOfWork.Dispose();

            return result;
        }

        private string getOutwardCode(string fullPostcode)
        {
            return fullPostcode.Remove(fullPostcode.Length - 3);
        }

        private class PostCodeWkt {
            public DbGeography PostcodeWkt(int easting, int northing, OSTransformationClass converter)
            {
                eVertDatum output;

                //TODO: Get input height data
                int inputHeight = 0;

                eErrorCode result = converter.SetOSGB36(easting, northing, inputHeight, out output);

                double outputLatitude;
                double outputLongitude;
                double outputHeight;
                converter.GetETRS89Geodetic(out outputLatitude, out outputLongitude, out outputHeight);

                double latitude = Math.Round(outputLatitude, 4);
                double longtitude = Math.Round(outputLongitude, 4);

                string pointText = string.Format("POINT ({0} {1})", longtitude, latitude);

                DbGeography point = DbGeography.PointFromText(pointText, 4326);

                return point;
            }
        }
    }
}
