using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation
{
    class Engine
    {
        //engine inertiaI
        const double Inertia = 10;

        const double OverheatingTemperature = 110;

        //Коэффициент зависимости скорости нагрева от крутящего момента
        const double Hm = 0.01;

        //Коэффициент зависимости скорости нагрева от скорости вращения коленвала 
        const double Hv = 0.0001;

        //Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
        const double C = 0.1;

        //Температура двигателя
        double EngineTemperature=0;

        //Температура снаружи
        double TOutside;

        //время в секундах , очевидно
        double TimeInSeconds=0;

        //ускорение 
        double Acceleration=0;
        
        //скорость вращения коленвала  
        double CrankshaftRotationSpeed=0;
        
        //крутящий момент двигателя 
        double EngineTorque=20;

        private int[] MArray = null;
        private int[] VArray = null;

       public Engine()
        {
            
            //int ArraySize = 6;

            //MArray = new int[ArraySize];
            //VArray= new int[ArraySize];

        }
        double Linear( int LeftBorder,double x)
        {

            double x0 = VArray[LeftBorder];
            double y0= MArray[LeftBorder];
            double x1= VArray[LeftBorder+1];
            double y1= MArray[LeftBorder+1];

            return ((x-x0)*(y1-y0)/(x1-x0))+y0;
            

        }

        public double RunEnigneSimulation(double OutsideTemperature)
        {
            if (OutsideTemperature > -150 && OutsideTemperature < 200)
            {
                double DeltaTemperature = 0;
                double PreviousTemperature = 0;
                double Epsilon = 0.00000001;


                TOutside = OutsideTemperature;
                EngineTemperature = TOutside;

                
                //начальная скорость
                CrankshaftRotationSpeed = 0;

                //engine  simulation func
                while (true)
                {
                    CalculateAcceleration();
                    
                    CrankshaftRotationSpeed += Acceleration; // ??????????????????????????


                    int LeftBorder = GetLeftBorderForInterpolation(CrankshaftRotationSpeed);
                    
                    //EngineTorque = Math.Round(Linear(LeftBorder, CrankshaftRotationSpeed),4);
                    EngineTorque = Linear(LeftBorder, CrankshaftRotationSpeed);

                    //Console.Write($"Cooling speed= {Math.Round(CalculateCoolingSpeed(), 4)}  HeatingSpeed= {Math.Round(CalculateHeatingSpeed(), 4)} \n");

                    //EngineTemperature += Math.Round(CalculateCoolingSpeed(),4)  + Math.Round(CalculateHeatingSpeed(), 4) ;

                    
                    EngineTemperature += CalculateCoolingSpeed() +CalculateHeatingSpeed() ;

                    if(EngineTemperature-PreviousTemperature<Epsilon)
                    {
                        throw new Exception("The engine will not overheat");
                    }
                    else
                    {
                        PreviousTemperature = EngineTemperature;
                    }

                    //Console.WriteLine($"Temperature={EngineTemperature} V= {CrankshaftRotationSpeed} " + $"M= {EngineTorque}  Time={TimeInSeconds}");
                   // Console.WriteLine("\n----------------------------- \n");

                     
                    

                    if(EngineTemperature>=OverheatingTemperature)
                    {
                        return TimeInSeconds;
                    }


                    TimeInSeconds++;
                }

                throw new NotFiniteNumberException();
            }
            else
            {
                throw new Exception("Extreme temperature given");
            }
        }

        private int GetLeftBorderForInterpolation(double crankshaftRotationSpeed)
        {
            for (int i = 0; i < VArray.Length-1; i++)
            {
                if(VArray[i]<=crankshaftRotationSpeed && crankshaftRotationSpeed<=VArray[i+1])
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException();
        }

        private void CalculateAcceleration()
        {
            Acceleration =/*M /  */ EngineTorque / Inertia;
        }

        private double CalculateHeatingSpeed()
        {
            double HeatingSpeed = 0;

            HeatingSpeed = EngineTorque* Hm +Math.Pow(CrankshaftRotationSpeed ,2) * Hv;

            return HeatingSpeed;
        
        }

        private double CalculateCoolingSpeed()
        {
            double CoolingSpeed = 0;

            CoolingSpeed = C * (TOutside - EngineTemperature);

            return CoolingSpeed;
        }

        public void FillMArray(int [] RightMArray)
        {
            if (RightMArray.Length<=0)
            {
                throw new Exception("Zero length array given");
            }

            MArray = new int[RightMArray.Length];

            for (int i = 0; i < RightMArray.Length; i++)
            {
                MArray[i] = RightMArray[i];
            }
        }

        public void FillVArray(int [] RightVArray)
        {
            if (RightVArray.Length <= 0)
            {
                throw new Exception("Zero length array given");
            }

            VArray = new int[RightVArray.Length];

            for (int i = 0; i < RightVArray.Length; i++)
            {
                VArray[i] = RightVArray[i];
            }

        }
    }
}
