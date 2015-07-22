using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;


namespace UKSSDC.Services.Import
{
    public class ProgressReporter : IProgressReporter
    {
        private string[] _directories; 

        private readonly IUnitOfWork _unitOfWork;

        public ProgressReporter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ImportProgress> Report(RecordType recordType)
        {
            switch (recordType)
            {
                    case RecordType.Place:
                    //TODO: Read database for places imports and return all incomplete.
                    break;

                    case RecordType.Postcode:
                    //TODO: Read database for postcode imports and return all incomplete.
                    break;

                    case RecordType.Region:
                    //TODO: Read database for region imports and return all incomplete.
                    break;

                    case RecordType.Road:
                    //TODO: Read database for road imports and return all incomplete.
                    break;
            }
            
            throw new NotImplementedException();
        }


        public bool Initialise(string path)
        {
            if (path == null) //TODO: Take the path that the program is operating from.
            {
                path =
                    "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV";
            }

            try
            {
                _directories = Directory.GetDirectories(path, "*");
            }
            catch (Exception ex)
            {
                //TODO: Handle and log correctly. 
                Console.WriteLine("Sorry, directory path isn't valid");
                Console.WriteLine("Exception thrown");
                return false;
            }
            
            foreach (string directory in _directories)
            {
                if (directory.Contains("Places"))
                {
                    AddRecord(directory, RecordType.Place);
                }
                else if (directory.Contains("PostCodes"))
                {
                    AddRecord(directory, RecordType.Postcode);
                }
                else if (directory.Contains("Regions"))
                {
                    AddRecord(directory, RecordType.Region);
                }
                else if (directory.Contains("Roads"))
                {
                    AddRecord(directory, RecordType.Road);
                }
                else
                {
                    Console.WriteLine("The following directory was found but is not supported by this program:");
                    Console.WriteLine(directory);
                }
            }

            return true;
        }

        //TODO: Review use of Async - refactoring required to use.
        //To anybody reading this. I'm mostly using async here because I can, when I find a way of writing all records at once, it might be more appropriate. 
        private void AddRecord(string directory, RecordType type)
        {
            string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            // ReSharper disable once SuggestVarOrType_Elsewhere
            List<ImportProgress> preStore = files.Where(file => !file.Contains(".csvt")).Select(file => ImportProgress.Create(file, type)).ToList();

            _unitOfWork.ImportProgress.AddRange(preStore.AsEnumerable()); 

            _unitOfWork.Save();
        }

    }
}