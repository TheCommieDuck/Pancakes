using System;
using System.Diagnostics;
using System.IO;

namespace WaffleCat.Core
{
    /// <summary>
    /// A debug logging class.
    /// </summary>
    static class Log
    {
        private static StreamWriter writer;

        private static bool isReady;

        /// <summary>
        /// Initialize the logger and the log file.
        /// </summary>
        public static void Initialize()
        {
            //File stuff
            FileInfo dir = new FileInfo("log/Log " + DateTime.Now.ToString("dd-MM-yyyy") + ".wclog");
            dir.Directory.Create();
            writer = dir.AppendText();

            writer.AutoFlush = true;
            writer.WriteLine("Log starting: " + DateTime.Now.ToString());
            isReady = true;
        }

        /// <summary>
        /// Write a message to the log file and console. Debug only.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        [Conditional("DEBUG")]
        public static void Write(String msg, params object[] objs)
        {
            string message = DateTime.Now.ToString("HH:mm:ss") + " " + msg;
            if (!isReady)
                Initialize();
            message = String.Format(message, objs);
            Log.WriteToConsole(message);
            writer.WriteLine(message);
        }

        /// <summary>
        /// Write an error to the log file, in release mode too.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        public static void WriteError(string msg)
        {
            if (!isReady)
                Initialize();
            string message = DateTime.Now.ToString("HH:mm:ss") + " ERROR:" + msg;
            writer.WriteLine(message);
            WriteToConsole(message, ConsoleColor.Red);
        }

        /// <summary>
        /// Close the log.
        /// </summary>
        public static void Close()
        {
            writer.WriteLine(new string('*', 10));
            writer.WriteLine("Log ending: " + DateTime.Now.ToString());
            writer.Close();
            isReady = false;
        }

        /// <summary>
        /// Helper method for writing to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="color">The colour to write in.</param>
        [Conditional("DEBUG")]
        private static void WriteToConsole(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
        }
    }
}
