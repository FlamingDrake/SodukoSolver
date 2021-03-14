# SudokuSolver

Console application which can load sudoku files and solve it.

## How to run

Start whichever IDE and run. Type `help` for more information on usage.

## Features

It will solve a given sudoku and inform the user if more than one solution has been found.

A warning will be shown if fewer than 17 digits are supplied.

### Known limitations

Given invalid input will lock up the program for a few minutes. Invalid as in same number on same row twice or more. SudokuSolver expects valid input.

The following input will be slow to complete, but it will complete. Limitation of implementation.

```
.........
.....3.85
..1.2....
...5.7...
..4...1..
.9.......
5......73
..2.1....
....4...9
```

### Note

Running and unit tests may require elevated privileges as it will try to write to disk. 