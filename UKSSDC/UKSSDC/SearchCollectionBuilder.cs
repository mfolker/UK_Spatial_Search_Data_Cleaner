using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Threading.Tasks;
using UKSSDC.Models;
using UKSSDC.Models.Enums;
using UKSSDC.Services.Data;

namespace UKSSDC
{
    public class SearchCollectionBuilder
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchCollectionBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            //TODO: Clean up this whole project using generics. 

            List<SpatialSearchObject> spatialSearchObjects = new List<SpatialSearchObject>();

            var places = _unitOfWork.Places.ToList();
            Parallel.ForEach(places, (place) =>
            {
                var spatialSeachObject = new SpatialSearchObject
                {
                    SpatialObject = place.Location,
                    Type = SpatialObjectType.Place,
                    Name = place.Name
                };

                lock (spatialSearchObjects)
                {
                     spatialSearchObjects.Add(spatialSeachObject);
                }
            });

            _unitOfWork.SpatialSearchObjects.AddRange(spatialSearchObjects);
            _unitOfWork.SaveChanges();
            spatialSearchObjects.Clear();

            long postcodesCount = _unitOfWork.Postcodes.LongCount();

            long postcodesProcessed = 0;

            do
            {
                Console.WriteLine(postcodesProcessed);
                int chunckSize = 50000;

                var postcodes = _unitOfWork.Postcodes.Where(x => x.Id > postcodesProcessed).Take(chunckSize);
                Parallel.ForEach(postcodes, (postcode) =>
                {
                    var spatialSeachObject = new SpatialSearchObject
                    {
                        SpatialObject = postcode.Location,
                        Type = SpatialObjectType.Postcode,
                        Name = postcode.FullPostcode
                    };

                    lock (spatialSearchObjects)
                    {
                        spatialSearchObjects.Add(spatialSeachObject);
                    }
                });

                _unitOfWork.SpatialSearchObjects.AddRange(spatialSearchObjects);
                _unitOfWork.SaveChanges();
                spatialSearchObjects.Clear();

                postcodesProcessed += chunckSize; 

            } while (postcodesProcessed < postcodesCount);


            var postcodePerimeters = _unitOfWork.PostcodePerimeters.ToList();
            Parallel.ForEach(postcodePerimeters, (postcode) =>
            {
                var spatialSeachObject = new SpatialSearchObject
                {
                    SpatialObject = postcode.Perimeter,
                    Type = SpatialObjectType.PostcodePerimeter,
                    Name = postcode.OutwardCode
                };

                lock (spatialSearchObjects)
                {
                    spatialSearchObjects.Add(spatialSeachObject);
                }
            });

            _unitOfWork.SpatialSearchObjects.AddRange(spatialSearchObjects);
            _unitOfWork.SaveChanges();
            spatialSearchObjects.Clear();

            var regions = _unitOfWork.Regions.ToList();
            Parallel.ForEach(regions, (region) =>
            {
                var spatialSeachObject = new SpatialSearchObject
                {
                    SpatialObject = region.Perimeter,
                    Type = SpatialObjectType.Region,
                    Name = region.Name
                };

                lock (spatialSearchObjects)
                {
                    spatialSearchObjects.Add(spatialSeachObject);
                }
            });

            _unitOfWork.SpatialSearchObjects.AddRange(spatialSearchObjects);
            _unitOfWork.SaveChanges();
        }
    }
}
