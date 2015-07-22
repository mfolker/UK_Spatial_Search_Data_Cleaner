using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UKSSDC.Models;

namespace UKSSDC.Services.Import
{
    public interface IPlaceReader : IDependency
    {
        List<PlaceRecord> Read(string filePath, int progress);

        int TotalRecords(string filePath);
    }
}
