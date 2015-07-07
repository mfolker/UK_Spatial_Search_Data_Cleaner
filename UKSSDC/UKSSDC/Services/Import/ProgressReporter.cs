using System.Collections.Generic;
using System.IO;
using UKSSDC.Models;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.Import
{
    internal class ProgressReporter : IProgressReporter
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
            
            //throw new System.NotImplementedException();
        }


        public bool Initialise()
        {
            //TODO: Loop through every sub directory and file of /CSV/* then 

            string[] files = Directory.GetFiles("CSV/", "*.*", SearchOption.AllDirectories);



            throw new System.NotImplementedException();
        }
    }
}