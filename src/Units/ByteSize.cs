using System;

namespace Units
{
    public readonly partial struct ByteSize : IEquatable<ByteSize>
    {
        public static readonly ByteSize MinValue = new ByteSize(long.MinValue);
        public static readonly ByteSize MaxValue = new ByteSize(long.MaxValue);

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
        public static implicit operator long(ByteSize bytes) => bytes._bytes;
        public static explicit operator int(ByteSize bytes) => (int)bytes._bytes;
        public static implicit operator string(ByteSize bytes) => bytes.ToString();

        private readonly long _bytes;

        private ByteSize(long bytes)
        {
            _bytes = bytes;
        }

        public ByteSize Add(ByteSize bs)
        {
            return new ByteSize(this._bytes + bs._bytes);
        }

        public ByteSize AddBytes(long value)
        {
            return new ByteSize(this._bytes + value);
        }

        public bool Equals(ByteSize other)
        {
            return _bytes == other._bytes;
        }

        public override bool Equals(object obj)
        {
            return obj is ByteSize other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _bytes.GetHashCode();
        }

        public override string ToString()
        {
            return ToStringWithDecimalPrefixedUnitName("0.##");
        }

        public string ToString(string? format)
        {
            return ToStringWithDecimalPrefixedUnitName(format);
        }

        public string ToString(IFormatProvider? provider)
        {
            return ToStringWithDecimalPrefixedUnitName(provider: provider);
        }

        public string ToString(string? format, IFormatProvider? provider)
        {
            return ToStringWithDecimalPrefixedUnitName(format, provider);
        }

        public string ToString(string? format, IFormatProvider? provider, bool useBinaryUnitNamePrefix)
        {
            return useBinaryUnitNamePrefix
                ? ToStringWithBinaryPrefixedUnitName(format, provider)
                : ToStringWithDecimalPrefixedUnitName(format, provider);
        }

        public string ToString(string? format, IFormatProvider? provider, bool useBinaryUnitNamePrefix, bool useShortUnitName)
        {
            return useBinaryUnitNamePrefix
                ? ToStringWithBinaryPrefixedUnitName(format, provider, useShortUnitName)
                : ToStringWithDecimalPrefixedUnitName(format, provider, useShortUnitName);
        }
    }
}