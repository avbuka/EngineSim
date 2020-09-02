//-- Copyright 2020 avbuka; all commercial usage is strictly prohibited.

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

            double Time = 0;

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
                                Console.Clear();
                                Console.Write("Please enter the outside temperature and press \"Enter\" \n");
                                Console.Write("Temperature in C: ");

                                LineString = string.Empty;

                                LineString = Console.ReadLine();

                                if (LineString.Length >= 1)
                                {

                                    if (double.TryParse(LineString, out double OutsideTemperature))
                                    {

                                        try
                                        {
                                            Time= IEngine.RunEngшneSimulation(OutsideTemperature);

                                           

                                            
                                            PrintResult(OutsideTemperature, Time);
                                            break;

                                        }
                                        catch (Exception e)
                                        {

                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Press any key to go back.\n");
                                            Console.ReadKey();
                                            break;

                                        }


                                    }

                                }
                                
                                    Console.WriteLine("Wrong input, try again. \nPress any key to go back.");
                                    Console.ReadKey();

                                
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
                            {
                                Console.WriteLine("Wrong input, try again. \nPress any key to go back.");
                                Console.ReadKey();

                                break;

                            }
                    }
                }
                else
                {
                    
                }
            }
            

        }

        private static void PrintResult(double OutsideTemperature ,double Time)
        {
            Console.Clear();

            string OutputString = $"Given the outside temperature ({OutsideTemperature})";

            if(Time>0)
            {
                OutputString += $" the engine will overheat in {Time} seconds.";
            }
            else
            {
                OutputString += $" the engine will not overheat.";
            }


            Console.WriteLine(OutputString);
            Console.WriteLine("Press any key to continue ");
            Console.ReadKey();
        }

        static void PrintMenu()
        {
            Console.Clear();

            string MenuString = "Welcome to the ignition engine simulation program by Andrei Tarakanov. \n";
            
            MenuString += "Choose one option from below followed by \"Enter\" \n\n";
            MenuString += "1) Run Engine Test \n";
            MenuString += "q) Exit \n";

          
            Console.Write(MenuString);
        }

      
    }
}
