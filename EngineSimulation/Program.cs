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
            double[] MArray = new double[6];

            MArray[0] = 20;
            MArray[1] = 75;
            MArray[2] = 100;
            MArray[3] = 105;
            MArray[4] = 75;
            MArray[5] = 0;

            // V array
            double[] VArray = new double[6];

            VArray[0] = 0;
            VArray[1] = 75;
            VArray[2] = 150;
            VArray[3] = 200;
            VArray[4] = 250;
            VArray[5] = 300;
            
            
            string LineString;

            double Time = 0;
            double TempValue =0;
            int NumOfPoints = 6;

            Engine IEngine = new Engine();

            IEngine.FillMArray(MArray);
            IEngine.FillVArray(VArray);

            IEngine.Inertia = 10;
            IEngine.OverheatingTemperature = 110;
            IEngine.Hm = 0.01;
            IEngine.Hv = 0.0001;
            IEngine.C = 0.1;
         

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
                                Console.Write("Please enter the outside temperature and press \"Enter\". \n");
                                Console.Write("Temperature in C: ");

                                LineString = string.Empty;

                                LineString = Console.ReadLine();

                                if (LineString.Length >= 1)
                                {

                                    if (double.TryParse(LineString, out double OutsideTemperature))
                                    {

                                        try
                                        {
                                            Time= IEngine.RunEngineSimulation(OutsideTemperature);

                                            
                                           

                                            
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
                        case "2":
                            {
                                Console.Clear();
                                Console.WriteLine(IEngine.ToString());
                                Console.WriteLine("Press any key to go back.\n");
                                Console.ReadKey();
                                break;
                            }
                        case "3":
                            {
                                Console.Clear();
                                Console.WriteLine(IEngine.ToString());
                                Console.WriteLine("q) Exit \n");

                                Console.WriteLine("Enter letter(-s) you would like to edit and press Enter.");

                                LineString = Console.ReadLine();

                                switch (LineString)
                                {
                                    case "I":
                                        {

                                            TempValue = ReadNewSpec();

                                            if(TempValue!=double.MinValue)
                                                IEngine.Inertia = TempValue;
                                            continue;
                                        }
                                    case "Hm":
                                        {
                                            TempValue = ReadNewSpec();
                                            IEngine.Hm = TempValue;
                                            break;
                                        }
                                    case "Hv":
                                        {

                                            TempValue = ReadNewSpec();
                                            IEngine.Hv = TempValue;
                                            break;
                                        } 
                                    case "C":
                                        {

                                            TempValue = ReadNewSpec();
                                            IEngine.C = TempValue;
                                            break;
                                        }
                                         
                                    case "M":
                                        {
                                            double [] NewArray = new double[NumOfPoints];

                                            for (int i = 0; i < NumOfPoints; i++)
                                            {
                                                TempValue = ReadNewSpec();
                                                NewArray[i] = TempValue;
                                            }
                                            IEngine.FillMArray(NewArray);
                                            break;
                                        }
                                    case "V":
                                        {
                                            double [] NewArray = new double[NumOfPoints];

                                            for (int i = 0; i < NumOfPoints; i++)
                                            {
                                                Console.Write($"{i+1}) ");
                                                TempValue = ReadNewSpec();
                                                NewArray[i] = TempValue;
                                            }
                                            IEngine.FillVArray(NewArray);
                                            break;
                                        } 
                                    case "T":
                                        {
                                            TempValue = ReadNewSpec();
                                            IEngine.OverheatingTemperature = TempValue;
                                            break;
                                        }
                                        
                                    case "q":
                                        {
                                            break;
                                        }

                                    default:
                                        break;
                                }
                               // Console.ReadKey();
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

        private static double ReadNewSpec()
        {
            Console.Write("Enter new value: ");

            if (double.TryParse(Console.ReadLine(), out double Value))
            {
                return Value;
            }
            else
            {
                Console.WriteLine("Wrong input, press any key to go back.");
                Console.ReadKey();
                return double.MinValue;
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
            
            MenuString += "Choose one option from below followed by \"Enter\". \n\n";
            MenuString += "1) Run Engine Test \n";
            MenuString += "2) Print engine specs \n";
            MenuString += "3) Edit engine specs \n";
            MenuString += "q) Exit \n";

          
            Console.Write(MenuString);
        }

      
    }
}
