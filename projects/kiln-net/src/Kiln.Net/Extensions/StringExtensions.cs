using System;
using System.Globalization;
using System.Text;

namespace KilnApi {
    public static class StringExtensions {
        /// <summary>
        /// Decode hexadecimal encoded string to UTF-8 string.
        /// </summary>
        public static string FromHexEncoded(this string str) {
            if (str.Length % 2 != 0) {
                throw new ArgumentException("Length of hex-encoded UTF-8 string should be multiple of 2");
            }

            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2) {
                bytes[i / 2] = byte.Parse(str.Substring(i, 2), NumberStyles.HexNumber);
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
