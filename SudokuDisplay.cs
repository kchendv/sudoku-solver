using Microsoft.VisualBasic;
using Timer = System.Windows.Forms.Timer;

namespace sudoku;

public partial class SudokuDisplay : Form
{
    private readonly SudokuNum[,] labels = new SudokuNum[9,9];
    private readonly Timer updateTimer = new() {Interval = 1};
    private readonly SudokuGame game = new();
    private readonly int speedTickMultiplier = 30000;

    public SudokuDisplay()
    {
        InitializeComponent();
        // Add number labels
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                labels[j,i] = new SudokuNum(10 + (110 * i) - 0 + 30,
                                            10 + (110 * j) - 0 + 25,
                                            game.GetNum(j,i),
                                            game.IsGuessSquare(j,i));
                Controls.Add(labels[j, i]);
            }
        }
        updateTimer.Tick += UpdateBoard;
        updateTimer.Start();
    }


    private void UpdateBoard(object? sender, EventArgs e) {
        for (int i = 0; i < speedTickMultiplier; i++) {
            game.Update();
        }
        // Update the square that was changed
        foreach ((int j, int i) in game.GetChangedSquares()) {
            labels[j, i].SetNum(game.GetNum(j,i));
        }

        game.ClearChangedSquares();

        // Stop timer if game finished
        if (game.IsComplete()) {
            for (int i = 0; i < 9; i++) {
                for (int j = 0; j < 9; j++) {
                    labels[j,i].SetComplete();
                }
            }
            updateTimer.Stop();
            return;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics graphics = e.Graphics;
        Pen blackPen = new(Color.Black, 3);
        // Draw sudoku board
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                graphics.FillRectangle(Brushes.Gainsboro, 10 + (110 * (i + 3)), 10 + (110 * j), 100, 100);
                graphics.FillRectangle(Brushes.Gainsboro, 10 + (110 * i), 10 + (110 * (j + 3)), 100, 100);
                graphics.FillRectangle(Brushes.Gainsboro, 10 + (110 * (i + 6)), 10 + (110 * (j + 3)), 100, 100);
                graphics.FillRectangle(Brushes.Gainsboro, 10 + (110 * (i + 3)), 10 + (110 * (j + 6)), 100, 100);
            }
        }

        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                graphics.DrawRectangle(blackPen, 10 + (110 * i), 10 + (110 * j), 100, 100);
            }
        }
    }
}
