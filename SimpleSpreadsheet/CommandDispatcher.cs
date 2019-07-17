using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSpreadsheet
{
    public class CommandDispatcher
    {
        private const string Quit_Command_Tag = "Q";
        public const string Help_Command_Tag = "help";

        public CommandDispatcher()
        {
            MyCommand = new CreateCommand();
            MyCommand.Successor = new NumberCommand();
            MyCommand.Successor.Successor = new SumCommand();
        }

        private CommandBase MyCommand { get; set; }

        private int?[,] mSpreadsheet;

        private string _Illustrations;
        private string Illustrations
        {
            get
            {
                if (_Illustrations == null)
                {
                    List<string> illustrations = new List<string>();

                    MyCommand.BuildIllustrations(illustrations);
                    illustrations.Add($"\"{Quit_Command_Tag}\": quit the program.");

                    _Illustrations = String.Join(Environment.NewLine, illustrations);
                }
                return _Illustrations;
            }
        }

        /// <returns>true means to continue; false means to quit.</returns>
        public bool CheckInputToDispatch(string command, out string message)
        {
            if (command == Quit_Command_Tag)
            {
                message = null;
                return false;
            }

            if (command == String.Empty)
            {
                message = $"Please input a command(e.g. \"{Help_Command_Tag}\" for getting help).\r\n";
                return true;
            }
            if (command == Help_Command_Tag)
            {
                message = Illustrations + Environment.NewLine;
                return true;
            }

            message = MyCommand.Execute(command, ref mSpreadsheet);
            if (String.IsNullOrEmpty(message))
            {
                message = FormatSpreadsheet();
            }
            message += Environment.NewLine;
            return true;
        }

        private string FormatSpreadsheet()
        {
            StringBuilder resultTextBuilder = new StringBuilder();

            int totalLength = (CommandBase.Max_Number_Bits + 1) * (mSpreadsheet.GetUpperBound(0) + 1);

            string decoratedLine = String.Concat(" ", String.Empty.PadRight(totalLength, '-'), " ");
            // Draw the top line.
            resultTextBuilder.AppendLine(decoratedLine);

            // Draw the body.
            for (int j = 0; j <= mSpreadsheet.GetUpperBound(1); ++j)
            {
                resultTextBuilder.Append("|");

                for (int i = 0; i <= mSpreadsheet.GetUpperBound(0); ++i)
                {
                    int? value = mSpreadsheet[i, j];
                    string number = !value.HasValue ? String.Empty : value.Value.ToString();

                    resultTextBuilder.Append(number.PadLeft((CommandBase.Max_Number_Bits + 1), ' '));
                }

                resultTextBuilder.AppendLine("|");
            }

            // Draw the bottom line.
            resultTextBuilder.Append(decoratedLine);

            string resultText = resultTextBuilder.ToString();
            return resultText;
        }
    }
}