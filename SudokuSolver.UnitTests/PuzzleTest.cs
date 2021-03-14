using FluentAssertions;
using Xunit;

namespace SodukoSolver.UnitTests
{
    public class PuzzleTest
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
    }
}