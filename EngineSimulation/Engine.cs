//-- Copyright 2020 avbuka; all commercial usage is strictly prohibited.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation
{
    class Engine
    {
        //engine inertia
        private const double Inertia = 10;

        private const double OverheatingTemperature = 110;

        //Коэффициент зависимости скорости нагрева от крутящего момента
        private const double Hm = 0.01;

        //Коэффициент зависимости скорости нагрева от скорости вращения коленвала 
        private const double Hv = 0.0001;

        //Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
        private const double C = 0.1;

        //Температура двигателя
        private double EngineTemperature =0;

        //Температура снаружи
        private double TOutside;

        //время в секундах , очевидно
        private double TimeInSeconds =0;

        //ускорение 
        private double Acceleration =0;

        //скорость вращения коленвала  
        private double CrankshaftRotationSpeed =0;

        //крутящий момент двигателя 
        private double EngineTorque =20;

        private int[] MArray = null;
        private int[] VArray = null;

       public Engine()
        {
        }
        public override string ToString()
        {
            string ReturnString =string.Empty;

            ReturnString += "Inertia = " + Inertia +"\n";

            if(MArray!=null)
            {
                ReturnString += "M = {";
                
                for (int i = 0; i < MArray.Length; i++)
                {
                    ReturnString += " " + MArray[i];

                    if(i<MArray.Length-1)
                    {
                        ReturnString += ",";
                    }
                }

                ReturnString += "} \n";
            }
            
            if(VArray!=null)
            {
                ReturnString += "V = {";
                
                for (int i = 0; i < VArray.Length; i++)
                {
                    ReturnString += " " + VArray[i];

                    if (i < VArray.Length - 1)
                    {
                        ReturnString += ",";
                    }
                }

                ReturnString += "} \n";
            }

            ReturnString += "Temperature of overheating = " + OverheatingTemperature + "\n";
            ReturnString += "Hm = " + Hm + "\n";
            ReturnString += "Hv = " + Hv + "\n";
            ReturnString += "C = " + C + "\n";


            return ReturnString;
        }

        //линейная интерполяция для получения крутящего момента
        double Linear( int LeftBorder,double x)
        {

            double x0 = VArray[LeftBorder];
            double y0= MArray[LeftBorder];
            double x1= VArray[LeftBorder+1];
            double y1= MArray[LeftBorder+1];

            return ((x-x0)*(y1-y0)/(x1-x0))+y0;        
        }

        public double RunEngineSimulation(double OutsideTemperature)
        {
            if (OutsideTemperature > -150 && OutsideTemperature < 110)
            {
                //перемення для выхода если двиг не перегревается
                double Epsilon = 0.00000001;

                double PreviousTemperature = 0;


                TOutside = OutsideTemperature;
                EngineTemperature = TOutside;

                
                //начальная скорость
                CrankshaftRotationSpeed = 0;

                //engine  simulation func
                while (true)
                {
                    CalculateAcceleration();
                    
                    CrankshaftRotationSpeed += Acceleration; 


                    //находим левую границу для послед. интерполяции
                    int LeftBorder = GetLeftBorderForInterpolation(CrankshaftRotationSpeed);

                    EngineTorque = Linear(LeftBorder, CrankshaftRotationSpeed); 

                    EngineTemperature += CalculateCoolingSpeed() +CalculateHeatingSpeed() ;

                    //если прирост меньше Е, то двиг не перегреется
                    if(EngineTemperature-PreviousTemperature<Epsilon)
                    {
                        TimeInSeconds = -1;
                        return TimeInSeconds ;
                    }
                    else
                    {
                        PreviousTemperature = EngineTemperature;
                    }

           

                    if(EngineTemperature>=OverheatingTemperature)
                    {
                        return TimeInSeconds;
                    }


                    TimeInSeconds++;
                }
             
            }
            else
            {
                throw new Exception($"Error: Extreme temperature given ({OutsideTemperature} C).\n");
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
            Acceleration = EngineTorque / Inertia;
        }

        private double CalculateHeatingSpeed()
        {
            return EngineTorque * Hm + Math.Pow(CrankshaftRotationSpeed, 2) * Hv;
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
                throw new Exception("Zero length array given.");
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
                throw new Exception("Zero length array given.");
            }

            VArray = new int[RightVArray.Length];

            for (int i = 0; i < RightVArray.Length; i++)
            {
                VArray[i] = RightVArray[i];
            }

        }
    }
}
