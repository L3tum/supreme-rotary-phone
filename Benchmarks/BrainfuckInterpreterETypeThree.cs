using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using Brainfuck;

namespace Benchmarks
{
    [HtmlExporter]
    [MemoryDiagnoser]
    [MinColumn]
    [MaxColumn]
    public class BrainfuckInterpreterETypeThree
    {
        // 6 steps báck
        private const string srcCode =
            ">5--------.7-----------.+++++++..+++.<2.5+++++++.>.+++.------.--------.2+.";

        private Interpreter interpreter;

        [GlobalSetup(Target = "OnlyInterpret")]
        public void Init()
        {
            interpreter = new Interpreter(srcCode, Interpreter.BFVersion.ExtT3);
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
            Interpreter interpret = new Interpreter(srcCode, Interpreter.BFVersion.ExtT3);

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
            Interpreter inter = new Interpreter(srcCode, Interpreter.BFVersion.ExtT3);
        }
    }
}
