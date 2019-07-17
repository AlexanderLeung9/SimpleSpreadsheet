using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSpreadsheet
{
    class TestProgram
    {
        private const int mTest_Width = 20;
        private const int mTest_Height = 10;
        private const int mTest_Height2 = 20;
        private static int mMin_Value = -(int)Math.Pow(10, CommandBase.Max_Number_Bits) + 1;
        private static int mMax_Value = (int)Math.Pow(10, CommandBase.Max_Number_Bits) - 1;

        static void Main(string[] args)
        {
            //Test4HelpAndUnrecognizedCommand();
            //Test4CreateCommand();
            //Test4NumberCommand();
            //Test4SumCommand();
            //StressTest4CreateCommand();
            //StressTest4NumberCommand();
            //StressTest4SumCommand();
            //StressTest4StateTransition();

            Console.ReadKey();
        }

        private static void Test4HelpAndUnrecognizedCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string message;

            // Cover "if (command == Quit_Command_Tag)" in CheckInputToDispatch() of CommandDispatcher
            Console.WriteLine("Test for quit command:");
            bool tContinue = commandDispatcher.CheckInputToDispatch("Q", out message);
            message = $"tContinue: {tContinue}\r\n";
            Console.WriteLine(message);

            // Cover "if (command == String.Empty)" in CheckInputToDispatch() of CommandDispatcher
            Console.WriteLine("Test for empty string:");
            commandDispatcher.CheckInputToDispatch("", out message);
            Console.WriteLine(message);

            // Cover "if (command == Help_Command_Tag)" in CheckInputToDispatch() of CommandDispatcher
            Console.WriteLine("Test for help command:");
            commandDispatcher.CheckInputToDispatch(CommandDispatcher.Help_Command_Tag, out message);
            Console.WriteLine(message);

            // Cover "if (Successor == null)" in Execute() of CommandBase
            Console.WriteLine("Test for unrecognized command 1:");
            commandDispatcher.CheckInputToDispatch("UNRECOGNIZED", out message);
            Console.WriteLine(message);
        }

        private static void Test4CreateCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string message;

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand
            Console.WriteLine("Test for a mere command tag:");
            commandDispatcher.CheckInputToDispatch(CreateCommand.Command_Tag, out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand and the Illustration
            Console.WriteLine("Test for insufficient parameters:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}1", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand and the Illustration
            Console.WriteLine("Test for redundant parameters:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}1 1 1", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand and the Illustration
            Console.WriteLine("Test for non-numbers:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}a b", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand and the Illustration
            Console.WriteLine("Test for negative width and height:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}-1 -1", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}$" in VerificationExpression of CreateCommand and the Illustration
            Console.WriteLine("Test for too more bits of width and height:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}100 100", out message);
            Console.WriteLine(message);

            // Cover "if (width < Min_Width)" in Execute() of CreateCommand
            Console.WriteLine("Test for width less than the minimun width:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{CreateCommand.Min_Width - 1} 10", out message);
            Console.WriteLine(message);

            // Cover "if (Max_Width < width)" in Execute() of CreateCommand
            Console.WriteLine("Test for width greater than the maximum width:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{CreateCommand.Max_Width + 1} 10", out message);
            Console.WriteLine(message);

            // Cover "if (height < Min_Height)" in Execute() of CreateCommand
            Console.WriteLine("Test for height less than the minimun height:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}20 {CreateCommand.Min_Height - 1}", out message);
            Console.WriteLine(message);

            // Cover "if (Max_Height < height)" in Execute() of CreateCommand
            Console.WriteLine("Test for height greater than the maximum height:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}20 {CreateCommand.Max_Height + 1}", out message);
            Console.WriteLine(message);

            // Cover "if (Min_Width <= width && width <= Max_Width)" and "if (Min_Height <= height && height <= Max_Height)" in Execute() of CreateCommand
            Console.WriteLine("Test for the minimum values:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{CreateCommand.Min_Width} {CreateCommand.Min_Height}", out message);
            Console.WriteLine(message);

            // Cover "if (Min_Width <= width && width <= Max_Width)" and "if (Min_Height <= height && height <= Max_Height)" in Execute() of CreateCommand
            Console.WriteLine("Test for the maximum values:");
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{CreateCommand.Max_Width} {CreateCommand.Max_Height}", out message);
            Console.WriteLine(message);
        }

        private static void Test4NumberCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string message;

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine("Test for a mere command tag:");
            commandDispatcher.CheckInputToDispatch(NumberCommand.Command_Tag, out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine("Test for non-numbers:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}a b c", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine("Test for insufficient parameters:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 1", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine("Test for redundant parameters:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 1 1 1", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet == null)" in Execute() of NumberCommand
            Console.WriteLine("Test for inputing number without creating spreadsheet first:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 1 1", out message);
            Console.WriteLine(message);

            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{mTest_Width} {mTest_Height}", out message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine("Test for negative position:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}-1 -1 -1", out message);
            Console.WriteLine(message);

            // Cover "if (x < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero x1:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}0 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (y < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero y1:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(0) + 1 < x)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for x1 greater than the maximum width:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width + 1} {mTest_Height / 2} 0", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(1) + 1 < y)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for y1 greater than the maximum height:");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width / 2} {mTest_Height + 1} 0", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine($"Test for v1({mMin_Value - 1}) less than the minimum value({mMin_Value}):");
            string command = $"{NumberCommand.Command_Tag}1 2 {mMin_Value - 1}";
            commandDispatcher.CheckInputToDispatch(command, out message);
            Console.WriteLine(message);

            // Cover "if (minValue <= value && value <= maxValue)" in CheckValueValid() of CommandBase
            Console.WriteLine($"Test for v1 equal to the minimum value({mMin_Value}):");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 3 {mMin_Value}", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h} -?\d{1,2}$" in VerificationExpression of NumberCommand and the Illustration
            Console.WriteLine($"Test for v1({mMax_Value + 1}) greater than the maximum value({mMax_Value}):");
            command = $"{NumberCommand.Command_Tag}2 1 {mMax_Value + 1}";
            commandDispatcher.CheckInputToDispatch(command, out message);
            Console.WriteLine(message);

            // Cover "if (minValue <= value && value <= maxValue)" in CheckValueValid() of CommandBase
            Console.WriteLine($"Test for v1 equal to the maximum value({mMax_Value}):");
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}2 2 {mMax_Value}", out message);
            Console.WriteLine(message);
        }

        private static void Test4SumCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string message;

            // Cover the @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$" in VerificationExpression of SumCommand and the Illustration
            Console.WriteLine("Test for a mere command tag:");
            commandDispatcher.CheckInputToDispatch(SumCommand.Command_Tag, out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$" in VerificationExpression of SumCommand and the Illustration
            Console.WriteLine("Test for non-numbers:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}a b c d e f", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$" in VerificationExpression of SumCommand and the Illustration
            Console.WriteLine("Test for insufficient parameters:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 2", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$" in VerificationExpression of SumCommand and the Illustration
            Console.WriteLine("Test for redundant parameters:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 2 3 3 4 4", out message);
            Console.WriteLine(message);

            // Cover the @"^ct\d{1,w} \d{1,h}(?: \d{1,w} \d{1,h}){2}$" in VerificationExpression of SumCommand and the Illustration
            Console.WriteLine("Test for negative position:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}-1 -1 -1 -1 -1 -1", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet == null)" in Execute() of SumCommand
            Console.WriteLine("Test for performing sum without creating spreadsheet first:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 2 3 3", out message);
            Console.WriteLine(message);

            const int mTest_Width = 20;
            const int mTest_Height = 10;
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{mTest_Width} {mTest_Height}", out message);
            // Input numbers forming a 3*3 matrix.
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 1 1", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}2 1 2", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}3 1 3", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 2 2", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}2 2 4", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}3 2 6", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}1 3 3", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}2 3 6", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}3 3 9", out message);

            // Cover "if (x < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero x1:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}0 0 0 0 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (y < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero y1:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 0 0 0 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (x < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero x2:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 0 0 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (y < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero y2:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 0 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (x < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero x3:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 2 0 0", out message);
            Console.WriteLine(message);

            // Cover "if (y < 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for zero y3:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 2 2 3 0", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(0) + 1 < x)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for x1 greater than the maximum width:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}{mTest_Width + 1} {mTest_Height / 2} 3 3 4 4", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(1) + 1 < y)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for y1 greater than the maximum height:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}{mTest_Width / 2} {mTest_Height + 1} 3 3 4 4", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(0) + 1 < x)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for x2 greater than the maximum width:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 {mTest_Width + 1} {mTest_Height / 2} 4 4", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(1) + 1 < y)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for y2 greater than the maximum height:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 {mTest_Width / 2} {mTest_Height + 1} 4 4", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(0) + 1 < x)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for x3 greater than the maximum width:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 3 3 {mTest_Width + 1} {mTest_Height / 2}", out message);
            Console.WriteLine(message);

            // Cover "if (spreadsheet.GetUpperBound(1) + 1 < y)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for y3 greater than the maximum height:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 3 3 {mTest_Width / 2} {mTest_Height + 1}", out message);
            Console.WriteLine(message);

            // Cover "if (1 <= x && x <= spreadsheet.GetUpperBound(0) + 1 && 1 <= y && y <= spreadsheet.GetUpperBound(1) + 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for (x3,y3) out of the range from (x1,y1) to (x2,y2):");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 3 3 4 4", out message);
            Console.WriteLine(message);

            // Cover "if (1 <= x && x <= spreadsheet.GetUpperBound(0) + 1 && 1 <= y && y <= spreadsheet.GetUpperBound(1) + 1)" in CheckPositionValid() of CommandBase
            Console.WriteLine("Test for (x3,y3) in the range from (x1,y1) to (x2,y2):");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 3 3 2 2", out message);
            Console.WriteLine(message);

            // Cover "if (minValue <= value && value <= maxValue)" in CheckValueValid() of CommandBase
            Console.WriteLine("Test for (x1,y1) equal to (x2,y2):");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 1 1 4 4", out message);
            Console.WriteLine(message);

            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}2 2 4", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}4 4 {mMin_Value}", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}5 5 -1", out message);

            // Cover "if (value < minValue)" in CheckValueValid() of CommandBase
            Console.WriteLine("Test for the sum underflowed:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}4 4 5 5 6 6", out message);
            Console.WriteLine(message);

            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}4 4 {mMax_Value}", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}5 5 1", out message);

            // Cover "if (maxValue < value)" in CheckValueValid() of CommandBase
            Console.WriteLine("Test for the sum overflowed:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}4 4 5 5 6 6", out message);
            Console.WriteLine(message);

            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}4 4 0", out message);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}5 5 0", out message);

            // Cover "if (minValue <= value && value <= maxValue)" in CheckValueValid() of CommandBase
            Console.WriteLine("Test for some empty cells:");
            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}1 1 5 5 6 6", out message);
            Console.WriteLine(message);
        }

        private static void StressTest4CreateCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string spreadsheet;

            for (int i = CreateCommand.Min_Width; i <= CreateCommand.Max_Width; ++i)
            {
                for (int j = CreateCommand.Min_Height; j <= CreateCommand.Max_Height; ++j)
                {
                    string command = $"{CreateCommand.Command_Tag}{i} {j}";
                    commandDispatcher.CheckInputToDispatch(command, out spreadsheet);

                    Console.WriteLine(spreadsheet);
                }
            }
        }

        private static void StressTest4NumberCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string spreadsheet;

            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{mTest_Width} {mTest_Height}", out spreadsheet);

            for (int v = mMin_Value; v <= mMax_Value; ++v)
            {
                for (int x = 1; x <= mTest_Width; ++x)
                {
                    for (int y = 1; y <= mTest_Height; ++y)
                    {
                        string command = $"{NumberCommand.Command_Tag}{x} {y} {v}";
                        commandDispatcher.CheckInputToDispatch(command, out spreadsheet);

                        Console.WriteLine(spreadsheet);
                    }
                }
            }
        }

        private static void StressTest4SumCommand()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string spreadsheet;

            // Build a square with 2 positive numbers and 2 negative numbers.
            commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{mTest_Width} {mTest_Height2}", out spreadsheet);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width / 4} {mTest_Height2 / 4} 1", out spreadsheet);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width / 4 * 3} {mTest_Height2 / 4} -1", out spreadsheet);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width / 4} {mTest_Height2 / 4 * 3} -1", out spreadsheet);
            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{mTest_Width / 4 * 3} {mTest_Height2 / 4 * 3} 1", out spreadsheet);

            int errorEndurableTimes = 0;
            int count;
            for (count = 0; count != 100; ++count)
            {
                var random = new Random(DateTime.Now.Millisecond + count);

                RandomSum(commandDispatcher, random, out spreadsheet);
                Console.WriteLine(spreadsheet);

                if (!spreadsheet.StartsWith(" -----"))
                {
                    if (++errorEndurableTimes == 10)
                        break;
                }
            }
            Console.WriteLine($"count={count}");
        }

        private static void RandomSum(CommandDispatcher commandDispatcher, Random random, out string spreadsheet)
        {
            int x1 = (int)(random.NextDouble() * mTest_Width);
            int y1 = (int)(random.NextDouble() * mTest_Height2);
            int x2 = (int)(random.NextDouble() * mTest_Width);
            int y2 = (int)(random.NextDouble() * mTest_Height2);
            int x3 = (int)(random.NextDouble() * mTest_Width);
            int y3 = (int)(random.NextDouble() * mTest_Height2);

            if (x1 == 0) x1 = mTest_Width;
            if (y1 == 0) y1 = mTest_Height2;
            if (x2 == 0) x2 = mTest_Width;
            if (y2 == 0) y2 = mTest_Height2;
            if (x3 == 0) x3 = mTest_Width;
            if (y3 == 0) y3 = mTest_Height2;

            commandDispatcher.CheckInputToDispatch($"{SumCommand.Command_Tag}{x1} {y1} {x2} {y2} {x3} {y3}", out spreadsheet);
        }

        private static void StressTest4StateTransition()
        {
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            string spreadsheet;

            for (int round = 0; round != 100; ++round)
            {
                commandDispatcher.CheckInputToDispatch($"{CreateCommand.Command_Tag}{mTest_Width} {mTest_Height2}", out spreadsheet);

                // input two random numbers then perform sum randomly
                int errorEndurableTimes = 0;
                int count;
                for (count = 0; count != 100; ++count)
                {
                    var random = new Random(DateTime.Now.Millisecond + count);

                    InputRandomNumber(commandDispatcher, random, out spreadsheet);
                    InputRandomNumber(commandDispatcher, random, out spreadsheet);
                    RandomSum(commandDispatcher, random, out spreadsheet);
                    Console.WriteLine(spreadsheet);

                    if (!spreadsheet.StartsWith(" -----"))
                    {
                        if (++errorEndurableTimes == 10)
                            break;
                    }
                }
                Console.WriteLine($"count of Round {round + 1}: {count}");

                //Console.ReadKey();
            }
        }

        private static void InputRandomNumber(CommandDispatcher commandDispatcher, Random random, out string spreadsheet)
        {
            int x1 = (int)(random.NextDouble() * mTest_Width);
            int y1 = (int)(random.NextDouble() * mTest_Height2);
            int v1 = (int)(random.NextDouble() * (mMax_Value - mMin_Value + 1) + mMin_Value);

            if (x1 == 0) x1 = mTest_Width;
            if (y1 == 0) y1 = mTest_Height2;
            if (v1 == mMax_Value + 1) v1 = mMin_Value;

            commandDispatcher.CheckInputToDispatch($"{NumberCommand.Command_Tag}{x1} {y1} {v1}", out spreadsheet);
        }
    }
}
