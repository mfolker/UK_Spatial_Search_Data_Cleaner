using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKSSDC.Services.Import
{
    interface IPlaceReader
    {
        void ReadNext(int quantity, int progress)
    }
}
