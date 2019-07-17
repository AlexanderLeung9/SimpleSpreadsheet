using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSpreadsheet
{
    public class CreateCommand : CommandBase
    {
        public const int Min_Width = 2;
        public const int Max_Width = 25;
        public const int Min_Height = 2;
        public const int Max_Height = 58;
        public const string Command_Tag = "C ";

        protected override string CommandTag
        {
            get { return Command_Tag; }
        }

        protected override string VerificationExpression { get; set; }
          = @"^ct\d{1,w} \d{1,h}$".Replace("ct", Command_Tag).Replace("w", Max_Width_Bits.ToString()).Replace("h", Max_Height_Bits.ToString());

        protected override string Illustration { get; set; }
          = $"\"{Command_Tag}w h\": create a new spread sheet of width w({Min_Width} <= w <= {Max_Width}) and height h({Min_Height} <= h <= {Max_Height}) (i.e. the spreadsheet can hold w * h amount of cells).";

        public override string Execute(string command, ref int?[,] spreadsheet)
        {
            string message = base.Execute(command, ref spreadsheet);
            if (message != null) return message;

            string[] parameters = command.Split(' ');

            #region Check parameters
            int width, height;
            width = Int32.Parse(parameters[1]);
            height = Int32.Parse(parameters[2]);

            if (width < Min_Width || Max_Width < width)
            {
                return $"The width({width}) must be between {Min_Width} and {Max_Width}.";
            }
            if (height < Min_Height || Max_Height < height)
            {
                return $"The height({height}) must be between {Min_Height} and {Max_Height}.";
            }
            #endregion

            spreadsheet = new int?[width, height];
            return String.Empty;
        }
    }
}