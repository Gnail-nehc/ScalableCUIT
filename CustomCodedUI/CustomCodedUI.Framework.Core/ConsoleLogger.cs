using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class ConsoleLogger
    {
        private static void WriteLine(string msg, params object[] args)
        {
            string message = string.Empty;
            try
            {
                message = string.Format(msg, args);
            }
            catch
            {
                message = msg;
            }
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " " + message.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void LogDebug(string msg, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine(msg, args);
            Console.ResetColor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void LogInfo(string msg, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(msg, args);
            Console.ResetColor();
        }

        public static void LogError(string msg, params object[] args)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(msg, args);
            Console.ResetColor();
        }

        public static void LogWarning(string msg, params object[] args)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine(msg, args);
            Console.ResetColor();
        }
    }
}
