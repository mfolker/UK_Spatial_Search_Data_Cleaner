using System;
using System.Collections.Generic;
using System.IO;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;


namespace UKSSDC.Services.Import
{
    public class ProgressReporter : IProgressReporter
    {
        private string[] directories;

        public UnitOfWork UnitOfWork;


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
            
            throw new System.NotImplementedException();
        }


        public bool Initialise(string path = null)
        {
            //TODO: Loop through every sub directory and file of /CSV/* then 

            if (path == null)
            {
                path =
                    "C:\\Users\\Matthew\\Desktop\\Project\\Implementation\\Maps Data\\UK_Spatial_Search_Data_Cleaner\\UKSSDC\\UKSSDC\\CSV";
            }


            try
            {
                directories = Directory.GetDirectories(path, "*");
            }
            catch (Exception)
            {
                Console.WriteLine("Sorry, directory path isn't valid");

                //TODO: Handle program flow if the directory isn't valid.

                throw;

            }
            

            foreach (string directory in directories)
            {
                if (directory.Contains("Places"))
                {
                    
                }
                else if (directory.Contains("PostCodes"))
                {
                    
                }
                else if (directory.Contains("Regions"))
                {
                    
                }
                else if (directory.Contains("Roads"))
                {

                }
                else
                {
                    //TODO: Implement actions for others
                }

            }

            return true;
        }

        //TODO: Review use of Async 
        //To anybody reading this. I'm mostly using async here because I can, when I find a way of writing all records at once, it might be more appropriate. 
        private async void AddRecord(string directory)
        {
            string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            //TODO: Determine if there is a cleaner way of doing this, i.e. saving all records at once. 

            foreach (string file in files)
            {
                if (file.Contains(".csvt")) //Stops the program trying to handle csv
                {
                    break;
                }

                //TODO: Isolate file name.

                var progressRecord = new ImportProgress(); 
               

            }
        }

    }
}