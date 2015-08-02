using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class ImportProgress : Common
    {
        public RecordType RecordType { get; private set; }

        public string FileName { get; set; }

        //Record number holds the number of thelast record that was processed. 
        public int RecordNumber { get; private set; } //TODO: Change the name of this record to be more suggestive of thefunctionality

        public bool Complete { get; private set; }

        public static ImportProgress Create(string filename, RecordType type) 
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
