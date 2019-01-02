using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_v2
{
    public class Accident : ILoader
    {
        public int startPointY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        //public double Speed { get; set; }
        public bool Crash { get; set; }
        public Car car { get; set; }

        public void Move()
        {
            Y -= 1/*(int)(Speed * 10)*/;
        }

        public void MoveBack()
        {
            Y += 1/*(int)(Speed * 10)*/;
        }
        //public delegate void Loader();
        //public event Loader CrashSituation;
        //int speed;
        //int X;
        //int Y;
        //bool Check;

        //public Accident(int height)
        //{
        //    speed = 30;
        //    X = 30;
        //    Y = height - 60;
        //    Check = false;
        //    //CrashSituation = Ride;
        //}

        //public double Probability()
        //{
        //    Random rnd = new Random();
        //    return rnd.NextDouble();
        //}

        //public void Crash()
        //{
        //    if (Probability() > 0.9)
        //    {
        //        CrashSituation();
        //    }
        //}
        //void OnWay()
        //{
        //    Check = true;
        //}

    }
}
