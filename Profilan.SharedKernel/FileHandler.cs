using Microsoft.Office.Core;
using System;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Profilan.SharedKernel
{
    public static class FileHandler
    {
        public static bool ConvertPPTToMP4(string inputFile, string outputVideo)
        {
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

                    return true;
                }
                catch (Exception e)
                {
                    var temp = e;
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
                var temp = e;
                 return false;
            }
        }
    }
}
