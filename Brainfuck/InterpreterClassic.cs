#region usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Brainfuck
{
    internal class InterpreterClassic : IInterpreter
    {
        protected readonly Func<byte> input;
        protected readonly char[] instructions;
        protected readonly Action<byte> output;

        protected Dictionary<char, Action> actions;
        protected int instructionPointer;
        protected List<byte> mem;
        protected int pointer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        /// <param name="version">The version.</param>
        internal InterpreterClassic(string instructions, Func<byte> input, Action<byte> output)
        {
            this.instructions = instructions.ToCharArray();
            this.input = input;
            this.output = output;

            InitActions();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        internal InterpreterClassic(string instructions)
        {
            this.instructions = instructions.ToCharArray();
            InitActions();
        }

        public virtual void Interpret()
        {
            instructionPointer = 0;
            pointer = 0;
            mem = new List<byte>(Enumerable.Repeat((byte) 0, 64));

            ProtectedInterpret();
        }

        private void InitActions()
        {
            actions = new Dictionary<char, Action>
            {
                {
                    '>', () =>
                    {
                        pointer++;

                        if (pointer == mem.Count) mem.AddRange(Enumerable.Repeat((byte) 0, 64));
                    }
                },
                {'<', () => pointer--},
                {'+', () => mem[pointer]++},
                {'-', () => mem[pointer]--},
                {
                    '.', () => { output?.Invoke(mem[pointer]); }
                },
                {
                    ',', () =>
                    {
                        if (input != null) mem[pointer] = input();
                    }
                },
                {
                    '[', () =>
                    {
                        if (mem[pointer] == 0)
                        {
                            var loopLevel = 0;
                            for (var index = instructionPointer + 1; index < instructions.Length; index++)
                            {
                                if (instructions[index] == '[')
                                    loopLevel++;
                                else if (instructions[index] == ']') loopLevel--;

                                if (loopLevel < 0)
                                {
                                    instructionPointer = index;
                                    break;
                                }
                            }
                        }
                    }
                },
                {
                    ']', () =>
                    {
                        if (mem[pointer] != 0)
                        {
                            var loopLevel = 0;
                            for (var index = instructionPointer - 1; index > 0; index--)
                            {
                                if (instructions[index] == ']')
                                    loopLevel++;
                                else if (instructions[index] == '[') loopLevel--;

                                if (loopLevel < 0)
                                {
                                    instructionPointer = index;

                                    break;
                                }
                            }
                        }
                    }
                }
            };
        }

        protected void ProtectedInterpret()
        {
            for (instructionPointer = 0; instructionPointer < instructions.Length; instructionPointer++)
            {
                if (actions.ContainsKey(instructions[instructionPointer])) actions[instructions[instructionPointer]]();
            }
        }

        public override string ToString()
        {
            return "Memory: [" + mem.Count + "/" + mem.Capacity + "]" + string.Join(";", mem) + Environment.NewLine +
                   "InstructionPointer: " + instructionPointer + "; Pointer: " +
                   pointer;
        }
    }
}