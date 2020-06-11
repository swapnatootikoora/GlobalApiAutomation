using System;
using System.IO;

namespace Common.ConfigUtils
{
    public static class Report
    {
        private static DateTime TestStartTime { get; } = DateTime.Now;

        public static void WriteToFile(string content)
        {
            if (Environment.GetEnvironmentVariable("HOST_ENVIRONMENT") != "OCTOPUS")
            {
                AutomationVariables AutomationVariables = AppSettingsInitialization.GetConfigInstance();
                Directory.CreateDirectory("./Log");
                var resultFileName = $"{Path.Combine("./Log", AutomationVariables.ReportName)}{TestStartTime:yyyy-MM-dd_HHmm}.txt";
                using (StreamWriter writer = new StreamWriter(resultFileName, true))
                {
                    writer.WriteLine(content);
                }
            }
            //Below code is used if you want to debug through Octopus
            //else
            //{
            //    Console.WriteLine(content);
            //}
        }
    }
}
