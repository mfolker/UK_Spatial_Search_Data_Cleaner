using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class ImportProgress : Common
    {
        public RecordType RecordType { get; private set; }

        public string FileName { get; set; }

        //Record number holds the number of the last record that was processed. 
        public int ProcessedRecords { get; set; }

        public int TotalRecords { get; set; }

        public bool Complete { get; set; }

        public static ImportProgress Create(string filename, RecordType type) 
        {
            return new ImportProgress
            {
                RecordType = type,
                FileName = filename,
                Complete = false,
                ProcessedRecords = 0,
                TotalRecords = 0
            };
        }
    }


}
