using System;
using System.Globalization;

namespace Units
{
    public readonly partial struct ByteSize
    {
        private const long OneKibiByte = 1024;
        private const long OneMebiByte = 1024 * OneKibiByte;
        private const long OneGibiByte = 1024 * OneMebiByte;
        private const long OneTebiByte = 1024 * OneGibiByte;
        private const long OnePebiByte = 1024 * OneTebiByte;
        private const long OneExbiByte = 1024 * OnePebiByte;

        public static ByteSize FromKibiBytes(double value)
        {
            return new ByteSize((long)(value * OneKibiByte));
        }

        public static ByteSize FromMebiBytes(double value)
        {
            return new ByteSize((long)(value * OneMebiByte));
        }

        public static ByteSize FromGibiBytes(double value)
        {
            return new ByteSize((long)(value * OneGibiByte));
        }

        public static ByteSize FromTebiBytes(double value)
        {
            return new ByteSize((long)(value * OneTebiByte));
        }

        public static ByteSize FromPebiBytes(double value)
        {
            return new ByteSize((long)(value * OnePebiByte));
        }

        public ByteSize AddKibiBytes(double value)
        {
            return new ByteSize((long)(value * OneKibiByte) + _bytes);
        }

        public ByteSize AddMebiBytes(double value)
        {
            return new ByteSize((long)(value * OneMebiByte) + _bytes);
        }

        public ByteSize AddGibiBytes(double value)
        {
            return new ByteSize((long)(value * OneGibiByte) + _bytes);
        }

        public ByteSize AddTebiBytes(double value)
        {
            return new ByteSize((long)(value * OneTebiByte) + _bytes);
        }

        public ByteSize AddPebiBytes(double value)
        {
            return new ByteSize((long)(value * OnePebiByte) + _bytes);
        }

        public string ToStringWithBinaryPrefixedShortUnitName(string? format = null, IFormatProvider? provider = null)
        {
            return ToStringWithBinaryPrefixedUnitName(format, provider, true);
        }

        public string ToStringWithBinaryPrefixedLongUnitName(string? format = null, IFormatProvider? provider = null)
        {
            return ToStringWithBinaryPrefixedUnitName(format, provider, false);
        }

        public string ToStringWithBinaryPrefixedUnitName(string? format = null, IFormatProvider? provider = null, bool useShortUnitName = true)
        {
            provider ??= CultureInfo.CurrentCulture;

            return _bytes switch
            {
                var b when b >= OneExbiByte =>
                    (b / (double)OneExbiByte).ToString(format, provider) + (useShortUnitName ? " EiB" : b == OneExbiByte ? " exbibyte" : " exbibytes"),
                var b when b >= OnePebiByte =>
                    (b / (double)OnePebiByte).ToString(format, provider) + (useShortUnitName ? " PiB" : b == OnePebiByte ? " pebibyte" : " pebibytes"),
                var b when b >= OneTebiByte =>
                    (b / (double)OneTebiByte).ToString(format, provider) + (useShortUnitName ? " TiB" : b == OneTebiByte ? " tebibyte" : " tebibytes"),
                var b when b >= OneGibiByte =>
                    (b / (double)OneGibiByte).ToString(format, provider) + (useShortUnitName ? " GiB" : b == OneGibiByte ? " gibibyte" : " gibibytes"),
                var b when b >= OneMebiByte =>
                    (b / (double)OneMebiByte).ToString(format, provider) + (useShortUnitName ? " MiB" : b == OneMebiByte ? " mebibyte" : " mebibytes"),
                var b when b >= OneKibiByte =>
                    (b / (double)OneKibiByte).ToString(format, provider) + (useShortUnitName ? " KiB" : b == OneKibiByte ? " kibibyte" : " kibibytes"),
                var b =>
                    b.ToString(format, provider) + (useShortUnitName ? " B" : b == 1 ? " byte" : " bytes")
            };
        }
    }
}
