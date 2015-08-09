using System;
using log4net;
using log4net.Config;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC
{
    public class Postcodes : RecordsCommon, IRecord
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWork)); 

        private readonly ICsvReader _csvReader;
        private readonly IProgressReporter _progressReporter;
        private readonly IUnitOfWork _unitOfWork;
        

        public Postcodes(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
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
            throw new NotImplementedException();
        }
    }
}