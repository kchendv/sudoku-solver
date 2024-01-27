using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using Timer = System.Windows.Forms.Timer;

namespace sudoku;

public partial class Form1 : Form
{
    private int[,] initialSD;
    private SudokuNum[,] labels;
    private List<(int, int)> notPerms = new();
    private readonly int DIM = 9;
    private readonly int DIMSQ = 81;
    private Timer updateTimer;

    private int curConsider = 0;
    public Form1()
    {
        InitializeComponent();
        // initialSD = new int[,] {{0,0,0,0,0,0,0,5,0},
        //                         {2,0,7,0,0,9,0,0,0},
        //                         {6,0,0,3,5,1,0,0,0},
        //                         {5,0,0,0,0,0,0,1,0},
        //                         {0,0,3,0,0,0,0,0,8},
        //                         {0,0,0,8,2,0,5,3,0},
        //                         {0,0,0,0,7,0,8,0,4},
        //                         {0,0,6,2,0,0,0,0,0},
        //                         {0,8,0,0,0,0,7,0,0}};
        initialSD = new int[,] {{0, 3, 0, 6, 0, 8, 0, 1, 0},
  {0, 0, 0, 1, 0, 0, 3, 0, 8},
  {1, 0, 0, 0, 0, 2, 0, 0, 7},
  {0, 5, 0, 7, 6, 0, 0, 2, 3},
  {4, 2, 6, 0, 5, 3, 0, 9, 0},
  {0, 0, 0, 9, 0, 4, 0, 5, 6},
  {0, 0, 0, 0, 3, 0, 0, 0, 0},
  {0, 8, 0, 0, 1, 0, 0, 0, 0},
  {3, 0, 5, 0, 8, 0, 0, 0, 0}};
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
                if (initialSD[j ,i] == 0) {
                    notPerms.Add((j ,i));
                }
                
            }
        }

        updateTimer = new Timer
        {
            Interval = 1 // Set the interval to 1000 milliseconds (1 second)
        };
        updateTimer.Tick += UpdateTimer_Tick;

        // Start the timer
        updateTimer.Start();
    }


    private void UpdateTimer_Tick(object? sender, EventArgs e)
    {
        if (curConsider == notPerms.Count || curConsider < 0) {
            updateTimer.Stop();
            return;
        }
        (int j, int i) = notPerms[curConsider];
        initialSD[j,i]++;
        

        if (initialSD[j,i] > 9) {
            initialSD[j,i] = 0;
            labels[j, i].SetNum(0);
            curConsider--;
        } else {
            labels[j,i].SetNum(initialSD[j,i]);
            if (ValidateSudoku(j,i)) {
                curConsider++;
            }
        }
    }

    private bool ValidateSudoku(int j, int i)
    {
        for (int x = 0; x < 9; x++)
        {
            if ((initialSD[j,i] == initialSD[x,i] && x != j) || (initialSD[j,i] == initialSD[j,x] && x != i)) {
                return false;
            }
        }
        int blockx = 3 * (j / 3);
        int blocky = 3 * (i / 3);
        for (int bx = blockx; bx < blockx + 3; bx++) {
            for (int by = blocky; by < blocky + 3; by++) {
                if (initialSD[bx,by] == initialSD[j,i] && (bx != j || by != i)) {
                    return false;
                }
            }
        }
        return true;
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
