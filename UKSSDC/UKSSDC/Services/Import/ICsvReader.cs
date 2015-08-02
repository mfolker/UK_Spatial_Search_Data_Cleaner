using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKSSDC.Models;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.Import
{
    public interface ICsvReader : IDependency
    {
        List<PlaceRecord> Read(string filePath, int progress);

        int TotalRecords(string filePath, RecordType type);
    }
}
