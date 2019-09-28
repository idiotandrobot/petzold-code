using System.Drawing;

namespace Code
{
    public class BinaryCharLayout
    {
        public string Value { get; private set; }
        public Point Location { get; private set; }

        public BinaryCharLayout(string binaryString, BinaryFormatting formatting)
        {
            Value = binaryString;

            int x = -formatting.FontSize / 2;
            int y = formatting.FontSize * 2;

            Location = formatting.GetLocation(x, y);
        }
    }
}
