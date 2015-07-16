using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using UKSSDC.Services.Data;


namespace UKSSDC
{
    class Program
    {
        static void Main(string[] args)
        {

            


            Console.WriteLine("Starting to build your UK Spatial Search Database");

            Places places = new Places();

            if (!places.CheckComplete())
            {
                //Start or carry on importing places

                places.Start();
            }
            else
            {
                //TODO: Output total number of places imported.

                places.Complete();

            }


        }

    }
}
