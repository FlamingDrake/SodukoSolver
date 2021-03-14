using System;
using System.IO;

namespace SodukoSolver
{
    class Program
    {
        private static IPuzzle _puzzle;
        
        static void Main(string[] args)
        {
            Configure();
            
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
                                _puzzle.Load(pathToFile);
                                Console.WriteLine($"{pathToFile} loaded successfully.");
                                if (!_puzzle.IsValid())
                                {
                                    Console.WriteLine("WARN: There are too few digits to accurately solve this puzzle");
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.WriteLine("File was not found. Please check filepath");
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Invalid input. Please check the file.");
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
                            _puzzle.Save(pathToSaveFile);
                            Console.WriteLine($"{pathToSaveFile} saved successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error in usage");
                            Console.WriteLine("Usage: save -p path/to/file");
                        }
                        break;
                    case Command.Solve:
                        _puzzle.Solve();
                        Console.WriteLine(_puzzle.IsSolved()
                            ? "Sudoku solved successfully."
                            : "A solution was not found. Please try with another puzzle.");
                        if (!_puzzle.IsValid())
                        {
                            Console.WriteLine("This is not a valid sudoku. Multiple answers exist.");
                        }
                        break;
                    case Command.Help:
                        Help();
                        break;
                    case Command.UnknownCommand:
                        Console.WriteLine("Unknown command");
                        Help();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Poor man dependency injection.
        /// </summary>
        private static void Configure()
        {
            _puzzle = new Puzzle();
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

        private static void Help()
        {
            Console.WriteLine("Available commands are:");
            Console.WriteLine("   load");
            Console.WriteLine("   save");
            Console.WriteLine("   solve");
            Console.WriteLine("   exit");
        }
    }
}