using System;
using FluentAssertions;
using Xunit;

namespace SodukoSolver.UnitTests
{
    public class SudokuTest
    {
        [Fact]
        public void SudokuShouldThrowExceptionIfLengthIsnt81()
        {
            // Arrange
            var testString = "NotEnoughCharacters";
            
            // Act
            var action = new Func<Sudoku>(() => new Sudoku(testString));
            
            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SudokuShouldReturnObjectIfLengthIs81()
        {
            // Arrange
            var testString = ".2...............................................................................";
            
            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.SudokuGrid
                .Should()
                .BeOfType<int[,]>()
                .And
                .Contain(0)
                .And
                .Contain(2);
        }

        [Fact]
        public void SudokuShouldInsert()
        {
            // Arrange
            var testString = ".................................................................................";

            // Act
            var sudoku = new Sudoku(testString);
            sudoku.Insert(0, 0, 1);
            
            // Assert
            sudoku.SudokuGrid
                .Should()
                .Contain(1);
        }

        [Fact]
        public void SudokuShouldReturnFalseOnSameInsertRow()
        {
            // Arrange
            var testString = ".1...............................................................................";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.Insert(0, 0, 1)
                .Should()
                .BeFalse();
        }
        
        [Fact]
        public void SudokuShouldReturnFalseOnSameInsertLine()
        {
            // Arrange
            var testString = ".........1.......................................................................";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.Insert(0, 0, 1)
                .Should()
                .BeFalse();
        }
        
        [Fact]
        public void SudokuShouldReturnFalseOnSameInsertSubGrid()
        {
            // Arrange
            var testString = "...........1..........................................................1..........";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.Insert(8, 8, 1)
                .Should()
                .BeFalse();

            sudoku.Insert(0, 0, 1)
                .Should()
                .BeFalse();
        }

        [Fact]
        public void SudokuToStringShouldReturnString()
        {
            // Arrange
            var testString = "...........1..........................................................1..........";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.ToString()
                .Should()
                .Be(testString);
        }

        [Fact]
        public void SudokuWithLessThan17DigitsIsConsideredInvalid()
        {
            // Arrange
            var testString = "...........1..........................................................1..........";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.IsValid
                .Should()
                .BeFalse();
        }
        [Fact]
        public void SudokuWithMoreThan17DigitsIsConsideredValid()
        {
            // Arrange
            var testString = "123456789123456789...............................................................";

            // Act
            var sudoku = new Sudoku(testString);
            
            // Assert
            sudoku.IsValid
                .Should()
                .BeTrue();
        }
    }
}