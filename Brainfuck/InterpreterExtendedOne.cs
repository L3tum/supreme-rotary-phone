#region usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Brainfuck
{
    internal class InterpreterExtendedOne : InterpreterClassic
    {
        private bool halt;
        private byte storage;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        internal InterpreterExtendedOne(string instructions, Func<byte> input, Action<byte> output) : base(instructions, input, output)
        {
            InitActions();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Interpreter" /> class.
        /// </summary>
        /// <param name="instructions">The instructions.</param>
        internal InterpreterExtendedOne(string instructions) : base(instructions)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions = actions.Concat(new Dictionary<char, Action>
            {
                {
                    '@', () => { halt = true; }
                },
                {'$', () => { storage = mem[pointer]; }},
                {'!', () => { mem[pointer] = storage; }},
                {
                    '}', () => { mem[pointer] = (byte) ((uint) mem[pointer] >> 1); }
                },
                {
                    '{', () => { mem[pointer] = (byte) ((uint) mem[pointer] << 1); }
                },
                {
                    '~', () => { mem[pointer] = (byte) ~mem[pointer]; }
                },
                {
                    '^', () => { mem[pointer] = (byte) (mem[pointer] ^ storage); }
                },
                {
                    '&', () => { mem[pointer] = (byte) (mem[pointer] & storage); }
                },
                {
                    '|', () => { mem[pointer] = (byte) (mem[pointer] | storage); }
                }
            }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public override void Interpret()
        {
            instructionPointer = 0;
            pointer = 0;
            storage = 0;
            mem = new List<byte>(Enumerable.Repeat((byte)0, 64));

            ProtectedInterpret();
        }

        public override string ToString()
        { 
            return base.ToString() + Environment.NewLine + "Storage: " + storage;
        }
    }
}