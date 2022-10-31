using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiFiTracker.MathLogic
{
    public class Trilateration
    {
        public static double[] Compute(Point p1, Point p2, Point p3)
        {
            double[] a = new double[3];
            double[] b = new double[3];
            double c, d, f, g, h;
            double[] i = new double[2];
            double k;
            c = p2.glt() - p1.glt();
            d = p2.gln() - p1.gln();
            f = (180 / Math.PI) * Math.Acos(Math.Abs(c) / Math.Abs(Math.Sqrt(Math.Pow(c, 2) + Math.Pow(d, 2))));
            if ((c > 0 && d > 0)) { f = 360 - f; }
            else if ((c < 0 && d > 0)) { f = 180 + f; }
            else if ((c < 0 && d < 0)) { f = 180 - f; }
            a = C(c, d, B(A(D(p2.gr()))), f);
            b = C(p3.glt() - p1.glt(), p3.gln() - p1.gln(), B(A(D(p3.gr()))), f);
            g = (Math.Pow(B(A(D(p1.gr()))), 2) - Math.Pow(a[2], 2) + Math.Pow(a[0], 2)) / (2 * a[0]);
            h = (Math.Pow(B(A(D(p1.gr()))), 2) - Math.Pow(b[2], 2) - Math.Pow(g, 2) + Math.Pow(g - b[0], 2) + Math.Pow(b[1], 2)) / (2 * b[1]);
            i = C(g, h, 0, -f);
            i[0] = i[0] + p1.glt() - 0.086;
            i[1] = i[1] + p1.gln() - 0.004;
            k = E(i[0], i[1], p1.glt(), p1.gln());
            if (k > p1.gr() * 2) { i = null; }
            else
            {
                if (i[0] < -90 || i[0] > 90 || i[1] < -180 || i[1] > 180) { i = null; }
            }
            return i;
        }

        public static Point getTrilateration(Point position1, Point position2, Point position3)
        {


            double x1 = position1.glt();
            double y1 = position1.gln();
            double x2 = position2.glt();
            double y2 = position2.gln();
            double x3 = position3.glt();
            double y3 = position3.gln();

            double r1 = position1.gr();
            double r2 = position2.gr();
            double r3 = position3.gr();

            double S = (Math.Pow(x3, 2.0) - Math.Pow(x2, 2.0) + Math.Pow(y3, 2.0) - Math.Pow(y2, 2.0) + Math.Pow(r2, 2.0) - Math.Pow(r3, 2.0)) / 2.0;
            double T = (Math.Pow(x1, 2.0) - Math.Pow(x2, 2.0) + Math.Pow(y1, 2.0) - Math.Pow(y2, 2.0) + Math.Pow(r2, 2.0) - Math.Pow(r1, 2.0)) / 2.0;

            double y = ((T * (x2 - x3)) - (S * (x2 - x1))) / (((y1 - y2) * (x2 - x3)) - ((y3 - y2) * (x2 - x1)));
            double x = ((y * (y1 - y2)) - T) / (x2 - x1);

            Point userLocation = new Point(x, y, 0);
            return userLocation;

        }

    private static double A(double a) { return a * 7.2; }
        private static double B(double a) { return a / 900000; }
        private static double[] C(double a, double b, double c, double d) { return new double[] { a * Math.Cos((Math.PI / 180) * d) - b * Math.Sin((Math.PI / 180) * d), a * Math.Sin((Math.PI / 180) * d) + b * Math.Cos((Math.PI / 180) * d), c }; }
        private static double D(double a) { return 730.24198315 + 52.33325511 * a + 1.35152407 * Math.Pow(a, 2) + 0.01481265 * Math.Pow(a, 3) + 0.00005900 * Math.Pow(a, 4) + 0.00541703 * 180; }
        private static double E(double a, double b, double c, double d) { double e = Math.PI, f = e * a / 180, g = e * c / 180, h = b - d, i = e * h / 180, j = Math.Sin(f) * Math.Sin(g) + Math.Cos(f) * Math.Cos(g) * Math.Cos(i); if (j > 1) { j = 1; } j = Math.Acos(j); j = j * 180 / e; j = j * 60 * 1.1515; j = j * 1.609344; return j; }
    }

}
