using Timer = System.Windows.Forms.Timer;

namespace sudoku;

public partial class Form1 : Form
{
    private int[,] initialSD;
    private SudokuNum[,] labels;
    private readonly int DIM = 9;
    private readonly int DIMSQ = 81;
    private Timer updateTimer;
    public Form1()
    {
        InitializeComponent();
        initialSD = new int[,] {{0,0,0,0,0,0,0,5,0},
                                {2,0,7,0,0,9,0,0,0},
                                {6,0,0,3,5,1,0,0,0},
                                {5,0,0,0,0,0,0,1,0},
                                {0,0,3,0,0,0,0,0,8},
                                {0,0,0,8,2,0,5,3,0},
                                {0,0,0,0,7,0,8,0,4},
                                {0,0,6,2,0,0,0,0,0},
                                {0,8,0,0,0,0,7,0,0}};
        labels = new SudokuNum[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int numberX = 10 + (110 * i) - 0 + 30;
                int numberY = 10 + (110 * j) - 0 + 25;
                labels[j, i] = new SudokuNum(numberX, numberY);
                labels[j, i].SetNum(initialSD[j,i]);
                Controls.Add(labels[j, i]);
                
            }
        }

        updateTimer = new Timer();
        updateTimer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
        updateTimer.Tick += UpdateTimer_Tick;

        // Start the timer
        updateTimer.Start();
    }

    private void UpdateTimer_Tick(object? sender, EventArgs e)
    {
        initialSD[1,1]++;
        labels[1,1].SetNum(initialSD[1,1]);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);        
        // Custom drawing logic using the Graphics object
        Graphics graphics = e.Graphics;
        Pen blackPen = new Pen(Color.Black);
        Font font = new Font("Arial", 24, FontStyle.Bold);
        
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                graphics.DrawRectangle(blackPen, 10 + (110 * i), 10 + (110 * j), 100, 100);
            }
        }
    }

    private (int, int) convertLoc(int loc) {
        if (loc < 0 || loc > DIMSQ - 1) {
            return (-1, -1);
        }

        return (loc / DIM, loc % DIM);
    }
}
