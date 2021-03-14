using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace SodukoSolver.UnitTests
{
    public class PuzzleTest : IDisposable
    {
        [Fact]
        public void LoadShouldLoadFile()
        {
            // Arrange
            var puzzle = new Puzzle();

            // Act
            puzzle.Load("..\\..\\..\\TestSudoku.txt");

            // Assert
            // Not throw exception.
        }

        [Fact]
        public void SaveShouldSaveFile()
        {
            // Arrange
            var puzzle = new Puzzle();

            // Act
            puzzle.Load("..\\..\\..\\TestSudoku.txt");
            puzzle.Save("..\\..\\..\\Temp.txt");

            // Assert
            File.Exists("..\\..\\..\\Temp.txt").Should().BeTrue();
        }

        [Fact]
        public void SolveShouldSolve()
        {
            // Arrange
            var puzzle = new Puzzle();
            
            // Act
            puzzle.Load("..\\..\\..\\TestSudoku.txt");
            var sut = puzzle.Solve();

            // Assert
            sut.IsSolved().Should().BeTrue();
        }
        
        [Fact]
        public void EmptySudokuShouldYieldInvalidSolution()
        {
            // Arrange
            var puzzle = new Puzzle();
            
            // Act
            puzzle.Load("..\\..\\..\\EmptySudoku.txt");
            var sut = puzzle.Solve();

            // Assert
            sut.IsSolved().Should().BeTrue();
            sut.IsValid().Should().BeFalse();
        }

        public void Dispose()
        {
            if (File.Exists("..\\..\\..\\Temp.txt"))
            {
                File.Delete("..\\..\\..\\Temp.txt");
            }
        }
    }
}