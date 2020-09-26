namespace Units {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public readonly partial struct ByteSize {
        public static readonly Dictionary<string, long> DefaultMatchesForUnitsOfMeasure =
            new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase) {
                ["kb"] = _oneKiloByte,
                ["kilobyte"] = _oneKiloByte,
                ["kilobytes"] = _oneKiloByte,

                ["kib"] = _oneKibiByte,
                ["kibibyte"] = _oneKibiByte,
                ["kibibytes"] = _oneKibiByte,

                ["mb"] = _oneMegaByte,
                ["megabyte"] = _oneMegaByte,
                ["megabytes"] = _oneMegaByte,

                ["mib"] = _oneMebiByte,
                ["mebibyte"] = _oneMebiByte,
                ["mebibytes"] = _oneMebiByte,

                ["gb"] = _oneGigaByte,
                ["gigabyte"] = _oneGigaByte,
                ["gigabytes"] = _oneGigaByte,

                ["gib"] = _oneGibiByte,
                ["gibibyte"] = _oneGibiByte,
                ["gibibytes"] = _oneGibiByte,

                ["tb"] = _oneTeraByte,
                ["terabyte"] = _oneTeraByte,
                ["terabytes"] = _oneTeraByte,

                ["tib"] = _oneTebiByte,
                ["tebibyte"] = _oneTebiByte,
                ["tebibytes"] = _oneTebiByte,

                ["pb"] = _onePetaByte,
                ["petabyte"] = _onePetaByte,
                ["petabytes"] = _onePetaByte,

                ["pib"] = _onePebiByte,
                ["pebibyte"] = _onePebiByte,
                ["pebibytes"] = _onePebiByte,

                ["eb"] = _oneExaByte,
                ["exabyte"] = _oneExaByte,
                ["exabytes"] = _oneExaByte,

                ["eib"] = _oneExbiByte,
                ["exbibyte"] = _oneExbiByte,
                ["exbibytes"] = _oneExbiByte,
            };

        public static bool TryParse(string s, out ByteSize size) =>
            TryParse(s.AsSpan(), CultureInfo.CurrentCulture, out size, DefaultMatchesForUnitsOfMeasure);

        public static bool TryParse(string s, IFormatProvider provider, out ByteSize size) =>
            TryParse(s.AsSpan(), provider, out size, DefaultMatchesForUnitsOfMeasure);

        public static bool TryParse(string s, IFormatProvider provider, out ByteSize size,
            IDictionary<string, long> unitsOfMeasure) =>
            TryParse(s.AsSpan(), provider, out size, unitsOfMeasure);

        public static bool TryParse(ReadOnlySpan<char> span, IFormatProvider provider, out ByteSize size, IDictionary<string, long> unitsOfMeasure) {
            ReadOnlySpan<char> trimmedFromStart = SkipWhitespace(span);
            ReadOnlySpan<char> doublePart = TakeUntilWhitespace(trimmedFromStart, out ReadOnlySpan<char> afterDouble);

            ReadOnlySpan<char> unitPart = TakeUntilWhitespace(SkipWhitespace(afterDouble), out _);

            if (!double.TryParse(doublePart.ToString(), NumberStyles.Any, provider, out var parsedDouble)) {
                size = ZeroValue;
                return false;
            }

            string unit = unitPart.ToString();
            long multiplier;

            if (unitsOfMeasure.ContainsKey(unit)) {
                multiplier = unitsOfMeasure[unit];
            } else if (string.IsNullOrEmpty(unit)) {
                multiplier = 1;
            } else {
                size = ZeroValue;
                return false;
            }

            size = (long)(parsedDouble * multiplier);
            return true;
        }


        public static ByteSize Parse(string text) =>
            Parse(text.AsSpan(), CultureInfo.CurrentCulture, DefaultMatchesForUnitsOfMeasure);

        public static ByteSize Parse(string text, IFormatProvider provider) =>
            Parse(text.AsSpan(), provider, DefaultMatchesForUnitsOfMeasure);

        public static ByteSize Parse(string text, IFormatProvider provider, IDictionary<string, long> unitsOfMeasure) =>
            Parse(text.AsSpan(), provider, unitsOfMeasure);

        public static ByteSize Parse(ReadOnlySpan<char> span, IFormatProvider provider, IDictionary<string, long> unitsOfMeasure) {
            ReadOnlySpan<char> trimmedFromStart = SkipWhitespace(span);
            ReadOnlySpan<char> doublePart = TakeUntilWhitespace(trimmedFromStart, out ReadOnlySpan<char> afterDouble);

            var doubleValue = double.Parse(doublePart.ToString(), provider);

            ReadOnlySpan<char> unitPart = TakeUntilWhitespace(SkipWhitespace(afterDouble), out _);

            string unit = unitPart.ToString();
            long multiplier;

            if (unitsOfMeasure.ContainsKey(unit)) {
                multiplier = unitsOfMeasure[unit];
            } else if (string.IsNullOrEmpty(unit)) {
                multiplier = 1;
            } else {
                throw new ArgumentException("Unknown unit.", nameof(span));
            }

            return (long)(doubleValue * multiplier);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlySpan<char> SkipWhitespace(ReadOnlySpan<char> chars) {
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlySpan<char> TakeUntilWhitespace(ReadOnlySpan<char> chars, out ReadOnlySpan<char> rest) {
            for (int i = 0; i < chars.Length; i++) {
                if (char.IsWhiteSpace(chars[i])) {
                    rest = chars.Slice(i);
                    return chars.Slice(0, i);
                }
            }

            rest = ReadOnlySpan<char>.Empty;
            return chars;
        }
    }
}