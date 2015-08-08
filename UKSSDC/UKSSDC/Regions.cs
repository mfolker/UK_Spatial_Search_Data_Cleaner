using log4net;
using System;
using log4net.Config;
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

        public static bool CheckComplete()
        {
            return true;
        }

        public bool Run()
        {
            throw new NotImplementedException();
        }
    }
}