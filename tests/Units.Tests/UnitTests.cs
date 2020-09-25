namespace Units.Tests {
    using System;
    using System.Globalization;
    using Xunit;

    public class UnitTests {
        [Fact]
        public void Parse() {
            ByteSize oneAndAHalfKilobytes = 1500;// ByteSize.FromKiloBytes(1.5);
            ByteSize actual;

            Assert.True(ByteSize.TryParse(" 1.5 kB", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse("1.5 kB ", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse(" 1.5 kB ", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse("1.5\tkB", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse("\t1.5 kB\t", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse("    1.5     kB     ", CultureInfo.InvariantCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);
            Assert.True(ByteSize.TryParse(oneAndAHalfKilobytes.ToString(), CultureInfo.CurrentCulture, out actual));
            Assert.Equal(oneAndAHalfKilobytes, actual);


            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(" 1.5 kB", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("1.5 kB ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(" 1.5 kB ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("1.5\tkB", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("\t1.5 kB\t", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse("    1.5     kB     ", CultureInfo.InvariantCulture));
            Assert.Equal(oneAndAHalfKilobytes, ByteSize.Parse(oneAndAHalfKilobytes.ToString(), CultureInfo.CurrentCulture));
        }

        [Fact]
        public void TestDecimal() {
            var success = ByteSize.TryParse("1,555 kB", CultureInfo.GetCultureInfo("bg"), out ByteSize x);
            Assert.True(success);
            Assert.Equal((long)x, (long)ByteSize.FromKiloBytes(1.555));


            success = ByteSize.TryParse("1,555 megabytes", CultureInfo.GetCultureInfo("bg"), out x);
            Assert.True(success);
            Assert.Equal((long)x, (long)ByteSize.FromMegaBytes(1.555));
        }

        [Fact]
        public void TestDigital() {
            var success = ByteSize.TryParse("1,555 kiB", CultureInfo.GetCultureInfo("bg"), out ByteSize x);
            Assert.True(success);
            Assert.Equal((long)x, (long)ByteSize.FromKibiBytes(1.555));

            success = ByteSize.TryParse("1,555 mebibytes", CultureInfo.GetCultureInfo("bg"), out x);
            Assert.True(success);
            Assert.Equal((long)x, (long)ByteSize.FromMebiBytes(1.555));

            success = ByteSize.TryParse("1,111.555 mebibytes", CultureInfo.GetCultureInfo("en-US"), out x);
            Assert.True(success);
            Assert.Equal((long)x, (long)ByteSize.FromMebiBytes(1111.555));
        }
    }
}
