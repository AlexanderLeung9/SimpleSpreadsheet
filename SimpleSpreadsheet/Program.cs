using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSpreadsheet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Show welcome.
            Console.WriteLine($"Welcome to use \"Simple Spreadsheet\"!\r\nEnter \"{CommandDispatcher.Help_Command_Tag}\" to get help.\r\n");

            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string command, message;
            while (true)
            {
                command = Console.ReadLine();

                bool tContinue = commandDispatcher.CheckInputToDispatch(command, out message);
                if (tContinue)
                    Console.WriteLine(message);
                else
                    break;
            }
        }
    }
}
