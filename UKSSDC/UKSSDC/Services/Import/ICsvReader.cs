using System.Collections.Generic;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.Import
{
    public interface ICsvReader : IDependency
    {
        IList<string> Read(string filePath, int progress, bool readAll);

        int TotalRecords(string filePath, RecordType type);
    }
}
