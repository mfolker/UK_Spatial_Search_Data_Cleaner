using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;

namespace UKSSDC.Services.Import
{
    public class CsvReader : ICsvReader
    {
        private readonly IUnitOfWork _unitOfWork;

        public CsvReader (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static String getFileLines(String path, int indexOfLine)
        {
            return File.ReadLines(path).ElementAtOrDefault(indexOfLine);
        }

        List<PlaceRecord> ICsvReader.Read (string filePath, int progress)
        {

            var PlacesRecords = getFileLines(filePath, progress); 



            return null; //Placeholder
        }

        //List<PostcodeRecord> IPostcodeReader.Read(string filePath, int progress)
        //{
        //    return null; //Placeholder
        //}

        //List<RegionRecord> IRegionReader.Read(string filePath, int progress)
        //{
        //    return null; //Placeholder
        //}

        //List<RoadRecord> IRoadReader.Read(string filePath, int progress)
        //{
        //    return null; //Placeholder
        //}

        //int IRoadReader.TotalRecords(string filePath)
        //{
        //    //Roads have one line at the top.

        //    int total = System.IO.File.ReadAllLines(filePath).Length;

        //    return total--; 
        //}

        //int IRegionReader.TotalRecords(string filePath)
        //{
        //    //Regions have 1 line at the top.

        //    int total = System.IO.File.ReadAllLines(filePath).Length;

        //    return total--; 
        //}

        //int IPostcodeReader.TotalRecords(string filePath)
        //{
        //    return System.IO.File.ReadAllLines(filePath).Length;
        //}

        int ICsvReader.TotalRecords(string filePath, RecordType type)
        {
            //Places have 1 line at the top
            int total = System.IO.File.ReadAllLines(filePath).Length;
            
            return type == RecordType.Postcode ? total : total--;
        }
    }

    public class PlaceRecord
    {
    }

    public class PostcodeRecord
    {
    }

    public class RegionRecord
    {
    }

    public class RoadRecord
    {
    }
}
