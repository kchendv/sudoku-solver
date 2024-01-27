namespace sudoku;
public class SudokuNum : Control
{
    private string displayedString = ""; 
    public SudokuNum(int x, int y)
    {
        // Set up the initial properties of the control
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Size = new Size(100, 100);
        Location = new Point(x, y);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Font font = new("Arial", 24);
        if (displayedString != "0") {
            e.Graphics.DrawString(displayedString, font, Brushes.Black, 0, 0);
        }
        
    }

    internal void SetNum(int num) {
        displayedString = num.ToString();
        Invalidate();
    }
}