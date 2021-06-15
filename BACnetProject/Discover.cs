using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace BACnetProject
{
    public class Discover
    {
        private string ToolFileName = "bacwi";
       

        //Output is the base method.  Goes and runs bacrp with the above values and gets the result
        //into a string

        //If  you are going to use System.Diagnostics.myprocess in a class, you must do it this way
        public List<string> Output()
        {
            var stdOut = new StringBuilder();

            using (Process myprocess = new Process())
            {
                //First we fill up all the data for a Process to run

                myprocess.StartInfo.FileName = ToolFileName;
                myprocess.StartInfo.CreateNoWindow = true;
                myprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.StartInfo.RedirectStandardError = true;
                myprocess.StartInfo.RedirectStandardOutput = true;
                //the returned string, stdOut is built according to this line
                myprocess.OutputDataReceived += (sender, args) => stdOut.AppendLine(args.Data);
                // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

                string stdError = null;

                //We use a try/catch to run it safely
                //It's a process, not a one liner command
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
                    List<string> results = new List<string>();

                    string str = stdOut.ToString();

                    var object_List = str.Split(new[] { Environment.NewLine }, StringSplitOptions.None );

                    foreach( string address_record in object_List)
                    {
                        try
                        {
                            int index = address_record.IndexOf (' ', 2, 8);
                            string piece = address_record.Substring(2, index);
                            if(Char.IsDigit(piece, 0))
                            {
                                results.Add(piece);
                            }
                        }
                        catch (System.Exception)
                        {
                             break;
                        }
                        
                    }

                    return results.ToList();
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