using Microsoft.Office.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Profilan.SharedKernel
{
    public static class FileHandler
    {
        public static bool ConvertVideoToMP4(string inputFile, string outputVideo)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = @"C:\Program Files\ffmpeg\bin\ffmpeg.exe";
                process.StartInfo.Arguments = "-i \"" + inputFile + "\" -r 30 \"" + outputVideo + "\"";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                var result = process.Start();
                process.WaitForExit();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool ConvertPPTToMP4(string inputFile, string outputVideo, EventLog eventLog)
        {
            eventLog.WriteEntry("Converting PowerPoint " + outputVideo + " started", System.Diagnostics.EventLogEntryType.Information, 1005);

            try
            {
                var objApp = new PowerPoint.Application();
                var objPres = objApp.Presentations.Open(inputFile, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                System.Threading.Thread.Sleep(180);
                objPres.UpdateLinks();
                try
                {
                    objPres.UpdateLinks();
                    objPres.CreateVideo(outputVideo, false, 10, 1080);
                    // objPres.SaveAs(inputFile, PowerPoint.PpSaveAsFileType.ppSaveAsJPG);

                    while (objPres.CreateVideoStatus == PowerPoint.PpMediaTaskStatus.ppMediaTaskStatusInProgress || objPres.CreateVideoStatus == PowerPoint.PpMediaTaskStatus.ppMediaTaskStatusQueued)
                    {
                        System.Threading.Thread.Sleep(10000);
                    }

                    eventLog.WriteEntry("Converting PowerPoint " + outputVideo + " ended", System.Diagnostics.EventLogEntryType.Information, 1006);


                    return true;
                }
                catch (Exception e)
                {
                    eventLog.WriteEntry("EyeBoard Task error: " + e.StackTrace, System.Diagnostics.EventLogEntryType.Error, 1002);
                    throw new Exception(e.Message);
                }
                finally
                {
                    objPres.Close();
                    objApp.Quit();
                }
            }
            catch (Exception e)
            {
                eventLog.WriteEntry("EyeBoard Task error: " + e.StackTrace, System.Diagnostics.EventLogEntryType.Error, 1002);

                throw new Exception(e.Message);
            }
        }

        public static bool Save(string path, string physicalPath)
        {
            try
            {
                File.Copy(path, physicalPath);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
