using System;
using System.IO;

namespace SodukoSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzleSolver = new Puzzle();
            
            var running = true;
            while (running)
            {
                Console.WriteLine("Enter a command");
                var commandString = Console.ReadLine();

                var command = EvaluateCommand(commandString);

                switch (command)
                {
                    case Command.Exit:
                        running = false;
                        break;
                    case Command.Load:
                        if (FormattedCorrectly(commandString, out var pathToFile))
                        {
                            try
                            {
                                puzzleSolver.Load(pathToFile);
                                Console.WriteLine($"{pathToFile} loaded successfully.");
                                if (!puzzleSolver.IsValid())
                                {
                                    Console.WriteLine("WARN: There are too few digits to accurately solve this puzzle");
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("File was not found. Please check filepath");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error in usage");
                            Console.WriteLine("Usage: load -p path/to/file");
                        }
                        break;
                    case Command.Save:
                        if (FormattedCorrectly(commandString, out var pathToSaveFile))
                        {
                            puzzleSolver.Save(pathToSaveFile);
                            Console.WriteLine($"{pathToSaveFile} saved successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error in usage");
                            Console.WriteLine("Usage: save -p path/to/file");
                        }
                        break;
                    case Command.Solve:
                        puzzleSolver.Solve();
                        Console.WriteLine(puzzleSolver.IsSolved()
                            ? "Sudoku solved successfully."
                            : "A solution was not found. Please try with another puzzle.");
                        break;
                    case Command.Help:
                        Console.WriteLine("HERE IS HELP");
                        break;
                    case Command.UnknownCommand:
                        Console.WriteLine("Unknown command, please type help to list available commands.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static bool FormattedCorrectly(string commandString, out string pathToFile)
        {
            if (commandString.Contains("-p"))
            {
                pathToFile = commandString.Split("-p")[1].Trim();
                return true;
            }

            pathToFile = null;
            return false;
        }

        private static Command EvaluateCommand(string commandString)
        {
            if (commandString.Contains("help"))
            {
                return Command.Help;
            }

            if (commandString.Contains("load"))
            {
                return Command.Load;
            }
            
            if (commandString.Contains("save"))
            {
                return Command.Save;
            }

            if (commandString.Contains("solve"))
            {
                return Command.Solve;
            }

            if (commandString.Contains("exit"))
            {
                return Command.Exit;
            }

            return Command.UnknownCommand;
        }
    }
}