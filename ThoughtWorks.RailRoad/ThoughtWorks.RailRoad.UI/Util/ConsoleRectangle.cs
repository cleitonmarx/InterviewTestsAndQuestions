using System;
using System.Drawing;

namespace ThoughtWorks.RailRoad.UI.Util
{
    public class ConsoleRectangle
    {

        public ConsoleRectangle(int width, int hieght, Point location, ConsoleColor borderColor)
        {
            Width = width;
            Hieght = hieght;
            Location = location;
            BorderColor = borderColor;
        }

        public Point Location { get; set; }

        public int Width { get; set; }

        public int Hieght { get; set; }

        public ConsoleColor BorderColor { get; set; }

        public void Draw()
        {
            string s = "╔";
            string space = "";
            string temp = "";
            for (int i = 0; i < Width; i++)
            {
                space += " ";
                s += "═";
            }

            for (int j = 0; j < Location.X; j++)
                temp += " ";

            s += "╗" + "\n";

            for (int i = 0; i < Hieght; i++)
                s += temp + "║" + space + "║" + "\n";

            s += temp + "╚";
            for (int i = 0; i < Width; i++)
                s += "═";

            s += "╝" + "\n";

            Console.ForegroundColor = BorderColor;
            Console.CursorTop = Location.Y;
            Console.CursorLeft = Location.X;
            Console.Write(s);
            Console.ResetColor();
        }
    }
}
