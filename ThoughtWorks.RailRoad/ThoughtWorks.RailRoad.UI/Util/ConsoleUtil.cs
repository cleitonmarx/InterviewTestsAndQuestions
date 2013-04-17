using System;
using System.Drawing;

namespace ThoughtWorks.RailRoad.UI.Util
{
    /// <summary>
    /// Console utility class
    /// </summary>
    public static class ConsoleUtil
    {

        public static void DrawTextWithBorder(
            int x, int y, int heigth, string text, ConsoleColor borderColor, ConsoleColor textColor,
            ConsolePosition position
        )
        {
            var retangle = new ConsoleRectangle(Console.WindowWidth - 3, heigth, new Point(x, y), borderColor);
            retangle.Draw();
            var centerTextWidth = (position == ConsolePosition.Center ? (Console.WindowWidth / 2) - (text.Length / 2) : 2);
            var centerTextHeigth = (y + heigth)-1;

            Console.ForegroundColor = textColor;

            foreach (var line in text.Split(new[]{Environment.NewLine}, StringSplitOptions.None))
            {
                Console.SetCursorPosition(centerTextWidth, centerTextHeigth);
                Console.WriteLine(line);
                centerTextHeigth++;
            }

            Console.SetCursorPosition(0,heigth+3);
        }


    }


    

    
}
