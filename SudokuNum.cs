// namespace sudoku;

// public class SudokuNum : Control
// {
//     private new Size Size = new(100, 100);
//     private string displayedString = "ww";
//     private readonly int x;
//     private readonly int y;
//     public SudokuNum(int x, int y) {
//         this.x = x;
//         this.y = y;
//         this.Size = new Size(100, 100);
//         this.BackColor = Color.LightGray;
//     }
//     public void SetNum(int num)
//     {
//         // Update the displayed string and trigger invalidation
//         displayedString = num.ToString();
//         Invalidate();
//     }

//     protected override void OnPaint(PaintEventArgs e)
//     {
//         base.OnPaint(e);

//         // Draw the displayed string on the control
//         Graphics graphics = e.Graphics;
//         Font font = new Font("Arial", 12);
//         graphics.DrawString(displayedString, font, Brushes.Black, x, y);
//         Pen pen = new Pen(Color.Blue, 2);
//         Rectangle rectangle = new Rectangle(10, 10, this.Width - 20, this.Height - 20);
//         graphics.DrawRectangle(pen, rectangle);
//         Console.WriteLine(displayedString);
//     }
// }



public class SudokuNum : Control
{
    private string displayedString = "ww";
    private readonly int x;
    private readonly int y;   
    public SudokuNum(int x, int y)
    {
        // Set up the initial properties of the control
        this.x = x;
        this.y = y;
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        this.Size = new Size(100, 100);
        this.Location = new Point(x, y);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw a rectangle on the control
        Graphics graphics = e.Graphics;
        Font font = new Font("Arial", 12);
        graphics.DrawString(displayedString, font, Brushes.Black, 0, 0);
        Console.WriteLine(x.ToString() + y.ToString() + displayedString);
    }

    internal void SetNum(int num) {
        displayedString = num.ToString();
        Invalidate();
    }
}