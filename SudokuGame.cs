using System.Globalization;

namespace sudoku;
public class SudokuGame
{
    private readonly int [,] initialBoard = new int[9,9];
    private readonly int [,] currentBoard = new int[9,9];
    private int curConsider = 0;
    private (int,int) changedSquare = (0,0);

    private readonly List<(int, int)> guessSquares = [];

    public SudokuGame() {
        initialBoard =  new int[,] {{0, 3, 0, 6, 0, 8, 0, 1, 0},
                                    {0, 0, 0, 1, 0, 0, 3, 0, 8},
                                    {1, 0, 0, 0, 0, 2, 0, 0, 7},
                                    {0, 5, 0, 7, 6, 0, 0, 2, 3},
                                    {4, 2, 6, 0, 5, 3, 0, 9, 0},
                                    {0, 0, 0, 9, 0, 4, 0, 5, 6},
                                    {0, 0, 0, 0, 3, 0, 0, 0, 0},
                                    {0, 8, 0, 0, 1, 0, 0, 0, 0},
                                    {3, 0, 5, 0, 8, 0, 0, 0, 0}};
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                currentBoard[j,i] = initialBoard[j,i];
                if (initialBoard[j,i] == 0) {
                    guessSquares.Add((j,i));

                }
            }
        }
    }
    public int GetNum(int j, int i) {
        return currentBoard[j, i];
    }

    public bool IsGuessSquare(int j, int i) {
        return initialBoard[j,i] == 0;
    }

    public (int, int) GetChangedSquare() {
        return changedSquare;
    }

    public void Update() {
        if (IsComplete()) {
            return;
        }
        (int j, int i) = guessSquares[curConsider];
        currentBoard[j,i]++;
        
        if (currentBoard[j,i] > 9) {
            // Impossible to fill this square, back track to the last one
            currentBoard[j,i] = 0;
            changedSquare = (j,i);
            curConsider--;
        } else {
            changedSquare = (j,i);
            if (ValidateSquare(j,i)) {
                curConsider++;
            }
        }
    }   

    public bool IsComplete() {
        return curConsider == guessSquares.Count || curConsider < 0;
    }

    private bool ValidateSquare(int j, int i)
    {
        for (int x = 0; x < 9; x++)
        {
            if ((currentBoard[j,i] == currentBoard[x,i] && x != j) || (currentBoard[j,i] == currentBoard[j,x] && x != i)) {
                return false;
            }
        }
        int blockx = 3 * (j / 3);
        int blocky = 3 * (i / 3);
        for (int bx = blockx; bx < blockx + 3; bx++) {
            for (int by = blocky; by < blocky + 3; by++) {
                if (currentBoard[bx,by] == currentBoard[j,i] && (bx != j || by != i)) {
                    return false;
                }
            }
        }
        return true;
    }
}