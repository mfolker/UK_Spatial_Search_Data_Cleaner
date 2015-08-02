using System;

namespace UKSSDC.Models
{
    public abstract class Common
    {
        public int Id { get; private set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }
    }
}
