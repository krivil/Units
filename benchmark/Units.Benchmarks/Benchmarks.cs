namespace Units.Benchmarks {
    using System;
    using System.Globalization;
    using BenchmarkDotNet;
    using BenchmarkDotNet.Attributes;

    [MemoryDiagnoser]
    public class Benchmarks {
        [Params("1.5 kb", "1.5 kilobytes", "1.5 kilobyte")]
        public string Text { get; set; }

        [Benchmark]
        public void TryParse() {
            var result = ByteSize.TryParse(Text, CultureInfo.InvariantCulture, out var size,
                ByteSize.DefaultMatchesForUnitsOfMeasure);
        }

        [Benchmark]
        public void Parse() {
            var result = ByteSize.Parse(Text, CultureInfo.InvariantCulture);
        }
    }
}
