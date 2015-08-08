using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;

namespace UKSSDC.Services.Import
{
    public class CsvReader : ICsvReader
    {
        private readonly IUnitOfWork _unitOfWork;

        public CsvReader (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
            
            return type == RecordType.Postcode ? total : --total;
        }
    }

}
