namespace Units {
    using System;
    using System.Buffers;
    using System.Globalization;

    public readonly partial struct ByteSize : IEquatable<ByteSize>, IComparable<ByteSize> {

        public static ByteSize Parse(string text, IFormatProvider provider) => Parse(text.AsSpan(), provider);

        public static ByteSize Parse(ReadOnlySpan<char> span, IFormatProvider provider) {
            ReadOnlySpan<char> trimmedFromStart = SkipWhitespace(span);
            ReadOnlySpan<char> doublePart = TakeUntilWhitespace(trimmedFromStart);

            var doubleValue = double.Parse(doublePart.ToString(), provider);

            ReadOnlySpan<char> afterDouble = trimmedFromStart.Slice(doublePart.Length);
            ReadOnlySpan<char> unitPart = TakeUntilWhitespace(SkipWhitespace(afterDouble));

            // Slower

            //return (unitPart.ToString()) switch
            //{
            //    var empty when string.IsNullOrEmpty(empty) => new ByteSize((long)doubleValue),

            //    var kb when string.Equals(kb, "kb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(kb, "kilobyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(kb, "kilobytes", StringComparison.OrdinalIgnoreCase)
            //    => FromKiloBytes(doubleValue),

            //    var kib when string.Equals(kib, "kib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(kib, "kibibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(kib, "kibibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromKibiBytes(doubleValue),

            //    var mb when string.Equals(mb, "mb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(mb, "megabyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(mb, "megabytes", StringComparison.OrdinalIgnoreCase)
            //    => FromMegaBytes(doubleValue),

            //    var mib when string.Equals(mib, "mib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(mib, "mebibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(mib, "mebibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromMebiBytes(doubleValue),

            //    var gb when string.Equals(gb, "gb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(gb, "gigabyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(gb, "gigabytes", StringComparison.OrdinalIgnoreCase)
            //    => FromGigaBytes(doubleValue),

            //    var gib when string.Equals(gib, "gib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(gib, "gibibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(gib, "gibibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromGibiBytes(doubleValue),

            //    var tb when string.Equals(tb, "tb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(tb, "terabyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(tb, "terabytes", StringComparison.OrdinalIgnoreCase)
            //    => FromTeraBytes(doubleValue),

            //    var tib when string.Equals(tib, "tib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(tib, "tebibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(tib, "tebibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromTebiBytes(doubleValue),

            //    var pb when string.Equals(pb, "pb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(pb, "petabyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(pb, "petabytes", StringComparison.OrdinalIgnoreCase)
            //    => FromPetaBytes(doubleValue),

            //    var pib when string.Equals(pib, "pib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(pib, "pebibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(pib, "pebibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromPebiBytes(doubleValue),

            //    var eb when string.Equals(eb, "eb", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(eb, "exabyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(eb, "exabytes", StringComparison.OrdinalIgnoreCase)
            //    => FromPetaBytes(1000 * doubleValue),

            //    var eib when string.Equals(eib, "eib", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(eib, "exbibyte", StringComparison.OrdinalIgnoreCase)
            //    || string.Equals(eib, "exbibytes", StringComparison.OrdinalIgnoreCase)
            //    => FromPebiBytes(1024 * doubleValue),

            //    _ => throw new ArgumentException("Unknown unit.", nameof(span)),
            //};

            switch (ToStringLower(unitPart)) {
                case null:
                case "":
                    return new ByteSize((long)doubleValue);
                case "kb":
                case "kilobyte":
                case "kilobytes":
                    return FromKiloBytes(doubleValue);
                case "kib":
                case "kibibyte":
                case "kibibytes":
                    return FromKibiBytes(doubleValue);
                case "mb":
                case "megabyte":
                case "megabytes":
                    return FromMegaBytes(doubleValue);
                case "mib":
                case "mebibyte":
                case "mebibytes":
                    return FromMebiBytes(doubleValue);
                case "gb":
                case "gigabyte":
                case "gigabytes":
                    return FromGigaBytes(doubleValue);
                case "gib":
                case "gibibyte":
                case "gibibytes":
                    return FromGibiBytes(doubleValue);
                case "tb":
                case "terabyte":
                case "terabytes":
                    return FromTeraBytes(doubleValue);
                case "tib":
                case "tebibyte":
                case "tebibytes":
                    return FromTebiBytes(doubleValue);
                case "pb":
                case "petabyte":
                case "petabytes":
                    return FromPetaBytes(doubleValue);
                case "pib":
                case "pebibyte":
                case "pebibytes":
                    return FromPebiBytes(doubleValue);
                case "eb":
                case "exabyte":
                case "exabytes":
                    return FromPetaBytes(1000 * doubleValue);
                case "eib":
                case "exbibyte":
                case "exbibytes":
                    return FromPebiBytes(1024 * doubleValue);
                default:
                    throw new ArgumentException("Unknown unit.", nameof(span));
            }

            static ReadOnlySpan<char> SkipWhitespace(ReadOnlySpan<char> chars) {
                int start = 0;
                for (int i = 0; i < chars.Length; i++) {
                    if (char.IsWhiteSpace(chars[i])) {
                        start++;
                    } else {
                        return chars.Slice(start, chars.Length - start);
                    }
                }
                
                return ReadOnlySpan<char>.Empty;
            }

            static ReadOnlySpan<char> TakeUntilWhitespace(ReadOnlySpan<char> chars) {
                for (int i = 0; i < chars.Length; i++) {
                    if (char.IsWhiteSpace(chars[i])) {
                        return chars.Slice(0, i);
                    }
                }
                
                return chars;
            }

            static string ToStringLower(ReadOnlySpan<char> chars) {

                if (chars.Length == 0) return string.Empty;

                char[]? buffer = ArrayPool<char>.Shared.Rent(chars.Length);
                
                try {
                    Span<char> span = ((Span<char>)buffer).Slice(0, chars.Length);
                    
                    for (int i = 0; i < span.Length; i++) {
                        span[i] = char.ToLowerInvariant(chars[i]);
                    }

                    return span.ToString();
                } finally {
                    ArrayPool<char>.Shared.Return(buffer);
                }
            }

        }

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