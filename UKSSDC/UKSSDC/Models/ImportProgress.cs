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
        public RecordType RecordType { get; private set; }

        public string FileName { get; private set; }

        public string RecordNumber { get; private set; }

        public bool Complete { get; private set; }
    }
}
