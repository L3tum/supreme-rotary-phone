#region usings

using System;

#endregion

namespace Brainfuck
{
    public class Interpreter : IInterpreter
    {
        public enum BFVersion
        {
            Classic,
            ExtT1,
            ExtT2,
            ExtT3
        }

        private readonly IInterpreter interpreter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="version">The version.</param>
        public Interpreter(string instructions, Func<byte> input, Action<byte> output,
            BFVersion version = BFVersion.Classic)
        {
            switch (version)
            {
                case BFVersion.ExtT1:
                {
                    interpreter = new InterpreterExtendedOne(instructions, input, output);
                    break;
                }

                case BFVersion.ExtT2:
                {
                    interpreter = new InterpreterExtendedTwo(instructions, input, output);
                    break;
                }

                default:
                {
                    interpreter = new InterpreterClassic(instructions, input, output);
                    break;
                }
            }

            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="version">The version.</param>
        public Interpreter(string instructions, BFVersion version = BFVersion.Classic)
        {
            switch (version)
            {
                case BFVersion.ExtT1:
                {
                    interpreter = new InterpreterExtendedOne(instructions);
                    break;
                }

                case BFVersion.ExtT2:
                {
                    interpreter = new InterpreterExtendedTwo(instructions);
                    break;
                }

                case BFVersion.ExtT3:
                {
                    interpreter = new InterpreterExtendedThree(instructions);
                    break;
                }

                default:
                {
                    interpreter = new InterpreterClassic(instructions);
                    break;
                }
            }

            Init();
        }

        public void Interpret()
        {
            interpreter.Interpret();
        }

        private void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CurrentDomainOnUnhandledException(object sender,
            UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Console.WriteLine();
            Console.WriteLine(interpreter.ToString());
        }
    }
}