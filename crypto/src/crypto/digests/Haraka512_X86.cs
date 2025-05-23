﻿#if NETCOREAPP3_0_OR_GREATER
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Org.BouncyCastle.Crypto.Digests
{
    using Aes = System.Runtime.Intrinsics.X86.Aes;
    using Sse2 = System.Runtime.Intrinsics.X86.Sse2;

    public static class Haraka512_X86
    {
        public static bool IsSupported => Org.BouncyCastle.Runtime.Intrinsics.X86.Aes.IsEnabled;

        // Haraka round constants
        internal static readonly Vector128<byte>[] DefaultRoundConstants = new Vector128<byte>[]
        {
            Vector128.Create(0x9D, 0x7B, 0x81, 0x75, 0xF0, 0xFE, 0xC5, 0xB2, 0x0A, 0xC0, 0x20, 0xE6, 0x4C, 0x70, 0x84, 0x06),
            Vector128.Create(0x17, 0xF7, 0x08, 0x2F, 0xA4, 0x6B, 0x0F, 0x64, 0x6B, 0xA0, 0xF3, 0x88, 0xE1, 0xB4, 0x66, 0x8B),
            Vector128.Create(0x14, 0x91, 0x02, 0x9F, 0x60, 0x9D, 0x02, 0xCF, 0x98, 0x84, 0xF2, 0x53, 0x2D, 0xDE, 0x02, 0x34),
            Vector128.Create(0x79, 0x4F, 0x5B, 0xFD, 0xAF, 0xBC, 0xF3, 0xBB, 0x08, 0x4F, 0x7B, 0x2E, 0xE6, 0xEA, 0xD6, 0x0E),
            Vector128.Create(0x44, 0x70, 0x39, 0xBE, 0x1C, 0xCD, 0xEE, 0x79, 0x8B, 0x44, 0x72, 0x48, 0xCB, 0xB0, 0xCF, 0xCB),
            Vector128.Create(0x7B, 0x05, 0x8A, 0x2B, 0xED, 0x35, 0x53, 0x8D, 0xB7, 0x32, 0x90, 0x6E, 0xEE, 0xCD, 0xEA, 0x7E),
            Vector128.Create(0x1B, 0xEF, 0x4F, 0xDA, 0x61, 0x27, 0x41, 0xE2, 0xD0, 0x7C, 0x2E, 0x5E, 0x43, 0x8F, 0xC2, 0x67),
            Vector128.Create(0x3B, 0x0B, 0xC7, 0x1F, 0xE2, 0xFD, 0x5F, 0x67, 0x07, 0xCC, 0xCA, 0xAF, 0xB0, 0xD9, 0x24, 0x29),
            Vector128.Create(0xEE, 0x65, 0xD4, 0xB9, 0xCA, 0x8F, 0xDB, 0xEC, 0xE9, 0x7F, 0x86, 0xE6, 0xF1, 0x63, 0x4D, 0xAB),
            Vector128.Create(0x33, 0x7E, 0x03, 0xAD, 0x4F, 0x40, 0x2A, 0x5B, 0x64, 0xCD, 0xB7, 0xD4, 0x84, 0xBF, 0x30, 0x1C),
            Vector128.Create(0x00, 0x98, 0xF6, 0x8D, 0x2E, 0x8B, 0x02, 0x69, 0xBF, 0x23, 0x17, 0x94, 0xB9, 0x0B, 0xCC, 0xB2),
            Vector128.Create(0x8A, 0x2D, 0x9D, 0x5C, 0xC8, 0x9E, 0xAA, 0x4A, 0x72, 0x55, 0x6F, 0xDE, 0xA6, 0x78, 0x04, 0xFA),
            Vector128.Create(0xD4, 0x9F, 0x12, 0x29, 0x2E, 0x4F, 0xFA, 0x0E, 0x12, 0x2A, 0x77, 0x6B, 0x2B, 0x9F, 0xB4, 0xDF),
            Vector128.Create(0xEE, 0x12, 0x6A, 0xBB, 0xAE, 0x11, 0xD6, 0x32, 0x36, 0xA2, 0x49, 0xF4, 0x44, 0x03, 0xA1, 0x1E),
            Vector128.Create(0xA6, 0xEC, 0xA8, 0x9C, 0xC9, 0x00, 0x96, 0x5F, 0x84, 0x00, 0x05, 0x4B, 0x88, 0x49, 0x04, 0xAF),
            Vector128.Create(0xEC, 0x93, 0xE5, 0x27, 0xE3, 0xC7, 0xA2, 0x78, 0x4F, 0x9C, 0x19, 0x9D, 0xD8, 0x5E, 0x02, 0x21),
            Vector128.Create(0x73, 0x01, 0xD4, 0x82, 0xCD, 0x2E, 0x28, 0xB9, 0xB7, 0xC9, 0x59, 0xA7, 0xF8, 0xAA, 0x3A, 0xBF),
            Vector128.Create(0x6B, 0x7D, 0x30, 0x10, 0xD9, 0xEF, 0xF2, 0x37, 0x17, 0xB0, 0x86, 0x61, 0x0D, 0x70, 0x60, 0x62),
            Vector128.Create(0xC6, 0x9A, 0xFC, 0xF6, 0x53, 0x91, 0xC2, 0x81, 0x43, 0x04, 0x30, 0x21, 0xC2, 0x45, 0xCA, 0x5A),
            Vector128.Create(0x3A, 0x94, 0xD1, 0x36, 0xE8, 0x92, 0xAF, 0x2C, 0xBB, 0x68, 0x6B, 0x22, 0x3C, 0x97, 0x23, 0x92),
            Vector128.Create(0xB4, 0x71, 0x10, 0xE5, 0x58, 0xB9, 0xBA, 0x6C, 0xEB, 0x86, 0x58, 0x22, 0x38, 0x92, 0xBF, 0xD3),
            Vector128.Create(0x8D, 0x12, 0xE1, 0x24, 0xDD, 0xFD, 0x3D, 0x93, 0x77, 0xC6, 0xF0, 0xAE, 0xE5, 0x3C, 0x86, 0xDB),
            Vector128.Create(0xB1, 0x12, 0x22, 0xCB, 0xE3, 0x8D, 0xE4, 0x83, 0x9C, 0xA0, 0xEB, 0xFF, 0x68, 0x62, 0x60, 0xBB),
            Vector128.Create(0x7D, 0xF7, 0x2B, 0xC7, 0x4E, 0x1A, 0xB9, 0x2D, 0x9C, 0xD1, 0xE4, 0xE2, 0xDC, 0xD3, 0x4B, 0x73),
            Vector128.Create(0x4E, 0x92, 0xB3, 0x2C, 0xC4, 0x15, 0x14, 0x4B, 0x43, 0x1B, 0x30, 0x61, 0xC3, 0x47, 0xBB, 0x43),
            Vector128.Create(0x99, 0x68, 0xEB, 0x16, 0xDD, 0x31, 0xB2, 0x03, 0xF6, 0xEF, 0x07, 0xE7, 0xA8, 0x75, 0xA7, 0xDB),
            Vector128.Create(0x2C, 0x47, 0xCA, 0x7E, 0x02, 0x23, 0x5E, 0x8E, 0x77, 0x59, 0x75, 0x3C, 0x4B, 0x61, 0xF3, 0x6D),
            Vector128.Create(0xF9, 0x17, 0x86, 0xB8, 0xB9, 0xE5, 0x1B, 0x6D, 0x77, 0x7D, 0xDE, 0xD6, 0x17, 0x5A, 0xA7, 0xCD),
            Vector128.Create(0x5D, 0xEE, 0x46, 0xA9, 0x9D, 0x06, 0x6C, 0x9D, 0xAA, 0xE9, 0xA8, 0x6B, 0xF0, 0x43, 0x6B, 0xEC),
            Vector128.Create(0xC1, 0x27, 0xF3, 0x3B, 0x59, 0x11, 0x53, 0xA2, 0x2B, 0x33, 0x57, 0xF9, 0x50, 0x69, 0x1E, 0xCB),
            Vector128.Create(0xD9, 0xD0, 0x0E, 0x60, 0x53, 0x03, 0xED, 0xE4, 0x9C, 0x61, 0xDA, 0x00, 0x75, 0x0C, 0xEE, 0x2C),
            Vector128.Create(0x50, 0xA3, 0xA4, 0x63, 0xBC, 0xBA, 0xBB, 0x80, 0xAB, 0x0C, 0xE9, 0x96, 0xA1, 0xA5, 0xB1, 0xF0),
            Vector128.Create(0x39, 0xCA, 0x8D, 0x93, 0x30, 0xDE, 0x0D, 0xAB, 0x88, 0x29, 0x96, 0x5E, 0x02, 0xB1, 0x3D, 0xAE),
            Vector128.Create(0x42, 0xB4, 0x75, 0x2E, 0xA8, 0xF3, 0x14, 0x88, 0x0B, 0xA4, 0x54, 0xD5, 0x38, 0x8F, 0xBB, 0x17),
            Vector128.Create(0xF6, 0x16, 0x0A, 0x36, 0x79, 0xB7, 0xB6, 0xAE, 0xD7, 0x7F, 0x42, 0x5F, 0x5B, 0x8A, 0xBB, 0x34),
            Vector128.Create(0xDE, 0xAF, 0xBA, 0xFF, 0x18, 0x59, 0xCE, 0x43, 0x38, 0x54, 0xE5, 0xCB, 0x41, 0x52, 0xF6, 0x26),
            Vector128.Create(0x78, 0xC9, 0x9E, 0x83, 0xF7, 0x9C, 0xCA, 0xA2, 0x6A, 0x02, 0xF3, 0xB9, 0x54, 0x9A, 0xE9, 0x4C),
            Vector128.Create(0x35, 0x12, 0x90, 0x22, 0x28, 0x6E, 0xC0, 0x40, 0xBE, 0xF7, 0xDF, 0x1B, 0x1A, 0xA5, 0x51, 0xAE),
            Vector128.Create(0xCF, 0x59, 0xA6, 0x48, 0x0F, 0xBC, 0x73, 0xC1, 0x2B, 0xD2, 0x7E, 0xBA, 0x3C, 0x61, 0xC1, 0xA0),
            Vector128.Create(0xA1, 0x9D, 0xC5, 0xE9, 0xFD, 0xBD, 0xD6, 0x4A, 0x88, 0x82, 0x28, 0x02, 0x03, 0xCC, 0x6A, 0x75),
        };

        public static void Hash(ReadOnlySpan<byte> input, Span<byte> output)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException(nameof(Haraka512_X86));

            var s0 = Load128(input[  ..16]);
            var s1 = Load128(input[16..32]);
            var s2 = Load128(input[32..48]);
            var s3 = Load128(input[48..64]);

            ImplRounds(ref s0, ref s1, ref s2, ref s3, DefaultRoundConstants.AsSpan(0, 40));

            s0 = Sse2.Xor(s0, Load128(input[  ..16]));
            s1 = Sse2.Xor(s1, Load128(input[16..32]));
            s2 = Sse2.Xor(s2, Load128(input[32..48]));
            s3 = Sse2.Xor(s3, Load128(input[48..64]));

            Store64(s0.GetUpper(), output[  .. 8]);
            Store64(s1.GetUpper(), output[ 8..16]);
            Store64(s2.GetLower(), output[16..24]);
            Store64(s3.GetLower(), output[24..32]);
        }

        public static void Hash(ReadOnlySpan<byte> input, Span<byte> output,
            ReadOnlySpan<Vector128<byte>> roundConstants)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException(nameof(Haraka512_X86));

            var s0 = Load128(input[  ..16]);
            var s1 = Load128(input[16..32]);
            var s2 = Load128(input[32..48]);
            var s3 = Load128(input[48..64]);

            ImplRounds(ref s0, ref s1, ref s2, ref s3, roundConstants[..40]);

            s0 = Sse2.Xor(s0, Load128(input[  ..16]));
            s1 = Sse2.Xor(s1, Load128(input[16..32]));
            s2 = Sse2.Xor(s2, Load128(input[32..48]));
            s3 = Sse2.Xor(s3, Load128(input[48..64]));

            Store64(s0.GetUpper(), output[  .. 8]);
            Store64(s1.GetUpper(), output[ 8..16]);
            Store64(s2.GetLower(), output[16..24]);
            Store64(s3.GetLower(), output[24..32]);
        }

        public static void Permute(ReadOnlySpan<byte> input, Span<byte> output)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException(nameof(Haraka512_X86));

            var s0 = Load128(input[  ..16]);
            var s1 = Load128(input[16..32]);
            var s2 = Load128(input[32..48]);
            var s3 = Load128(input[48..64]);

            ImplRounds(ref s0, ref s1, ref s2, ref s3, DefaultRoundConstants.AsSpan(0, 40));

            Store128(s0, output[  ..16]);
            Store128(s1, output[16..32]);
            Store128(s2, output[32..48]);
            Store128(s3, output[48..64]);
        }

        public static void Permute(ReadOnlySpan<byte> input, Span<byte> output,
            ReadOnlySpan<Vector128<byte>> roundConstants)
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException(nameof(Haraka512_X86));

            var s0 = Load128(input[  ..16]);
            var s1 = Load128(input[16..32]);
            var s2 = Load128(input[32..48]);
            var s3 = Load128(input[48..64]);

            ImplRounds(ref s0, ref s1, ref s2, ref s3, roundConstants[..40]);

            Store128(s0, output[  ..16]);
            Store128(s1, output[16..32]);
            Store128(s2, output[32..48]);
            Store128(s3, output[48..64]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ImplRounds(ref Vector128<byte> s0, ref Vector128<byte> s1, ref Vector128<byte> s2,
            ref Vector128<byte> s3, ReadOnlySpan<Vector128<byte>> rc)
        {
            ImplRound(ref s0, ref s1, ref s2, ref s3, rc[  .. 8]);
            ImplRound(ref s0, ref s1, ref s2, ref s3, rc[ 8..16]);
            ImplRound(ref s0, ref s1, ref s2, ref s3, rc[16..24]);
            ImplRound(ref s0, ref s1, ref s2, ref s3, rc[24..32]);
            ImplRound(ref s0, ref s1, ref s2, ref s3, rc[32..40]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ImplRound(ref Vector128<byte> s0, ref Vector128<byte> s1, ref Vector128<byte> s2,
            ref Vector128<byte> s3, ReadOnlySpan<Vector128<byte>> rc)
        {
            ImplAes(ref s0, ref s1, ref s2, ref s3, rc[..8]);
            ImplMix(ref s0, ref s1, ref s2, ref s3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ImplAes(ref Vector128<byte> s0, ref Vector128<byte> s1, ref Vector128<byte> s2,
            ref Vector128<byte> s3, ReadOnlySpan<Vector128<byte>> rc)
        {
            var t0 = Aes.Encrypt(s0, rc[0]);
            var t1 = Aes.Encrypt(s1, rc[1]);
            var t2 = Aes.Encrypt(s2, rc[2]);
            var t3 = Aes.Encrypt(s3, rc[3]);

            s0 = Aes.Encrypt(t0, rc[4]);
            s1 = Aes.Encrypt(t1, rc[5]);
            s2 = Aes.Encrypt(t2, rc[6]);
            s3 = Aes.Encrypt(t3, rc[7]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ImplMix(ref Vector128<byte> s0, ref Vector128<byte> s1, ref Vector128<byte> s2,
            ref Vector128<byte> s3)
        {
            var t0 = s0.AsUInt32();
            var t1 = s1.AsUInt32();
            var t2 = s2.AsUInt32();
            var t3 = s3.AsUInt32();

            var u0 = Sse2.UnpackLow(t0, t1);
            var u1 = Sse2.UnpackHigh(t0, t1);
            var u2 = Sse2.UnpackLow(t2, t3);
            var u3 = Sse2.UnpackHigh(t2, t3);

            s0 = Sse2.UnpackHigh(u1, u3).AsByte();
            s1 = Sse2.UnpackLow(u2, u0).AsByte();
            s2 = Sse2.UnpackHigh(u2, u0).AsByte();
            s3 = Sse2.UnpackLow(u1, u3).AsByte();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector128<byte> Load128(ReadOnlySpan<byte> t)
        {
            if (Org.BouncyCastle.Runtime.Intrinsics.Vector.IsPackedLittleEndian)
                return MemoryMarshal.Read<Vector128<byte>>(t);

            return Vector128.Create(
                BinaryPrimitives.ReadUInt64LittleEndian(t[..8]),
                BinaryPrimitives.ReadUInt64LittleEndian(t[8..])
            ).AsByte();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Store128(Vector128<byte> s, Span<byte> t)
        {
            if (Org.BouncyCastle.Runtime.Intrinsics.Vector.IsPackedLittleEndian)
            {
#if NET8_0_OR_GREATER
                MemoryMarshal.Write(t, in s);
#else
                MemoryMarshal.Write(t, ref s);
#endif
                return;
            }

            var u = s.AsUInt64();
            BinaryPrimitives.WriteUInt64LittleEndian(t[..8], u.GetElement(0));
            BinaryPrimitives.WriteUInt64LittleEndian(t[8..], u.GetElement(1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Store64(Vector64<byte> s, Span<byte> t)
        {
            if (Org.BouncyCastle.Runtime.Intrinsics.Vector.IsPackedLittleEndian)
            {
#if NET8_0_OR_GREATER
                MemoryMarshal.Write(t, in s);
#else
                MemoryMarshal.Write(t, ref s);
#endif
                return;
            }

            var u = s.AsUInt64();
            BinaryPrimitives.WriteUInt64LittleEndian(t, u.ToScalar());
        }
    }
}
#endif
