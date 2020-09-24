namespace Units {
    using System;
    using System.Globalization;

    public readonly partial struct ByteSize {
        private const long _oneKibiByte = 1024;
        private const long _oneMebiByte = 1024 * _oneKibiByte;
        private const long _oneGibiByte = 1024 * _oneMebiByte;
        private const long _oneTebiByte = 1024 * _oneGibiByte;
        private const long _onePebiByte = 1024 * _oneTebiByte;
        private const long _oneExbiByte = 1024 * _onePebiByte;

        public static ByteSize FromKibiBytes(double value) => new ByteSize((long)(value * _oneKibiByte));

        public static ByteSize FromMebiBytes(double value) => new ByteSize((long)(value * _oneMebiByte));

        public static ByteSize FromGibiBytes(double value) => new ByteSize((long)(value * _oneGibiByte));

        public static ByteSize FromTebiBytes(double value) => new ByteSize((long)(value * _oneTebiByte));

        public static ByteSize FromPebiBytes(double value) => new ByteSize((long)(value * _onePebiByte));

        public ByteSize AddKibiBytes(double value) => new ByteSize((long)(value * _oneKibiByte) + _bytes);

        public ByteSize AddMebiBytes(double value) => new ByteSize((long)(value * _oneMebiByte) + _bytes);

        public ByteSize AddGibiBytes(double value) => new ByteSize((long)(value * _oneGibiByte) + _bytes);

        public ByteSize AddTebiBytes(double value) => new ByteSize((long)(value * _oneTebiByte) + _bytes);

        public ByteSize AddPebiBytes(double value) => new ByteSize((long)(value * _onePebiByte) + _bytes);

        public double AsKibiBytes => (double)_bytes / _oneKibiByte;

        public double AsMebiBytes => (double)_bytes / _oneMebiByte;

        public double AsGibiBytes => (double)_bytes / _oneGibiByte;

        public double AsTebiBytes => (double)_bytes / _oneTebiByte;

        public double AsPebiBytes => (double)_bytes / _onePebiByte;

        public string ToStringWithBinaryPrefixedShortUnitName(string? format = null, IFormatProvider? provider = null) =>
            ToStringWithBinaryPrefixedUnitName(format, provider, true);

        public string ToStringWithBinaryPrefixedLongUnitName(string? format = null, IFormatProvider? provider = null) =>
            ToStringWithBinaryPrefixedUnitName(format, provider, false);

        public string ToStringWithBinaryPrefixedUnitName(string? format = null, IFormatProvider? provider = null, bool useShortUnitName = true) {
            provider ??= CultureInfo.CurrentCulture;

            return _bytes switch
            {
                var b when b >= _oneExbiByte =>
                    (b / (double)_oneExbiByte).ToString(format, provider) + (useShortUnitName ? " EiB" : b == _oneExbiByte ? " exbibyte" : " exbibytes"),
                var b when b >= _onePebiByte =>
                    (b / (double)_onePebiByte).ToString(format, provider) + (useShortUnitName ? " PiB" : b == _onePebiByte ? " pebibyte" : " pebibytes"),
                var b when b >= _oneTebiByte =>
                    (b / (double)_oneTebiByte).ToString(format, provider) + (useShortUnitName ? " TiB" : b == _oneTebiByte ? " tebibyte" : " tebibytes"),
                var b when b >= _oneGibiByte =>
                    (b / (double)_oneGibiByte).ToString(format, provider) + (useShortUnitName ? " GiB" : b == _oneGibiByte ? " gibibyte" : " gibibytes"),
                var b when b >= _oneMebiByte =>
                    (b / (double)_oneMebiByte).ToString(format, provider) + (useShortUnitName ? " MiB" : b == _oneMebiByte ? " mebibyte" : " mebibytes"),
                var b when b >= _oneKibiByte =>
                    (b / (double)_oneKibiByte).ToString(format, provider) + (useShortUnitName ? " KiB" : b == _oneKibiByte ? " kibibyte" : " kibibytes"),
                var b =>
                    b.ToString(format, provider) + (useShortUnitName ? " B" : b == 1 ? " byte" : " bytes")
            };
        }
    }
}
