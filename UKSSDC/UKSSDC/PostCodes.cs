using System;

namespace UKSSDC
{
    public class Postcodes : IRecord
    {
        public static bool CheckComplete()
        {
            return true;
        }

        //This type of data requires conversion between easting northings and lat longs

        internal static void Start()
        {
            throw new NotImplementedException();
        }

        public bool Run()
        {
            throw new NotImplementedException();
        }
    }
}