using System;
using System.Collections.Generic;
using System.IO;
using UKSSDC.Models;
using UKSSDC.Models.Enums;


namespace UKSSDC.Services.Import
{
    public class ProgressReporter : IProgressReporter
    {
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

            string[] directories = Directory.GetDirectories(path, "*");


            //TODO: Adjust to take relative paths
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file); 


                //TODO: Log file outputs. 
                //TODO: Research logging.
            }

            return true;
        }

    }
}