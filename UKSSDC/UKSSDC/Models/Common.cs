using System;

namespace UKSSDC.Models
{
    public abstract class Common
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
