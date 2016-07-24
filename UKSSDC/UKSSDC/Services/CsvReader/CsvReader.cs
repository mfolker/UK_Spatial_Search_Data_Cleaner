using System.Collections.Generic;
using System.IO;
using System.Linq;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public IList<string> Read(string filePath, int progress, bool readPart = false)
        {
            if (readPart)
            {
                IEnumerable<int> range = Enumerable.Range(progress, 2500);
                return File.ReadLines(filePath).Where((l, i) => range.Contains(i)).ToList();
            }

            return File.ReadAllLines(filePath).Skip(progress).ToList();
        }

        public int TotalRecords(string filePath, RecordType type)
        {
            //Places have 1 line at the top
            int total = File.ReadAllLines(filePath).Length;
            
            //If the type is post code, take one off the top.
            return type == RecordType.Postcode ? total : --total;
        }
    }

}
