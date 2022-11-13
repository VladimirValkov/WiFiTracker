using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace WiFiTracker.MathLogic
{
    public static class NonlinearLeastSquares
    {
        const string RpathWin = @"E:\Rlang\R-4.2.2\bin\x64\R.exe";
        const string RpathLinux = "/usr/bin/R";

        public static Coords Calculate(List<Coords> data)
        {
            var Rexe = RpathWin;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Rexe = RpathLinux;
            }

            var Rscript = $@"library(geosphere)

locations <- data.frame(
    latitude = c(
        {string.Join(',', data.Select(a=>a.latitude.ToString().Replace(',', '.')))}
    ),
    longitude = c(
        {string.Join(',', data.Select(a => a.longitude.ToString().Replace(',', '.')))}
    ),
    distance = c(
        {string.Join(',', data.Select(a => a.distance))}
    )
)

# Use average as the starting point
fit <- nls(
    distance ~ distm(
        data.frame(longitude, latitude),
        c(fitLongitude, fitLatitude)
    ),
    data = locations,
    start = list(
        fitLongitude=mean(locations$longitude),
        fitLatitude=mean(locations$latitude)
    ),
    control = list(maxiter = 1000, tol = 1e-02)
)

# Result
latitude <- summary(fit)$coefficients[2]
longitude <- summary(fit)$coefficients[1]

paste(latitude, longitude, sep=',')";

            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, Rscript);

            var Rprocess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Rexe,
                    Arguments = $"--slave --vanilla --file={tempFile}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            var result = new Coords();
            Rprocess.Start();
            while (!Rprocess.StandardOutput.EndOfStream)
            {
                string line = Rprocess.StandardOutput.ReadLine();
                //[1] "59.4262603475089,24.7252213758825"
                if (line.StartsWith("[1] \""))
                {
                    var splitted_line = line.Replace("[1]", "").Replace("\"","").Trim().Split(',');
                    char seperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    result.latitude = Convert.ToDouble(splitted_line[0].Replace('.', seperator));
                    result.longitude = Convert.ToDouble(splitted_line[1].Replace('.', seperator));
                    break;
                }
            }
            Rprocess.WaitForExit();
            File.Delete(tempFile);

            return result;
        }
        public class Coords
        {
            public double longitude { get; set; }
            public double latitude { get; set; }
            public int distance { get; set; }
        }
    }
}
