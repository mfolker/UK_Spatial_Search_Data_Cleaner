using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKSSDC.Services.Import
{
    interface IPostCodeReader
    {
        List<PostCodeRecord> Read(string filePath, int progress);
    }
}
