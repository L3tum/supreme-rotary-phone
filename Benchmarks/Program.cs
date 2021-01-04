#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using Brainfuck;

#endregion

namespace Benchmarks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var benchmarks = new Dictionary<string, Type>
            {
                {"classic", typeof(BrainfuckInterpreterClassic)},
                {"extType1", typeof(BrainfuckInterpreterETypeOne)},
                {"extType2", typeof(BrainfuckInterpreterETypeTwo)},
                {"extType3", typeof(BrainfuckInterpreterETypeThree)}
            };

            var command = "";

            while (command != "exit")
            {
                Console.WriteLine();
                Console.WriteLine("Choose program to run!");
                Console.WriteLine("Type 'list' to get a list of programs!");
                Console.WriteLine("Type 'exit' to exit!");
                Console.Write("Program: ");
                command = Console.ReadLine();

                if (command == "list")
                {
                    Console.WriteLine("benchmark, program");
                }
                else
                {
                    if (command == "benchmark")
                    {
                        while (command != "exit")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Choose benchmark to run!");
                            Console.WriteLine("Type 'list' to get a list of benchmarks!");
                            Console.WriteLine("Type 'exit' to exit!");
                            Console.Write("Benchmark: ");
                            command = Console.ReadLine();

                            if (command == "list")
                            {
                                Console.WriteLine(string.Join(",", benchmarks.Keys));
                            }
                            else if (command != "exit")
                            {
                                if (benchmarks.ContainsKey(command))
                                {
                                    BenchmarkRunner.Run(benchmarks[command]);
                                }
                                else
                                {
                                    Console.WriteLine("Not a valid benchmark name!");
                                }
                            }
                        }
                    }
                    else if (command == "program")
                    {
                        while (command != "exit")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Choose language of interpreter to run!");
                            Console.WriteLine("Type 'list' to get a list of languages!");
                            Console.WriteLine("Type 'exit' to exit!");
                            Console.Write("Language: ");
                            command = Console.ReadLine();

                            if (command == "list")
                            {
                                Console.WriteLine("Classic, Ext1, Ext2, Ext3");
                            }
                            else if (command != "exit")
                            {
                                if (command == "Classic" || command == "Ext1" || command == "Ext2" ||
                                    command == "Ext3" || command == "")
                                {
                                    Interpreter.BFVersion language = Interpreter.BFVersion.Classic;

                                    if (command == "Ext1")
                                    {
                                        language = Interpreter.BFVersion.ExtT1;
                                    }
                                    else if (command == "Ext2")
                                    {
                                        language = Interpreter.BFVersion.ExtT2;
                                    }
                                    else if (command == "Ext3")
                                    {
                                        language = Interpreter.BFVersion.ExtT3;
                                    }

                                    Console.WriteLine("Enter code to run!");
                                    var code = Console.ReadLine();
                                    var interpreter = new Interpreter(code, () => (byte) Console.Read(),
                                        b => Console.Write((char) b), language);
                                    interpreter.Interpret();
                                }
                            }
                        }
                    }
                }
            }

            Process.GetCurrentProcess().Close();
        }
    }
}