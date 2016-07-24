using System.Collections.Generic;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Autofac;

namespace UKSSDC.Services.CsvReader
{
    public interface ICsvReader : IDependency
    {
        IList<string> Read(string filePath, int progress, bool readAll);

        int TotalRecords(string filePath, RecordType type);
    }
}
