using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class ImportProgress : Common
    {
        public RecordType RecordType { get; private set; }

        public string FileName { get; private set; }

        public int RecordNumber { get; private set; }

        public bool Complete { get; private set; }

        //TODO: Check use of static is correct here.
        public static ImportProgress Add(string filename, RecordType type) 
        {
            return new ImportProgress
            {
                RecordType = type,
                FileName = filename,
                Complete = false,
                RecordNumber = 0
            };
        }
    }


}
