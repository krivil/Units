using System;
using System.Globalization;

namespace Units
{
    public readonly partial struct ByteSize
    {
        private const long OneKiloByte = 1000;
        private const long OneMegaByte = 1000 * OneKiloByte;
        private const long OneGigaByte = 1000 * OneMegaByte;
        private const long OneTeraByte = 1000 * OneGigaByte;
        private const long OnePetaByte = 1000 * OneTeraByte;
        private const long OneExaByte = 1000 * OnePetaByte;

        public static ByteSize FromKiloBytes(double value)
        {
            return new ByteSize((long)(value * OneKiloByte));
        }

        public static ByteSize FromMegaBytes(double value)
        {
            return new ByteSize((long)(value * OneMegaByte));
        }

        public static ByteSize FromGigaBytes(double value)
        {
            return new ByteSize((long)(value * OneGigaByte));
        }

        public static ByteSize FromTeraBytes(double value)
        {
            return new ByteSize((long)(value * OneTeraByte));
        }

        public static ByteSize FromPetaBytes(double value)
        {
            return new ByteSize((long)(value * OnePetaByte));
        }

        public ByteSize AddKiloBytes(double value)
        {
            return new ByteSize((long)(value * OneKiloByte) + _bytes);
        }

        public ByteSize AddMegaBytes(double value)
        {
            return new ByteSize((long)(value * OneMegaByte) + _bytes);
        }

        public ByteSize AddGigaBytes(double value)
        {
            return new ByteSize((long)(value * OneGigaByte) + _bytes);
        }

        public ByteSize AddTeraBytes(double value)
        {
            return new ByteSize((long)(value * OneTeraByte) + _bytes);
        }

        public ByteSize AddPetaBytes(double value)
        {
            return new ByteSize((long)(value * OnePetaByte) + _bytes);
        }

        public string ToStringWithDecimalPrefixedShortUnitName(string? format = null, IFormatProvider? provider = null)
        {
            return ToStringWithDecimalPrefixedUnitName(format, provider, true);
        }

        public string ToStringWithDecimalPrefixedLongUnitName(string? format = null, IFormatProvider? provider = null)
        {
            return ToStringWithDecimalPrefixedUnitName(format, provider, true);
        }

        public string ToStringWithDecimalPrefixedUnitName(string? format = null, IFormatProvider? provider = null, bool useShortUnitName = true)
        {
            provider ??= CultureInfo.CurrentCulture;

            return _bytes switch
            {
                var b when b >= OneExaByte =>
                    (b / (double)OneExaByte).ToString(format, provider) + (useShortUnitName ? " EB" : b == OneExaByte ? " exabyte" : " exabytes"),
                var b when b >= OnePetaByte =>
                    (b / (double)OnePetaByte).ToString(format, provider) + (useShortUnitName ? " PB" : b == OnePetaByte ? " petabyte" : " petabytes"),
                var b when b >= OneTeraByte =>
                    (b / (double)OneTeraByte).ToString(format, provider) + (useShortUnitName ? " TB" : b == OneTeraByte ? " terabyte" : " terabytes"),
                var b when b >= OneGigaByte =>
                    (b / (double)OneGigaByte).ToString(format, provider) + (useShortUnitName ? " GB" : b == OneGigaByte ? " gigabyte" : " gigabytes"),
                var b when b >= OneMegaByte =>
                    (b / (double)OneMegaByte).ToString(format, provider) + (useShortUnitName ? " MB" : b == OneMegaByte ? " megabyte" : " megabytes"),
                var b when b >= OneKiloByte =>
                    (b / (double)OneKiloByte).ToString(format, provider) + (useShortUnitName ? " kB" : b == OneKiloByte ? " kilobyte" : " kilobytes"),
                var b =>
                b.ToString(format, provider) + (useShortUnitName ? " B" : b == 1 ? " byte" : " bytes")
            };
        }
    }
}
