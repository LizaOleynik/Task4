﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_v2
{
    public interface ILoader
    {
        bool Crash { get; set; }
        void Move();
    }
}
