using System;

namespace UKSSDC
{
    static class Places
    {
        public static bool CheckComplete()
        {
            return true;
        }

        internal static void Start()
        {
            Progress(); 

        }

        private static void Progress()
        {
            throw new NotImplementedException();
        }

        internal static void Complete()
        {
            //TODO: Dry up? 

            Console.WriteLine("All places have been imported");
        }
    }
}