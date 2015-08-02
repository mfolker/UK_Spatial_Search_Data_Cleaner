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

        public static String getFileLines(String path, int indexOfLine)
        {
            return File.ReadLines(path).ElementAtOrDefault(indexOfLine);
        }

        public IEnumerable<object> Read(string filePath, int progress)
        {
            throw new NotImplementedException();
        }

        public int TotalRecords(string filePath, RecordType type)
        {
            //Places have 1 line at the top
            int total = File.ReadAllLines(filePath).Length;
            
            return type == RecordType.Postcode ? total : --total;
        }
    }

}
