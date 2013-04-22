using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace KilnApi.Helpers {
    /// <summary>
    /// Provides handy methods to work with SecureString.
    /// </summary>
    public static class SecureStringHelper {
        /// <summary>
        /// Initialize secure string with given string.
        /// </summary>
        /// <param name="source">Source string value.</param>
        /// <param name="target">The secure string that must be initialized.</param>
        public static void InitializeSecureString(string source, out SecureString target) {
            target = new SecureString();
            foreach (char c in source)
                target.AppendChar(c);
        }

        /// <summary>
        /// Decrypt the content of a secure string.
        /// </summary>
        /// <param name="secureString">Secure string that needs to be read.</param>
        /// <returns>Decrypted string.</returns>
        public unsafe static string Decrypt(this SecureString secureString) {
            // Create buffer for result string.
            StringBuilder result = new StringBuilder();
            char* pChar = null;
            try {
                // Decrypt secure string into an unmanaged memory buffer.
                pChar = (char*)Marshal.SecureStringToCoTaskMemUnicode(secureString);

                // Access the unmanaged memory buffer
                // where decrypted secure string is stored.
                for (int i = 0; pChar[i] != 0; i++)
                    result.Append(pChar[i]);
            }
            finally {
                // Zero and free the unmanaged memory buffer
                // where decrypted secure string is stored.
                if (pChar != null)
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pChar);
            }
            return result.ToString();
        }
    }
}