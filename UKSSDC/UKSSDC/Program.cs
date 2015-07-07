using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

            //TODO: Import Postcodes

            if (!PostCodes.CheckComplete())
            {
                PostCodes.Start();
            }
            else
            {
                Console.WriteLine("All postcodes have been imported"); //todo: Refactor into function in class.

                //TODO: Check to see if the perimeters are complete.

                if (!PostCodePerimeters.CheckComplete())
                {
                    PostCodePerimeters.Start();
                }
                else
                {
                    Console.WriteLine("All post code perimeters have been built."); //TODO: Refactor into function in class.
                }
            }

            //TODO: Import Regions

            if (!Regions.CheckComplete())
            {
                
            }
            else
            {
                Console.WriteLine("All regions have been imported"); //TODO: Refactor into function in class.
            }

            //TODO: Import Roads

            if (!Regions.CheckComplete())
            {
                Console.WriteLine("All roads have been imported"); //TODO: Refactor into function in class.
            }
            
            //TODO: Add success message. 
        }
    }
}
