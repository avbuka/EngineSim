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

        const double TOfOverheating = 110;

        //Коэффициент зависимости скорости нагрева от крутящего момента
        const double Hm = 0.01;

        //Коэффициент зависимости скорости нагрева от скорости вращения коленвала 
        const double Hv = 0.0001;

        //Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
        const double C = 0.1;

        //Температура двигателя
        double TOfEngine;

        //Температура снаружи
        double TOutside;

        private int[] MArray = null;
        private int[] VArray = null;

       public Engine()
        {
            
            //int ArraySize = 6;

            //MArray = new int[ArraySize];
            //VArray= new int[ArraySize];

        }

        public double RunEnigneSimulation(double OutsideTemperature)
        {
            throw new NotFiniteNumberException();
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
