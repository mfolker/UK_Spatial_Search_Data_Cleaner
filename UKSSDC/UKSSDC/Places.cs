using System;
using UKSSDC.Services.Import;
using UKSSDC.Models.Enums;

namespace UKSSDC
{
    public class Places
    {

        private IPlaceReader _placeReader;
        private IProgressReporter _progressReporter;

        public Places(IPlaceReader placeReader, IProgressReporter progressReporter)
        {
            _progressReporter = progressReporter;
            _placeReader = placeReader;
        }

        public Places()
        {
            // TODO: Complete member initialization
        }

        public bool CheckComplete()
        {
            return true;
        }

        public void Start()
        {
            //Todo: check progess

            _progressReporter.Report(RecordType.Place);
            
            //Loop through each file, and process each record.

            //Todo: Read the csv into memory.

            //Todo: Loop through every line and format correctly

            Complete();

        }

        internal void Complete()
        {
            //TODO: Dry up? 

            Console.WriteLine("All places have been imported");
        }
    }
}