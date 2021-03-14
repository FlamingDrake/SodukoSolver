using System;

namespace SodukoSolver
{
    public class Sudoku
    {
        public Sudoku(string oneDimensionSudoku)
        {
            if (oneDimensionSudoku.Length != 81)
            {
                throw new ArgumentException("Length of sudoku is incorrect");
            }

            if (oneDimensionSudoku.Replace(".", "").Length < 17)
            {
                IsValid = false;
            }

            SudokuGrid = new int[9,9];

            for (var i = 0; i < oneDimensionSudoku.Length; i++)
            {
                var row = i / 9;
                var line = i % 9;
                if (oneDimensionSudoku[i] == '.')
                {
                    SudokuGrid[row, line] = 0;
                }
                else
                {
                    SudokuGrid[row, line] = int.Parse(oneDimensionSudoku[i].ToString());
                }
            }
        }
        
        public int[,] SudokuGrid { get; }

        public bool IsValid { get; } = true;

        /// <summary>
        /// Tries to insert <paramref name="number"/> at {<paramref name="row"/>,<paramref name="line"/>}
        /// </summary>
        /// <param name="row">The row</param>
        /// <param name="line">The line</param>
        /// <param name="number">The number, between 0-9</param>
        /// <returns>True if acceptable, false if it isn't valid</returns>
        public bool Insert(int row, int line, int number)
        {
            if (number == 0)
            {
                SudokuGrid[row, line] = 0;
                return true;
            }

            if (ValidNumber(row, line, number))
            {
                SudokuGrid[row, line] = number;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var returnString = string.Empty;

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    returnString += SudokuGrid[i, j] == 0 ? "." : SudokuGrid[i, j];
                }
            }

            return returnString;
        }

        private bool ValidNumber(int row, int line, int number)
        {
            var firstPart = (row * 9 + line) % 9 / 3;
            var secondPart = (row * 9 + line) / 27;
            
            var grid = firstPart + 3 * secondPart;

            if (IsGridOk(grid, number) &&
                IsRowOk(row, number) &&
                IsLineOk(line, number))
            {
                return true;
            }

            return false;
        }

        private bool IsLineOk(int line, int number)
        {
            var ok = true;
            for (var i = 0; i < 9; i++)
            {
                if (SudokuGrid[i, line] == number)
                {
                    ok = false;
                }
            }

            return ok;
        }

        private bool IsRowOk(int row, int number)
        {
            var ok = true;
            for (var i = 0; i < 9; i++)
            {
                if (SudokuGrid[row, i] == number)
                {
                    ok = false;
                }
            }

            return ok;
        }

        private bool IsGridOk(int grid, int number)
        {
            var topLeftPositionRow = grid / 3 * 3;
            var topLeftPositionLine = grid % 3 * 3;
            var ok = true;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (SudokuGrid[topLeftPositionRow + i, topLeftPositionLine + j] == number)
                    {
                        ok = false;
                    }
                }
            }
            
            return ok;
        }

    }
}