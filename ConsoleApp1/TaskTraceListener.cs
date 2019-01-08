using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    internal class TaskTraceListener :  TraceListener
    {
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            switch (eventType)
            {
                case TraceEventType.Warning:
                    Console.WriteLine(message);
                    break;
                case TraceEventType.Verbose:
                    //Console.WriteLine(message);
                    break;
                case TraceEventType.Error:
                    Console.WriteLine(message);
                    break;
                default:
                    //Console.WriteLine(message);
                    break;
            }
        }

        public override void Write(string message)
        {
            //Console.WriteLine(message);
        }
        public override void WriteLine(string message)
        {
            //Console.WriteLine(message);
        }
    }
}
