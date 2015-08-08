using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net.Repository.Hierarchy;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;

namespace UKSSDC.Services.Import
{
    public class ProgressReporter : IProgressReporter
    {
        private string[] _directories; 

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICsvReader _csvReader;

        public ProgressReporter(IUnitOfWork unitOfWork, ICsvReader csvReader)
        {
            _unitOfWork = unitOfWork;
            _csvReader = csvReader;
        }

        public List<ImportProgress> Report(RecordType recordType)
        {
            List<ImportProgress> report = new List<ImportProgress>();

            report = _unitOfWork.ImportProgress.Where(x => x.Complete == false && x.RecordType == recordType).ToList();

            return report; 
        }


        public bool Initialise(string path)
        {
            if (path == "") //TODO: Take the path that the program is operating from or C:\\Places_CSV
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
                Console.WriteLine(ex);
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

        
        private void AddRecord(string directory, RecordType type)
        {
            string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            // ReSharper disable once SuggestVarOrType_Elsewhere
            List<ImportProgress> records = files.Where(file => !file.Contains(".csvt")).Select(file => ImportProgress.Create(file, type)).ToList();

            foreach (var record in records)
            {
                record.TotalRecords = _csvReader.TotalRecords(record.FileName, type);
            }

            _unitOfWork.ImportProgress.AddRange(records.AsEnumerable()); 

            _unitOfWork.Save();
        }

    }
}