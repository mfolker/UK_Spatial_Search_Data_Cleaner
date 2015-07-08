﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKSSDC.Services.Import
{
    interface IRoadReader
    {
        List<RoadRecord> Read(string filePath, int progress);
    }
}