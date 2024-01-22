namespace sudoku;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        int[,] initialSD = {{0,0,0,0,0,0,0,5,0},
                            {2,0,7,0,0,9,0,0,0},
                            {6,0,0,3,5,1,0,0,0},
                            {5,0,0,0,0,0,0,1,0},
                            {0,0,3,0,0,0,0,0,8},
                            {0,0,0,8,2,0,5,3,0},
                            {0,0,0,0,7,0,8,0,4},
                            {0,0,6,2,0,0,0,0,0},
                            {0,8,0,0,0,0,7,0,0}};

        // Custom drawing logic using the Graphics object
        Graphics graphics = e.Graphics;
        Pen blackPen = new Pen(Color.Black);
        Font font = new Font("Arial", 24, FontStyle.Bold);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                graphics.DrawRectangle(blackPen, 10 + (110 * i), 10 + (110 * j), 100, 100);
                if (initialSD[j, i] != 0) {
                    int numberX = 10 + (110 * i) - 0 + 30;
                    int numberY = 10 + (110 * j) - 0 + 25;
                    graphics.DrawString(initialSD[j, i].ToString(), font, Brushes.Black, numberX, numberY);
                }
            }
        }
    }
}
