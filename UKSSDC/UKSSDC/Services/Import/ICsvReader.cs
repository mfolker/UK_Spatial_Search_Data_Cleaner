using System.Collections.Generic;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.Import
{
    public interface ICsvReader : IDependency
    {
        IEnumerable<object> Read(string filePath, int progress);

        int TotalRecords(string filePath, RecordType type);
    }
}
