using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulation
{
    class Program
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

        Dictionary<int, int> MDictionary = new Dictionary<int, int>();
        private Dictionary<int, int> VDictionary = new Dictionary<int, int>();


        void Main(string[] args)
        {
            SetUpDictionaries();

            Console.Write("Hello world");
            Console.ReadKey();


        }

        private void SetUpDictionaries()
        {
            //  { 0,75,150,200,250,300},{ 20,75,100,105,75,0}
            VDictionary.Add(20, 0);
            VDictionary.Add(75,75);
            VDictionary.Add(105,150);
            VDictionary.Add(75,250);
            VDictionary.Add(0,300);

            MDictionary.Add(0, 20);
            MDictionary.Add(75, 75);
            MDictionary.Add(150, 105);
            MDictionary.Add(250, 75);
            MDictionary.Add(300, 0);

        }
    }
}
