namespace sudoku;
public class SudokuNum : Control
{
    private string displayedString = "";
    private bool isGuessSquare = false;
    private Brush labelColor;

    public SudokuNum(int x, int y, int num, bool isGuessSquare)
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Size = new Size(50, 50);
        Location = new Point(x, y);
        displayedString = num.ToString();
        this.isGuessSquare = isGuessSquare;
        if (isGuessSquare) {
            labelColor = Brushes.Red;
        } else {
            labelColor = Brushes.Black;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Font font = new("Arial", 24);
        if (displayedString != "0") {
            e.Graphics.DrawString(displayedString, font, labelColor, 0, 0);
        }
    }

    internal void SetNum(int num) {
        if (num.ToString() != displayedString) {
            displayedString = num.ToString();
            Invalidate();
        }
    }

    internal void SetComplete() {
        if (isGuessSquare) {
            labelColor = Brushes.SeaGreen;
            Invalidate(); 
        }
    }
}