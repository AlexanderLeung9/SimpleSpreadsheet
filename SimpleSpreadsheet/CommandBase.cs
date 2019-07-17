using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleSpreadsheet
{
    public abstract class CommandBase
    {
        /// <summary>
        /// The bits of a maximal number in a cell of a speradsheet.
        /// It is the length of a cell minus one, because a space for a negative sign of a negative number or a white space of a positive number is necessary(For the side of positive numbers, it will make two three bits number concatenating together to look like a six bits number if there is no white space between them).
        /// </summary>
        public const int Max_Number_Bits = 2;
        /// <summary>
        /// The bits of maximal width of a spreadsheet.
        /// </summary>
        protected const int Max_Width_Bits = 2;
        /// <summary>
        /// The bits of maximal height of a spreadsheet.
        /// </summary>
        protected const int Max_Height_Bits = 2;

        public CommandBase()
        {
            Verification = new Regex(VerificationExpression);
        }

        /// <summary>
        /// The component in decorator pattern and the successor in chain of responsibility pattern.
        /// </summary>
        public CommandBase Successor { get; set; }
        /// <summary>
        /// Used to differentiate which command is belong to me.
        /// </summary>
        protected abstract string CommandTag { get; }

        /// <summary>
        /// A regular expression for checking the input command valid or not.
        /// </summary>
        protected abstract string VerificationExpression { get; set; }
        private Regex Verification { get; set; }

        /// <summary>
        /// Direction for user how to use this command.
        /// </summary>
        protected abstract string Illustration { get; set; }

        // The Operation() of component in decorator pattern.
        public void BuildIllustrations(List<string> illustrations)
        {
            illustrations.Add(Illustration);
            if (Successor != null)
            {
                Successor.BuildIllustrations(illustrations);
            }
        }

        /// <returns>Null if the command is legal, otherwise the error message.</returns>
        private string VerifyCommand(string command)
        {
            bool isLegal = Verification.IsMatch(command);
            return !isLegal ? String.Concat("Error command! Please see reference below:\r\n", Illustration) : null;
        }

        /// <summary>
        /// Execute the command for the spreadsheet.
        /// </summary>
        /// <returns>Null or empty if the command is legal, otherwise the error message.
        /// Null indicates the command is verified but not handled, empty indicates the command is handled.</returns>
        public virtual string Execute(string command, ref int?[,] spreadsheet)
        {
            // The HandleRequest() of Handler in chain of responsibility pattern.
            if (!command.StartsWith(CommandTag))
            {
                if (Successor != null)
                {
                    return Successor.Execute(command, ref spreadsheet);
                }
                else
                {
                    return "Unrecognized command!";
                }
            }

            string message = VerifyCommand(command);
            return message;
        }

        #region Helpers
        /// <summary>
        /// Check is the given position is in the range of the spreadsheet.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="spreadsheet"></param>
        /// <returns></returns>
        protected static string CheckPositionValid(byte num, int x, int y, int?[,] spreadsheet)
        {
            if (x < 1 || spreadsheet.GetUpperBound(0) + 1 < x)
            {
                return $"x{num}({x}) must be between 1 and {spreadsheet.GetUpperBound(0) + 1}.";
            }
            else if (y < 1 || spreadsheet.GetUpperBound(1) + 1 < y)
            {
                return $"y{num}({y}) must be between 1 and {spreadsheet.GetUpperBound(1) + 1}.";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if the given value is in the prescriptive range.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static string CheckValueValid(string name, int value)
        {
            int minValue = (int)-Math.Pow(10, Max_Number_Bits) + 1;
            int maxValue = (int)Math.Pow(10, Max_Number_Bits) - 1;
            if (value < minValue || maxValue < value)
            {
                return $"The {name}({value}) is out of range [{minValue},{maxValue}].";
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}