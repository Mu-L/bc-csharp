﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Generators
{
    /// <summary>Password hashing scheme BCrypt.</summary>
    /// <remarks>
    /// Designed by Niels Provos and David Mazières, using the string format and the Base64 encoding of the reference
    /// implementation in OpenBSD. Passwords are encoded using UTF-8 when provided as char[]. Encoded passwords longer than
    /// 72 bytes are truncated and all remaining bytes are ignored.
    /// </remarks>
    // TODO[api] Make static
    public class OpenBsdBCrypt
    {
        private static readonly byte[] EncodingTable = // the Bcrypts encoding table for OpenBSD
        {
            (byte)'.', (byte)'/', (byte)'A', (byte)'B', (byte)'C', (byte)'D',
            (byte)'E', (byte)'F', (byte)'G', (byte)'H', (byte)'I', (byte)'J',
            (byte)'K', (byte)'L', (byte)'M', (byte)'N', (byte)'O', (byte)'P',
            (byte)'Q', (byte)'R', (byte)'S', (byte)'T', (byte)'U', (byte)'V',
            (byte)'W', (byte)'X', (byte)'Y', (byte)'Z', (byte)'a', (byte)'b',
            (byte)'c', (byte)'d', (byte)'e', (byte)'f', (byte)'g', (byte)'h',
            (byte)'i', (byte)'j', (byte)'k', (byte)'l', (byte)'m', (byte)'n',
            (byte)'o', (byte)'p', (byte)'q', (byte)'r', (byte)'s', (byte)'t',
            (byte)'u', (byte)'v', (byte)'w', (byte)'x', (byte)'y', (byte)'z',
            (byte)'0', (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5',
            (byte)'6', (byte)'7', (byte)'8', (byte)'9'
        };

        /*
         * set up the decoding table.
         */
        private static readonly byte[] DecodingTable = new byte[128];
        private static readonly string DefaultVersion = "2y";
        private static readonly HashSet<string> AllowedVersions = new HashSet<string>();

        static OpenBsdBCrypt()
        {
            // Presently just the Bcrypt versions.
            AllowedVersions.Add("2a");
            AllowedVersions.Add("2y");
            AllowedVersions.Add("2b");

            Arrays.Fill(DecodingTable, 0xFF);

            for (int i = 0; i < EncodingTable.Length; i++)
            {
                DecodingTable[EncodingTable[i]] = (byte)i;
            }
        }

        public OpenBsdBCrypt()
        {
        }

        /**
         * Creates a 60 character Bcrypt String, including
         * version, cost factor, salt and hash, separated by '$'
         *
         * @param version  the version, 2y,2b or 2a. (2a is not backwards compatible.)
         * @param cost     the cost factor, treated as an exponent of 2
         * @param salt     a 16 byte salt
         * @param password the password
         * @return a 60 character Bcrypt String
         */
        private static string CreateBcryptString(string version, byte[] password, byte[] salt, int cost)
        {
            if (!AllowedVersions.Contains(version))
                throw new ArgumentException("Version " + version + " is not accepted by this implementation.", "version");

            byte[] key = BCrypt.Generate(password, salt, cost);

            StringBuilder sb = new StringBuilder(60);
            sb.Append('$');
            sb.Append(version);
            sb.Append('$');
            sb.Append(cost < 10 ? ("0" + cost) : cost.ToString());
            sb.Append('$');
            EncodeData(sb, salt);
            EncodeData(sb, key);
            return sb.ToString();
        }

        /**
         * Creates a 60 character Bcrypt String, including
         * version, cost factor, salt and hash, separated by '$' using version
         * '2y'.
         *
         * @param cost     the cost factor, treated as an exponent of 2
         * @param salt     a 16 byte salt
         * @param password the password
         * @return a 60 character Bcrypt String
         */
        public static string Generate(char[] password, byte[] salt, int cost) =>
            Generate(DefaultVersion, password, salt, cost);

        /**
         * Creates a 60 character Bcrypt String, including
         * version, cost factor, salt and hash, separated by '$'
         *
         * @param version  the version, may be 2b, 2y or 2a. (2a is not backwards compatible.)
         * @param cost     the cost factor, treated as an exponent of 2
         * @param salt     a 16 byte salt
         * @param password the password
         * @return a 60 character Bcrypt String
         */
        public static string Generate(string version, char[] password, byte[] salt, int cost)
        {
            if (!AllowedVersions.Contains(version))
                throw new ArgumentException($"Version {version} is not accepted by this implementation.", nameof(version));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (salt == null)
                throw new ArgumentNullException(nameof(salt));
            if (salt.Length != 16)
                throw new DataLengthException("16 byte salt required: " + salt.Length);

            if (cost < 4 || cost > 31) // Minimum rounds: 16, maximum 2^31
                throw new ArgumentException("Invalid cost factor.", nameof(cost));

            byte[] psw = Strings.ToUtf8ByteArray(password);

            // 0 termination:

            int tmpLen = System.Math.Min(72, psw.Length + 1);
            byte[] tmp = Arrays.CopyOf(psw, tmpLen);
            Array.Clear(psw, 0, psw.Length);

            string rv = CreateBcryptString(version, tmp, salt, cost);
            Array.Clear(tmp, 0, tmp.Length);
            return rv;
        }

        /**
         * Checks if a password corresponds to a 60 character Bcrypt String
         *
         * @param bcryptString a 60 character Bcrypt String, including
         *                     version, cost factor, salt and hash,
         *                     separated by '$'
         * @param password     the password as an array of chars
         * @return true if the password corresponds to the
         * Bcrypt String, otherwise false
         */
        public static bool CheckPassword(string bcryptString, char[] password)
        {
            // validate bcryptString:
            if (bcryptString.Length != 60)
                throw new DataLengthException("Bcrypt String length: " + bcryptString.Length + ", 60 required.");
            if (bcryptString[0] != '$' || bcryptString[3] != '$' || bcryptString[6] != '$')
                throw new ArgumentException("Invalid Bcrypt String format.", "bcryptString");

            string version = bcryptString.Substring(1, 2);
            if (!AllowedVersions.Contains(version))
                throw new ArgumentException("Bcrypt version '" + version + "' is not supported by this implementation", "bcryptString");

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            if (!int.TryParse(bcryptString.AsSpan(4, 2), out int cost))
#else
            if (!int.TryParse(bcryptString.Substring(4, 2), out int cost))
#endif
                throw new ArgumentException("Invalid cost factor: " + bcryptString.Substring(4, 2), nameof(bcryptString));

            if (cost < 4 || cost > 31)
                throw new ArgumentException("Invalid cost factor: " + cost + ", 4 < cost < 31 expected.");

            // check password:
            if (password == null)
                throw new ArgumentNullException("Missing password.", nameof(password));

            int start = bcryptString.LastIndexOf('$') + 1, end = bcryptString.Length - 31;
            byte[] salt = DecodeSaltString(bcryptString.Substring(start, end - start));

            string newBcryptString = Generate(version, password, salt, cost);

            return bcryptString.Equals(newBcryptString);
        }

        /*
         * encode the input data producing a Bcrypt base 64 string.
         *
         * @param 	a byte representation of the salt or the password
         * @return 	the Bcrypt base64 string
         */
        private static void EncodeData(StringBuilder sb, byte[] data)
        {
            if (data.Length != 24 && data.Length != 16) // 192 bit key or 128 bit salt expected
                throw new DataLengthException("Invalid length: " + data.Length + ", 24 for key or 16 for salt expected");

            bool salt = false;
            if (data.Length == 16)//salt
            {
                salt = true;
                data = Arrays.CopyOf(data, 18); // zero padding
            }
            else // key
            {
                data[data.Length - 1] = 0x00;
            }

            MemoryStream mOut = new MemoryStream();

            for (int i = 0; i < data.Length; i += 3)
            {
                uint a1 = data[i];
                uint a2 = data[i + 1];
                uint a3 = data[i + 2];

                mOut.WriteByte(EncodingTable[a1 >> 2]);
                mOut.WriteByte(EncodingTable[((a1 << 4) | (a2 >> 4)) & 0x3f]);
                mOut.WriteByte(EncodingTable[((a2 << 2) | (a3 >> 6)) & 0x3f]);
                mOut.WriteByte(EncodingTable[a3 & 0x3f]);
            }

            byte[] buf = mOut.GetBuffer();
            int len = Convert.ToInt32(mOut.Length);

            int resultLen = salt
                ? 22  // truncate padding
                : len - 1;

            Strings.AppendFromByteArray(sb, buf, 0, resultLen);
        }

        /*
         * decodes the bcrypt base 64 encoded SaltString
         *
         * @param 		a 22 character Bcrypt base 64 encoded String 
         * @return 		the 16 byte salt
         * @exception 	DataLengthException if the length 
         * 				of parameter is not 22
         * @exception 	InvalidArgumentException if the parameter
         * 				contains a value other than from Bcrypts base 64 encoding table
         */
        private static byte[] DecodeSaltString(string saltString)
        {
            if (saltString.Length != 22)// bcrypt salt must be 22 (16 bytes)
                throw new DataLengthException("Invalid base64 salt length: " + saltString.Length + " , 22 required.");

            // Padding: add two '\u0000'
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            Span<char> saltChars = stackalloc char[24];
            saltString.CopyTo(saltChars);
#else
            char[] saltChars = new char[24];
            saltString.CopyTo(0, saltChars, 0, 22);
#endif

            // check string for invalid characters:
            for (int i = 0; i < 22; i++)
            {
                int value = Convert.ToInt32(saltChars[i]);
                if (value > 122 || value < 46 || (value > 57 && value < 65))
                    throw new ArgumentException("Salt string contains invalid character: " + value, "saltString");
            }

            MemoryStream mOut = new MemoryStream(16);

            for (int i = 0; i < 24; i += 4)
            {
                byte b1 = DecodingTable[saltChars[i]];
                byte b2 = DecodingTable[saltChars[i + 1]];
                byte b3 = DecodingTable[saltChars[i + 2]];
                byte b4 = DecodingTable[saltChars[i + 3]];

                mOut.WriteByte((byte)((b1 << 2) | (b2 >> 4)));
                mOut.WriteByte((byte)((b2 << 4) | (b3 >> 2)));
                mOut.WriteByte((byte)((b3 << 6) | b4));
            }

            // truncate to 16 bytes:
            return Arrays.CopyOf(mOut.GetBuffer(), 16);
        }
    }
}
