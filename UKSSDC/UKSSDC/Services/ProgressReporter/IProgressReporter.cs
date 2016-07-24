using System.Collections.Generic;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Autofac;

namespace UKSSDC.Services.ProgressReporter
{
    public interface IProgressReporter : IDependency
    {

        List<ImportProgress> Report(RecordType recordType);

        bool Initialise(string path);

    }
}
