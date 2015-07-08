using System.Collections.Generic;
using UKSSDC.Models;
using UKSSDC.Models.Enums;

namespace UKSSDC.Services.Import
{
    public interface IProgressReporter
    {

        List<ImportProgress> Report(RecordType recordType);

        bool Initialise(string path); 
    }
}
