using Microsoft.VisualBasic;
using Timer = System.Windows.Forms.Timer;

namespace sudoku;

public partial class SudokuDisplay : Form
{
    private readonly SudokuNum[,] labels = new SudokuNum[9,9];
    private readonly Timer updateTimer = new() {Interval = 1};
    private readonly SudokuGame game = new();
    private readonly int speedTickMultiplier = 30;

    public SudokuDisplay()
    {
        InitializeComponent();
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                labels[j,i] = new SudokuNum(10 + (110 * i) - 0 + 30,
                                            10 + (110 * j) - 0 + 25,
                                            game.GetNum(j,i),
                                            game.IsGuessSquare(j,i));
                Controls.Add(labels[j, i]);
            }
        }

        for (int i = 0; i < speedTickMultiplier; i++) {
            updateTimer.Tick += UpdateBoard;
        }
        updateTimer.Start();
    }


    private void UpdateBoard(object? sender, EventArgs e) {
        game.Update();
        // Stop timer if game finished
        if (game.IsComplete()) {
            updateTimer.Stop();
            return;
        }
        // Update the square that was changed
        (int j, int i) = game.GetChangedSquare();
        labels[j, i].SetNum(game.GetNum(j,i));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        Pen blackPen = new(Color.Black);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                graphics.DrawRectangle(blackPen, 10 + (110 * i), 10 + (110 * j), 100, 100);
            }
        }
    }
}
