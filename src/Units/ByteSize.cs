namespace Units {
    using System;
    using System.Globalization;

    public readonly partial struct ByteSize : IEquatable<ByteSize>, IComparable<ByteSize> {
        public static readonly ByteSize MinValue = new ByteSize(long.MinValue);
        public static readonly ByteSize MaxValue = new ByteSize(long.MaxValue);
        public static readonly ByteSize ZeroValue = new ByteSize(0);

        public static ByteSize operator +(ByteSize b1, ByteSize b2) => new ByteSize(b1._bytes + b2._bytes);
        public static ByteSize operator +(ByteSize b1, long b2) => new ByteSize(b1._bytes + b2);
        public static ByteSize operator +(ByteSize b1, int b2) => new ByteSize(b1._bytes + b2);
        public static ByteSize operator ++(ByteSize b) => new ByteSize(b._bytes + 1);
        public static ByteSize operator -(ByteSize b) => new ByteSize(-b._bytes);
        public static ByteSize operator -(ByteSize b1, ByteSize b2) => new ByteSize(b1._bytes - b2._bytes);
        public static ByteSize operator -(ByteSize b1, long b2) => new ByteSize(b1._bytes - b2);
        public static ByteSize operator -(ByteSize b1, int b2) => new ByteSize(b1._bytes - b2);
        public static ByteSize operator --(ByteSize b) => new ByteSize(b._bytes - 1);
        public static bool operator ==(ByteSize b1, ByteSize b2) => b1._bytes == b2._bytes;
        public static bool operator !=(ByteSize b1, ByteSize b2) => b1._bytes != b2._bytes;
        public static bool operator <(ByteSize b1, ByteSize b2) => b1._bytes < b2._bytes;
        public static bool operator <=(ByteSize b1, ByteSize b2) => b1._bytes <= b2._bytes;
        public static bool operator >(ByteSize b1, ByteSize b2) => b1._bytes > b2._bytes;
        public static bool operator >=(ByteSize b1, ByteSize b2) => b1._bytes >= b2._bytes;
        public static implicit operator ByteSize(long bytes) => new ByteSize(bytes);
        public static implicit operator ByteSize(int bytes) => new ByteSize(bytes);
        public static implicit operator ByteSize(string text) => Parse(text, CultureInfo.CurrentCulture);
        public static implicit operator long(ByteSize bytes) => bytes._bytes;
        public static explicit operator int(ByteSize bytes) => (int)bytes._bytes;
        public static implicit operator string(ByteSize bytes) => bytes.ToString();

        private readonly long _bytes;

        private ByteSize(long bytes) {
            _bytes = bytes;
        }

        public ByteSize Add(ByteSize bs) => new ByteSize(_bytes + bs._bytes);

        public ByteSize AddBytes(long value) => new ByteSize(_bytes + value);

        public int CompareTo(ByteSize other) => _bytes.CompareTo(other._bytes);

        public bool Equals(ByteSize other) => _bytes == other._bytes;

        public override bool Equals(object obj) => obj is ByteSize other && Equals(other);

        public override int GetHashCode() => _bytes.GetHashCode();

        public override string ToString() =>
            ToStringWithDecimalPrefixedUnitName("0.##");

        public string ToString(string? format) =>
            ToStringWithDecimalPrefixedUnitName(format);

        public string ToString(IFormatProvider? provider) =>
            ToStringWithDecimalPrefixedUnitName(provider: provider);

        public string ToString(string? format, IFormatProvider? provider) =>
            ToStringWithDecimalPrefixedUnitName(format, provider);

        public string ToString(string? format, IFormatProvider? provider, bool useBinaryUnitNamePrefix) {
            return useBinaryUnitNamePrefix
                ? ToStringWithBinaryPrefixedUnitName(format, provider)
                : ToStringWithDecimalPrefixedUnitName(format, provider);
        }

        public string ToString(string? format, IFormatProvider? provider, bool useBinaryUnitNamePrefix, bool useShortUnitName) {
            return useBinaryUnitNamePrefix
                ? ToStringWithBinaryPrefixedUnitName(format, provider, useShortUnitName)
                : ToStringWithDecimalPrefixedUnitName(format, provider, useShortUnitName);
        }
    }
}