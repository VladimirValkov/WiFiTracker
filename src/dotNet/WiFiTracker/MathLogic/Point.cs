using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.MathLogic
{
    public class Point
    {
        private double lt, ln, r;
        public Point(double lt, double ln, double r) { this.lt = lt; this.ln = ln; this.r = r; }
        public double glt() { return lt; }
        public double gln() { return ln; }
        public double gr() { return r; }
    }
}
