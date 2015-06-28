using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Models.Enums;

namespace UKSSDC.Models
{
    public class ImportProgress
    {
        public RecordType recordType { get; private set; }

        public string fileName { get; private set; }

        public string recordNumber { get; private set; }

        public bool complete { get; private set; }
    }
}
