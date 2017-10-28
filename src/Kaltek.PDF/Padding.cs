namespace Kaltek.PDF
{
    public class Padding
    {

        public Padding()
        {
            Left = Right = Top = Bottom = 0;
        }

        public Padding(int left, int top, int right, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }
}
