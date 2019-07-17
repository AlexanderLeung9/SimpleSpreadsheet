using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSpreadsheet
{
    public class SumCommand : CommandBase
    {
        public const string Command_Tag = "S ";

        protected override string CommandTag
        {
            get { return Command_Tag; }
        }

        protected override string VerificationExpression { get; set; }
          = @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$".Replace("ct", Command_Tag).Replace("w", Max_Width_Bits.ToString()).Replace("h", Max_Height_Bits.ToString());

        protected override string Illustration { get; set; }
          = $"\"{Command_Tag}x1 y1 x2 y2 x3 y3\": perform sum on top of all cells from (x1,y1) to (x2,y2) and store the result(at most {Max_Number_Bits} bits) in (x3,y3); all coordinates should be greater than zero.";

        public override string Execute(string command, ref int?[,] spreadsheet)
        {
            string message = base.Execute(command, ref spreadsheet);
            if (message != null) return message;

            if (spreadsheet == null)
            {
                return "You must create the spreadsheet first before calculate the sum.";
            }

            string[] parameters = command.Split(' ');

            #region Check parameters
            int x1, y1, x2, y2, x3, y3;
            x1 = Int32.Parse(parameters[1]);
            y1 = Int32.Parse(parameters[2]);
            x2 = Int32.Parse(parameters[3]);
            y2 = Int32.Parse(parameters[4]);
            x3 = Int32.Parse(parameters[5]);
            y3 = Int32.Parse(parameters[6]);

            message = CheckPositionValid(1, x1, y1, spreadsheet);
            if (message != null) return message;

            message = CheckPositionValid(2, x2, y2, spreadsheet);
            if (message != null) return message;

            message = CheckPositionValid(3, x3, y3, spreadsheet);
            if (message != null) return message;

            #region Confine that the (x1,y1) must be at the left top of the (x2,y2).
            /*if (x1 > x2)
            {
                return $"x1({x1}) must be less or equal than x2({x2}).";
            }
            if (y1 > y2)
            {
                return $"y1({y1}) must be less or equal than y2({y2}).";
            }*/
            #endregion
            #region Confine the (x3,y3) not located in the range from (x1,y1) to (x2,y2).
            /*if (x1 <= x3 && x3 <= x2)
            {
                return $"x3 should not be between x1({x1}) and x2({x2})";
            }
            if (y1 <= y3 && y3 <= y2)
            {
                return $"y3 should not be between y1({y1}) and y2({y2})";
            }*/
            #endregion
            #endregion

            #region Sum
            int sum = 0;
            int xFrom = Math.Min(x1, x2);
            int xTo = Math.Max(x1, x2);
            int yFrom = Math.Min(y1, y2);
            int yTo = Math.Max(y1, y2);
            for (int x = xFrom - 1; x <= xTo - 1; ++x)
            {
                for (int y = yFrom - 1; y <= yTo - 1; ++y)
                {
                    int value = spreadsheet[x, y] ?? default(int);
                    sum += value;
                }
            }
            #endregion

            message = CheckValueValid("sum", sum);
            if (message != null) return message;

            spreadsheet[x3 - 1, y3 - 1] = sum;
            return String.Empty;
        }
    }
}