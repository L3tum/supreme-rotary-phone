#region usings

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Configs;
using Brainfuck;

#endregion

namespace Benchmarks
{
    [HtmlExporter]
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    public class BrainfuckInterpreterClassic
    {
        private const string srcCode =
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";

        private Interpreter interpreter;

        [GlobalSetup(Target = "OnlyInterpret")]
        public void Init()
        {
            interpreter = new Interpreter(srcCode);
        }

        [GlobalCleanup(Target = "OnlyInterpret")]
        public void Teardown()
        {
            interpreter = null;
            GC.Collect();
        }

        [Benchmark(Baseline = true)]
        public void StartToFinish()
        {
            Interpreter interpret = new Interpreter(srcCode);

            interpret.Interpret();
        }

        [Benchmark]
        public void OnlyInterpret()
        {
            interpreter.Interpret();
        }

        [Benchmark]
        public void OnlyInitializing()
        {
            Interpreter inter = new Interpreter(srcCode);
        }
    }
}