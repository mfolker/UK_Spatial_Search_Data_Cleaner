using System;
using UKSSDC.Services.Import;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;

namespace UKSSDC
{
    public class Places : IRecord
    {

        private IPlaceReader _placeReader;
        private IProgressReporter _progressReporter;
        private IUnitOfWork _unitOfWork;

        public Places(IPlaceReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            _progressReporter = progressReporter;
            _placeReader = placeReader;
            _unitOfWork = unitOfWork;
        }

        public Places()
        {
            // TODO: Complete member initialization
        }

        public bool Run()
        {
            //Get all files that are not complete.

            var inCompleteFiles = _progressReporter.Report(RecordType.Place);

            if (inCompleteFiles == null)
                return true;
            
            foreach (var inCompleteFile in inCompleteFiles)
            {

            }

            return true;                 
            
        }

    }
}