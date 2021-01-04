#region usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Brainfuck
{
    internal class InterpreterExtendedTwo : InterpreterExtendedOne
    {
        protected int memoryPointer = 0;
        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        internal InterpreterExtendedTwo(string instructions, Func<byte> input, Action<byte> output) : base(instructions,
            input, output)
        {
            InitActions();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        internal InterpreterExtendedTwo(string instructions) : base(instructions)
        {
            InitActions();
        }

        public override void Interpret()
        {
            // Already adds one byte as initializer for storage
            mem = new List<byte> {0};

            var inst = string.Join("", instructions);

            if (inst.Contains('@') && !inst.EndsWith('@'))
            {
                var parts = inst.Split('@');

                mem.AddRange(parts[0].ToList().ConvertAll(c => (byte)c));
                mem.AddRange(parts[1].ToList().ConvertAll(c => (byte)c));
                pointer = parts[0].Length + 1;
            }
            else
            {
                mem.AddRange(inst.ToList().ConvertAll(c => (byte) c));
                mem.AddRange(Enumerable.Repeat((byte) 0, 64));
                pointer = instructions.Length + 1;
            }

            ProtectedInterpret();
        }

        private void InitActions()
        {
            actions['$'] = () => mem[memoryPointer] = mem[pointer];
            actions['!'] = () => mem[pointer] = mem[0];
            actions['^'] = () => mem[pointer] = (byte) (mem[pointer] ^ mem[memoryPointer]);
            actions['&'] = () => mem[pointer] = (byte) (mem[pointer] & mem[memoryPointer]);
            actions['|'] = () => mem[pointer] = (byte) (mem[pointer] | mem[memoryPointer]);

            actions = actions.Concat(new Dictionary<char, Action>
            {
                {'?', () => instructionPointer = pointer - 1},
                {')', () => mem.Insert(pointer, 0)},
                {'(', () => mem.RemoveAt(pointer)},
                {'*', () => mem[pointer] = (byte) (mem[pointer] * mem[memoryPointer])},
                {'/', () => mem[pointer] = (byte) (mem[pointer] / mem[memoryPointer])},
                {'=', () => mem[pointer] = (byte) (mem[pointer] + mem[memoryPointer])},
                {'_', () => mem[pointer] = (byte) (mem[pointer] - mem[memoryPointer])},
                {'%', () => mem[pointer] = (byte) (mem[pointer] % mem[memoryPointer])}
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
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