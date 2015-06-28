using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKSSDC.Models
{
    public abstract class Common
    {
        public int id { get; private set; }

        public DateTime created { get; private set; }

        public DateTime updated { get; private set; }
    }
}
