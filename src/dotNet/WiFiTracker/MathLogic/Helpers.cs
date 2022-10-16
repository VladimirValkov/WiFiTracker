using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker
{
    public static class Helpers
    {
        public static double calculateDistance(double signalLevelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + Math.Abs(signalLevelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }
    }
}
