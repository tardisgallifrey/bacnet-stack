﻿using System;
using System.Diagnostics;
using System.Text;

//Modifed from Code provided by StackOverflow and Microsoft
//https://stackoverflow.com/questions/206323/how-to-execute-command-line-in-c-get-std-out-results
//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=net-5.0

namespace BACnetProject
{
    abstract public class ReadProperty
    {
        //This abstracted class is the base class for all the Read property (bacrp) needs
        //With an abstract class, I can simplify calls to just the data needed

        //bacrp works like this:  bacrp device_address object_property_number object_instance object_property
        //So, we establish a public string property for each of these, then use a 
        //System Call and return the value for processing

        private string ToolFileName = "bacrp";
        public string objectDevice { get; set; }
        public string objectNumber { get; set; }
        public string objectInstance { get; set; }
        public string objectProperty { get; set; }

        //Output is the base method.  Goes and runs bacrp with the above values and gets the result
        //into a string

        //If  you are going to use System.Diagnostics.myprocess in a class, you must do it this way
        public string Output()
        {
            var stdOut = new StringBuilder();

            using (Process myprocess = new Process())
            {
                //First we fill up all the data for a Process to run
                //We also add in the properties from above as arguments

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
                    //send back stdOut with our value string
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