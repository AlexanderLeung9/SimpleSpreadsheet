using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSpreadsheet
{
    public class NumberCommand : CommandBase
    {
        public const string Command_Tag = "N ";

        protected override string CommandTag
        {
            get { return Command_Tag; }
        }

        protected override string VerificationExpression { get; set; }
          = @"^ct\d{1,w} \d{1,h} -?\d{1,2}$".Replace("ct", Command_Tag).Replace("w", Max_Width_Bits.ToString()).Replace("h", Max_Height_Bits.ToString());

        protected override string Illustration { get; set; }
          = $"\"{Command_Tag}x1 y1 v1\": insert a number v1(at most {Max_Number_Bits} bits) in specified cell (x1,y1)(x1>0 and y1>0).";

        public override string Execute(string command, ref int?[,] spreadsheet)
        {
            string message = base.Execute(command, ref spreadsheet);
            if (message != null) return message;

            if (spreadsheet == null)
            {
                return "You must create the spreadsheet first before input a number.";
            }

            string[] parameters = command.Split(' ');

            #region Check parameters
            int x1, y1, v1;
            x1 = Int32.Parse(parameters[1]);
            y1 = Int32.Parse(parameters[2]);
            v1 = Int32.Parse(parameters[3]);

            message = CheckPositionValid(1, x1, y1, spreadsheet);
            if (message != null) return message;

            message = CheckValueValid("value", v1);
            // will not enter this branch currently when the absolute maximum value is Math.Pow(10,Max_Number_Bits)-1
            if (message != null) return message;
            #endregion

            spreadsheet[x1 - 1, y1 - 1] = v1;
            return String.Empty;
        }
    }
}