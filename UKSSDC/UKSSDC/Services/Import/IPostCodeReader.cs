﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKSSDC.Services.Import
{
    interface IPostcodeReader : IDependency
    {
        List<PostcodeRecord> Read(string filePath, int progress);

        int TotalRecords(string filePath);
    }
}
