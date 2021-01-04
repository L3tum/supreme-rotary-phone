#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Brainfuck
{
    internal class InterpreterExtendedThree : InterpreterExtendedTwo
    {
        private int beforeXPointer = -1;
        private bool halt;
        private List<int> lockedCells;
        private byte storage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        internal InterpreterExtendedThree(string instructions, Func<byte> input, Action<byte> output) : base(
            instructions,
            input, output)
        {
            InitActions();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        internal InterpreterExtendedThree(string instructions) : base(instructions)
        {
            InitActions();
        }

        public override void Interpret()
        {
            storage = 0;
            memoryPointer = 0;
            lockedCells = new List<int>();

            new Dictionary<char, Action>
            {
                {
                    'X', () =>
                    {
                        beforeXPointer = pointer;
                        pointer = instructionPointer;
                    }
                },
                {
                    'x', () =>
                    {
                        if (beforeXPointer == -1)
                        {
                            pointer = 0;
                        }
                        else
                        {
                            pointer = beforeXPointer;
                        }
                    }
                },
                {
                    'M', () => { memoryPointer = pointer; }
                },
                {
                    'm', () => { memoryPointer = 0; }
                },
                {
                    'L', () => { lockedCells.Add(pointer); }
                },
                {
                    'l', () => { lockedCells.Remove(pointer); }
                },
                {
                    ':', () =>
                    {
                        var signed = (sbyte) mem[pointer];

                        pointer += signed;
                    }
                },
                {
                    '#', () =>
                    {
                        instructionPointer++;

                        while (instructions[instructionPointer] != '#' && instructionPointer < instructions.Length)
                        {
                            instructionPointer++;
                        }
                    }
                },
                {
                    '1', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 1 * 16;
                    }
                },
                {
                    '2', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 2 * 16;
                    }
                },
                {
                    '3', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 3 * 16;
                    }
                },
                {
                    '4', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 4 * 16;
                    }
                },
                {
                    '5', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 5 * 16;
                    }
                },
                {
                    '6', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 6 * 16;
                    }
                },
                {
                    '7', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 7 * 16;
                    }
                },
                {
                    '8', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 8 * 16;
                    }
                },
                {
                    '9', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 9 * 16;
                    }
                },
                {
                    'A', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 10 * 16;
                    }
                },
                {
                    'B', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 11 * 16;
                    }
                },
                {
                    'C', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 12 * 16;
                    }
                },
                {
                    'D', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 13 * 16;
                    }
                },
                {
                    'E', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 14 * 16;
                    }
                },
                {
                    'F', () =>
                    {
                        if (!lockedCells.Contains(pointer)) pointer = 15 * 16;
                    }
                }
            };
            base.Interpret();
        }

        private void InitActions()
        {
            actions['$'] = () =>
            {
                if (!lockedCells.Contains(0)) mem[0] = mem[pointer];
            };
            actions['!'] = () =>
            {
                if (!lockedCells.Contains(pointer)) mem[pointer] = mem[0];
            };
            actions['^'] = () =>
            {
                if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] ^ mem[0]);
            };
            actions['&'] = () =>
            {
                if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] & mem[0]);
            };
            actions['|'] = () =>
            {
                if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] | mem[0]);
            };

            var newActions = new Dictionary<char, Action>
            {
                {'?', () => instructionPointer = pointer - 1},
                {
                    ')', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem.Insert(pointer, 0);
                    }
                },
                {
                    '(', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem.RemoveAt(pointer);
                    }
                },
                {
                    '*', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] * mem[0]);
                    }
                },
                {
                    '/', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] / mem[0]);
                    }
                },
                {
                    '=', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] + mem[0]);
                    }
                },
                {
                    '_', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] - mem[0]);
                    }
                },
                {
                    '%', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] % mem[0]);
                    }
                },
                {
                    '!', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = storage;
                    }
                },
                {
                    '}', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) ((uint) mem[pointer] >> 1);
                    }
                },
                {
                    '{', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) ((uint) mem[pointer] << 1);
                    }
                },
                {
                    '~', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) ~mem[pointer];
                    }
                },
                {
                    '^', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] ^ storage);
                    }
                },
                {
                    '&', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] & storage);
                    }
                },
                {
                    '|', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer] = (byte) (mem[pointer] | storage);
                    }
                },
                {
                    '+', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer]++;
                    }
                },
                {
                    '-', () =>
                    {
                        if (!lockedCells.Contains(pointer)) mem[pointer]--;
                    }
                },
                {
                    ',', () =>
                    {
                        if (input != null && !lockedCells.Contains(pointer)) mem[pointer] = input();
                    }
                }
            };

            foreach (KeyValuePair<char, Action> action in newActions)
            {
                actions[action.Key] = action.Value;
            }
        }

        public override string ToString()
        {
            return "Memory: [" + mem.Count + "/" + mem.Capacity + "]" + string.Join(";", mem) +
                   Environment.NewLine +
                   "InstructionPointer: " + instructionPointer + "; Pointer: " +
                   pointer + Environment.NewLine + "Storage: " + mem[0];
        }
    }
}