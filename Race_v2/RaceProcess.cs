using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_v2
{
    public class RaceProcess
    {
        public int state;   //  1 - гонка не началась, 2 - начало, 3 - в процессе, 4 - финиш, 5 - авария
        public delegate void RaceProcessStateHandler();
        public RaceProcessStateHandler ChangeState;

        //public RaceProcess()
        //{
        //    ChangeState = State;
        //}

        public void Change()
        {
            ChangeState();
        }

        //public void State()
        //{
        //    state = !state;
        //}
    }
}
