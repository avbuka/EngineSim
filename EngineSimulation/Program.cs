using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation
{
    class Program
    {
    


        static void Main(string[] args)
        {

            // M array
            int[] MArray = new int[6];

            MArray[0] = 20;
            MArray[1] = 75;
            MArray[2] = 100;
            MArray[3] = 105;
            MArray[4] = 75;
            MArray[5] = 0;
            
            // V array
            int[] VArray = new int[6];

            VArray[0] = 0;
            VArray[1] = 75;
            VArray[2] = 150;
            VArray[3] = 200;
            VArray[4] = 250;
            VArray[5] = 300;
            
            
            string LineString;

            Engine IEngine = new Engine();

            IEngine.FillMArray(MArray);
            IEngine.FillVArray(VArray);

            
            
            

            while(true)
            {
                PrintMenu();

                LineString=Console.ReadLine();

                if(LineString.Length==1)
                {
                    switch (LineString)
                    {
                        case "1":
                            {
                                break;
                            }

                        case "2":
                            {
                                break;
                            }
                        case "3":
                            {
                                break;
                            }

                        case "q":
                            {
                                return;
                                
                            }


                        default:
                            break;
                    }
                }
            }
            

        }

         static void PrintMenu()
        {
            string MenuString = "1) Run Engine Test \n";

            MenuString += "2) Placeholder \n";

            Console.Write(MenuString);
        }

      
    }
}
