﻿using System;
using System.IO;

namespace SodukoSolver
{
    public class Puzzle : IPuzzle
    {
        private const int PuzzleSize = 9;
        
        private Sudoku _sudoku;

        private bool _isSolved;
        
        public Puzzle()
        {
            _isSolved = false;
        }

        public bool IsSolved()
        {
            return _isSolved;
        }

        public bool IsValid()
        {
            return _sudoku.IsValid;
        }

        public IPuzzle Solve()
        {
            _isSolved = Solve(0, 0);
            return this;
        }


        public void Load(string path)
        {
            using var sr = new StreamReader(path);
            string line;

            var sudokuString = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                sudokuString += line;
            }

            _sudoku = new Sudoku(sudokuString);
        }

        public void Save(string path)
        {
            var saveString = _sudoku.ToString();

            using var sw = new StreamWriter(path);

            for (var i = 0; i < PuzzleSize; i++)
            {
                sw.WriteLine(saveString.Substring(i * PuzzleSize, PuzzleSize));
            }
        }
        
        private bool Solve(int row, int line)
        {
            if (line == PuzzleSize)
            {
                row += 1;
                line = 0;
            }
            
            if (row == PuzzleSize)
            {
                return true;
            }

            if (_sudoku.SudokuGrid[row, line] != 0)
            {
                return Solve(row, line + 1);
            }

            for (var i = 1; i <= PuzzleSize; i++)
            {
                if (_sudoku.Insert(row, line, i))
                {
                    if (Solve(row, line + 1))
                    {
                        return true;
                    }

                    _sudoku.Insert(row, line, 0);
                }
            }

            return false;
        }
    }
}