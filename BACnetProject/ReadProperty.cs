using System;
using System.Diagnostics;
using System.Text;

//Modifed from Code provided by StackOverflow and Microsoft
//https://stackoverflow.com/questions/206323/how-to-execute-command-line-in-c-get-std-out-results
//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=net-5.0

namespace BACnetProject
{
    abstract public class ReadProperty
    {

        private string ToolFileName = "bacrp";
        public string objectDevice { get; set; }
        public string objectNumber { get; set; }
        public string objectInstance { get; set; }
        public string objectProperty { get; set; }

        //If  you are going to use System.Diagnostics.myprocess in a class, you must do it this way
        public string Output()
        {
            var stdOut = new StringBuilder();

            using (Process myprocess = new Process())
            {

                myprocess.StartInfo.FileName = ToolFileName;
                myprocess.StartInfo.ArgumentList.Add(objectDevice);
                myprocess.StartInfo.ArgumentList.Add(objectNumber);
                myprocess.StartInfo.ArgumentList.Add(objectInstance);
                myprocess.StartInfo.ArgumentList.Add(objectProperty);
                myprocess.StartInfo.CreateNoWindow = true;
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.StartInfo.RedirectStandardError = true;
                myprocess.StartInfo.RedirectStandardOutput = true;
                myprocess.OutputDataReceived += (sender, args) => stdOut.AppendLine(args.Data); // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

                string stdError = null;

                try
                {
                    myprocess.Start();
                    myprocess.BeginOutputReadLine();
                    stdError = myprocess.StandardError.ReadToEnd();
                    myprocess.WaitForExit();
                }
                catch (Exception e)
                {
                    throw new Exception("OS error while executing: " + this.ToolFileName + e.Message, e);
                }

                if (myprocess.ExitCode == 0)
                {
                    return stdOut.ToString();
                }
                else
                {
                    var message = new StringBuilder();

                    if (!string.IsNullOrEmpty(stdError))
                    {
                        message.AppendLine(stdError);
                    }

                    if (stdOut.Length != 0)
                    {
                        message.AppendLine("Std output:");
                        message.AppendLine(stdOut.ToString());
                    }

                    throw new Exception(" finished with exit code = " + myprocess.ExitCode + ": " + message);
                
                }


            }


        }

    }
}