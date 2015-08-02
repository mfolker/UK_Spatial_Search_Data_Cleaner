using System;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;
using UKSSDC.Services.Import;

namespace UKSSDC
{
    public class Places : IRecord
    {

        private ICsvReader _placeReader;
        private IProgressReporter _progressReporter;
        private IUnitOfWork _unitOfWork;
        
        private bool success; 

        public Places(ICsvReader placeReader, IProgressReporter progressReporter, IUnitOfWork unitOfWork)
        {
            _progressReporter = progressReporter;
            _placeReader = placeReader;
            _unitOfWork = unitOfWork;
        }

        public bool Run()
        {
            var inCompleteFiles = _progressReporter.Report(RecordType.Place);

            if (inCompleteFiles == null)
                return true;
            
            foreach (ImportProgress inCompleteFile in inCompleteFiles)
            {
                string fileName = inCompleteFile.FileName; 
                bool importSuccess = Import(inCompleteFile);
                if (!importSuccess)
                {
                    //TODO: Add error logging to file. 
                    Console.WriteLine("Problems encountered importing the following file:");
                    Console.WriteLine(fileName);
                }
            }

            if (success)
                return true;
            return false;
        }

        private bool Import(ImportProgress inCompleteFile)
        {
            //Look at where the last import got to.

            var chunk = _placeReader.Read(inCompleteFile.FileName, inCompleteFile.ProcessedRecords); 

            //Take 2.5k from that point onwards or however many are left.

            //Loop through each set of 2.5k and create new Places records (try and log exceptions)

            //Save all to database. 

            //Catch any exceptions when writing to the database. Skip record, log & write to console, then carry on. 

            //if any records where dropped, return message to say how many. 

            //or return true. 

            //update how many records (recordNumber) have been completed. 

            throw new NotImplementedException(); 
        }

    }
}