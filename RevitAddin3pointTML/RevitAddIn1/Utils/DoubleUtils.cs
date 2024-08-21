﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddIn1.Utils
{
    public static class DoubleUtils
    {
        public static double FeetToMeet(this double feet) => feet * 0.3048;
        public static double FeetToMeet(this int feet) => feet * 0.3048;
        public static double MeetToFeet(this double met) => met * 3.280840;
        public static double MeetToFeet(this int met) => met * 3.280840;

        public static double MmToFeet(this double met) => met * 3.280840 / 1000;
        public static double MmToFeet(this int met) => met * 3.280840 / 1000.0;

        public static double FeetToMm(this double feet) => feet * 0.304800 * 1000;
        public static double FeetToMm(this int feet) => feet * 0.304800 * 1000;
    }
}
