namespace Units.Tests {
    using System;
    using System.Globalization;
    using Xunit;

    public class UnitTests {
        [Fact]
        public void Parse() {
            ByteSize oneAndAHalfKilobytes = 1500;// ByteSize.FromKiloBytes(1.5);
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(" 1.5 kB", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("1.5 kB ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(" 1.5 kB ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("1.5\tkB", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("\t1.5 kB\t", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("    1.5     kB     ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(oneAndAHalfKilobytes.ToString(), CultureInfo.CurrentCulture));
        }
    }
}
