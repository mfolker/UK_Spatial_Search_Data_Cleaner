using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKSSDC.Services.Data;

namespace UKSSDC.Services.Import
{
    public class CsvReader : IPlaceReader, IPostCodeReader, IRegionReader, IRoadReader
    {
        private readonly UnitOfWork _unitOfWork;

        public CsvReader (UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        int IRoadReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
        }

        int IRegionReader.TotalRecords(string filePath)
        {
            throw new NotImplementedException();
        }

        int IPostCodeReader.TotalRecords(string filePath)
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
