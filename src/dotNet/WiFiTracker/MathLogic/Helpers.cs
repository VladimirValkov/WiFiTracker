using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker
{
    public static class Helpers
    {
        public static double calculateDistanceOld(double signalLevelInDb, double freqInMHz)
        {
            double exp = (27.55 - (20 * Math.Log10(freqInMHz)) + Math.Abs(signalLevelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }


        public static double calculateDistance(double signalLevelInDb, double freqInMHz)
        {
            double k = 32.44;
            double Ptx = 16;
            double Cltx = 0;
            double Agtx = 2;
            double Agrx = 0;
            double Clrx = 0;
            double Prx = signalLevelInDb;
            double FM = 22;
            double fspl = Ptx - Cltx + Agtx + Agrx - Clrx - Prx - FM;

            double exp = (fspl - k - 20 * (Math.Log10(freqInMHz))) / 20;
            return  (Math.Pow(10.0, exp));
        }

    }
}
