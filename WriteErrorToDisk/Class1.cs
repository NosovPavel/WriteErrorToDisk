using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;

namespace WriteErrorToDisk
{
    public class ErrorWriter
    {
        public static void WriteError(DateTime dt, ApplicationUnhandledExceptionEventArgs e)
        {
            string addedTime = dt.ToString().Replace("/", "_");
            addedTime = addedTime.Replace(" ", "_");
            addedTime = addedTime.Replace(":", "_");

            string innerex = "";
            if (e.ExceptionObject.InnerException != null)
            {
                innerex = e.ExceptionObject.InnerException.ToString();
            }
            string error = e.ExceptionObject + "/n/n" + "/n ExceptionObject.Message = " + e.ExceptionObject.Message + "/n e.ExceptionObject.InnerException =" + innerex;

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("Error_" + addedTime + ".xml", FileMode.CreateNew, storage))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(error);
                    }
                }
            }
        }
    }
}
