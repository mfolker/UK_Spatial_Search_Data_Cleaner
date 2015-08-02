using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Services.Data;

namespace UKSSDC.Services.Import
{
    public class CsvReader : IPlaceReader, IPostcodeReader, IRegionReader, IRoadReader
    {
        private readonly IUnitOfWork _unitOfWork;

        public CsvReader (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        List<PlaceRecord> IPlaceReader.Read (string filePath, int progress)
        {

            return null; //Placeholder
        }

        List<PostcodeRecord> IPostcodeReader.Read(string filePath, int progress)
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

        int IRoadReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
        }

        int IRegionReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
        }

        int IPostcodeReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
        }

        int IPlaceReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
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
