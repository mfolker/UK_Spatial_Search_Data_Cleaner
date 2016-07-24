using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UKSSDC.Models;
using UKSSDC.Services.Data;

namespace UKSSDC
{
    class PostcodePerimeters
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostcodePerimeters(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            Console.WriteLine("Postcode Perimeters");

            //NOTE: The implementation below is a stop gap until a convex hull wrapping algorithum can be produced.

            IQueryable<string> outwardCodes = _unitOfWork.Postcodes.Select(x => x.OutwardCode).Distinct();

            Parallel.ForEach(outwardCodes, (outwardCode) =>
            {
                Console.WriteLine(outwardCode);

                try
                {
                    PostcodePerimeter perimeter = new PostcodePerimeter
                    {
                        OutwardCode = outwardCode,
                        Perimeter = null
                    };

                    lock (_unitOfWork)
                    {
                        _unitOfWork.PostcodePerimeters.Add(perimeter);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following record could not be added as a postcode perimeter:");
                    Console.WriteLine(outwardCode);
                }
            });

            _unitOfWork.SaveChanges();

            double percentComplete = (_unitOfWork.PostcodePerimeters.Count() / outwardCodes.Count()) / 100;
            string result = String.Format("{0}% Complete", percentComplete.ToString(CultureInfo.InvariantCulture));
            Console.WriteLine();

        }
    }
}
