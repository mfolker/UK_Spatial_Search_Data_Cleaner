using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKSSDC.Services.Import
{
    public class CsvReader : IPlaceReader, IPostCodeReader, IRegionReader, IRoadReader
    {
        public CsvReader ()
	    {
                
	    }

        List<PlaceRecord> IPlaceReader.Read (string filePath, int progress)
        {


            return null; //Placeholder
        }

        List<PostCodeRecord> IPostCodeReader.Read(string filePath, int progress)
        {
            return null; //Placeholder
        }

        List<RegionRecord> IRegionReader.Read(string filePath, int progress)
        {
            return null; //Placeholder
        }

        List<RoadRecord> IRoadReader.Read(string filePath, int progress)
        {
            return null; //Placeholder
        }
    }

    public class PlaceRecord
    {
    }

    public class PostCodeRecord
    {
    }

    public class RegionRecord
    {
    }

    public class RoadRecord
    {
    }
}
