using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_v2
{
    public class Mechanic
    {
        int working_time;
        int wheel;
        Random rnd = new Random();

        public Mechanic()
        {
            working_time = rnd.Next(3000, 5000);
            wheel = rnd.Next(10000, 20000);
        }

        public void Stopping(int x, int y)
        {

        }
    }
}
