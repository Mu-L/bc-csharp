﻿using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.Test;

namespace Org.BouncyCastle.Crypto.Tests
{
    [TestFixture]
    public class AsconTest
    {
        [Test, Explicit]
        public void BenchDigest_AsconHash()
        {
            ImplBenchDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHash));
        }

        [Test, Explicit]
        public void BenchDigest_AsconHashA()
        {
            ImplBenchDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHashA));
        }

        [Test, Explicit]
        public void BenchDigest_AsconHash256()
        {
            ImplBenchDigest(new AsconHash256());
        }

        [Test, Explicit]
        public void BenchEngineAuth_ascon128()
        {
            ImplBenchEngineAuth(new AsconEngine(AsconEngine.AsconParameters.ascon128));
        }

        [Test, Explicit]
        public void BenchEngineAuth_ascon128a()
        {
            ImplBenchEngineAuth(new AsconEngine(AsconEngine.AsconParameters.ascon128a));
        }

        [Test, Explicit]
        public void BenchEngineAuth_ascon80pq()
        {
            ImplBenchEngineAuth(new AsconEngine(AsconEngine.AsconParameters.ascon80pq));
        }

        [Test, Explicit]
        public void BenchEngineAuth_AsconAead128()
        {
            ImplBenchEngineAuth(new AsconAead128());
        }

        [Test, Explicit]
        public void BenchEngineDecrypt_ascon128()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon128), false);
        }

        [Test, Explicit]
        public void BenchEngineDecrypt_ascon128a()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon128a), false);
        }

        [Test, Explicit]
        public void BenchEngineDecrypt_ascon80pq()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon80pq), false);
        }

        [Test, Explicit]
        public void BenchEngineDecrypt_AsconAead128()
        {
            ImplBenchEngineCrypt(new AsconAead128(), false);
        }

        [Test, Explicit]
        public void BenchEngineEncrypt_ascon128()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon128), true);
        }

        [Test, Explicit]
        public void BenchEngineEncrypt_ascon128a()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon128a), true);
        }

        [Test, Explicit]
        public void BenchEngineEncrypt_ascon80pq()
        {
            ImplBenchEngineCrypt(new AsconEngine(AsconEngine.AsconParameters.ascon80pq), true);
        }

        [Test, Explicit]
        public void BenchEngineEncrypt_AsconAead128()
        {
            ImplBenchEngineCrypt(new AsconAead128(), true);
        }

        [Test, Explicit]
        public void BenchXof_AsconXof()
        {
            ImplBenchXof(new AsconXof(AsconXof.AsconParameters.AsconXof));
        }

        [Test, Explicit]
        public void BenchXof_AsconXofA()
        {
            ImplBenchXof(new AsconXof(AsconXof.AsconParameters.AsconXofA));
        }

        [Test, Explicit]
        public void BenchXof_AsconXof128()
        {
            ImplBenchXof(new AsconXof128());
        }

        [Test, Explicit]
        public void BenchXof_AsconCXof128()
        {
            ImplBenchXof(new AsconCXof128());
        }

        [Test]
        public void TestBufferingEngine_ascon128()
        {
            ImplTestBufferingEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon128));
        }

        [Test]
        public void TestBufferingEngine_ascon128a()
        {
            ImplTestBufferingEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon128a));
        }

        [Test]
        public void TestBufferingEngine_ascon80()
        {
            ImplTestBufferingEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon80pq));
        }

        [Test]
        public void TestBufferingEngine_AsconAead128()
        {
            ImplTestBufferingEngine(() => new AsconAead128());
        }

        [Test]
        public void TestDigestReset()
        {
            CheckDigestReset(new AsconHash256());
            CheckDigestReset(new AsconXof128());
            CheckDigestReset(new AsconCXof128());
            CheckDigestReset(new AsconXof(AsconXof.AsconParameters.AsconXof));
            CheckDigestReset(new AsconXof(AsconXof.AsconParameters.AsconXofA));
            CheckDigestReset(new AsconDigest(AsconDigest.AsconParameters.AsconHash));
            CheckDigestReset(new AsconDigest(AsconDigest.AsconParameters.AsconHashA));
        }

        [Test]
        public void TestExceptionsDigest_AsconHash()
        {
            ImplTestExceptionsDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHash));
        }

        [Test]
        public void TestExceptionsDigest_AsconHashA()
        {
            ImplTestExceptionsDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHashA));
        }

        [Test]
        public void TestExceptionsDigest_AsconHash256()
        {
            ImplTestExceptionsDigest(new AsconHash256());
        }

        [Test]
        public void TestExceptionsEngine_ascon128()
        {
            ImplTestExceptionsEngine(new AsconEngine(AsconEngine.AsconParameters.ascon128));
        }

        [Test]
        public void TestExceptionsEngine_ascon128a()
        {
            ImplTestExceptionsEngine(new AsconEngine(AsconEngine.AsconParameters.ascon128a));
        }

        [Test]
        public void TestExceptionsEngine_ascon80pq()
        {
            ImplTestExceptionsEngine(new AsconEngine(AsconEngine.AsconParameters.ascon80pq));
        }

        [Test]
        public void TestExceptionsEngine_AsconAead128()
        {
            ImplTestExceptionsEngine(new AsconAead128());
        }

        [Test]
        public void TestExceptionsXof_AsconXof()
        {
            ImplTestExceptionsXof(new AsconXof(AsconXof.AsconParameters.AsconXof));
        }

        [Test]
        public void TestExceptionsXof_AsconXofA()
        {
            ImplTestExceptionsXof(new AsconXof(AsconXof.AsconParameters.AsconXofA));
        }

        [Test]
        public void TestExceptionsXof_AsconXof128()
        {
            ImplTestExceptionsXof(new AsconXof128());
        }

        [Test]
        public void TestExceptionsXof_AsconCXof128()
        {
            ImplTestExceptionsXof(new AsconCXof128());
        }

        [Test]
        public void TestLongMessage()
        {
            CheckAeadCipher(32, 16, 100, 128, () => new AsconAead128());
            CheckAeadCipher(32, 16, 100, 128, () => new AsconEngine(AsconEngine.AsconParameters.ascon128));
            CheckAeadCipher(32, 16, 100, 128, () => new AsconEngine(AsconEngine.AsconParameters.ascon128a));
            CheckAeadCipher(32, 16, 100, 160, () => new AsconEngine(AsconEngine.AsconParameters.ascon80pq));
        }

        [Test]
        public void TestOutputXof_AsconXof()
        {
            ImplTestOutputXof(new AsconXof(AsconXof.AsconParameters.AsconXof));
        }

        [Test]
        public void TestOutputXof_AsconXofA()
        {
            ImplTestOutputXof(new AsconXof(AsconXof.AsconParameters.AsconXofA));
        }

        [Test]
        public void TestOutputXof_AsconXof128()
        {
            ImplTestOutputXof(new AsconXof128());
        }

        [Test]
        public void TestOutputXof_AsconCXof128()
        {
            ImplTestOutputXof(new AsconCXof128());
        }

        [Test]
        public void TestParametersDigest_AsconHash()
        {
            ImplTestParametersDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHash), 32);
        }

        [Test]
        public void TestParametersDigest_AsconHashA()
        {
            ImplTestParametersDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHashA), 32);
        }

        [Test]
        public void TestParametersDigest_AsconHash256()
        {
            ImplTestParametersDigest(new AsconHash256(), 32);
        }

        [Test]
        public void TestParametersEngine_ascon128()
        {
            ImplTestParametersEngine(new AsconEngine(AsconEngine.AsconParameters.ascon128), 16, 16, 16);
        }

        [Test]
        public void TestParametersEngine_ascon128a()
        {
            ImplTestParametersEngine(new AsconEngine(AsconEngine.AsconParameters.ascon128a), 16, 16, 16);
        }

        [Test]
        public void TestParametersEngine_ascon80pq()
        {
            ImplTestParametersEngine(new AsconEngine(AsconEngine.AsconParameters.ascon80pq), 20, 16, 16);
        }

        [Test]
        public void TestParametersEngine_AsconAead128()
        {
            ImplTestParametersEngine(new AsconAead128(), 16, 16, 16);
        }

        [Test]
        public void TestParametersXof_AsconXof()
        {
            ImplTestParametersXof(new AsconXof(AsconXof.AsconParameters.AsconXof), 32);
        }

        [Test]
        public void TestParametersXof_AsconXofA()
        {
            ImplTestParametersXof(new AsconXof(AsconXof.AsconParameters.AsconXofA), 32);
        }

        [Test]
        public void TestParametersXof_AsconXof128()
        {
            ImplTestParametersXof(new AsconXof128(), 32);
        }

        [Test]
        public void TestParametersXof_AsconCXof128()
        {
            ImplTestParametersXof(new AsconCXof128(), 32);
        }

        [Test]
        public void TestVectorsDigest_AsconHash()
        {
            ImplTestVectorsDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHash), "crypto/ascon", "asconhash_LWC_HASH_KAT_256.txt");
        }

        [Test]
        public void TestVectorsDigest_AsconHashA()
        {
            ImplTestVectorsDigest(new AsconDigest(AsconDigest.AsconParameters.AsconHashA), "crypto/ascon", "asconhasha_LWC_HASH_KAT_256.txt");
        }

        [Test]
        public void TestVectorsDigest_AsconHash256()
        {
            ImplTestVectorsDigest(new AsconHash256(), "crypto/ascon/asconhash256", "LWC_HASH_KAT_256.txt");
        }

        [Test]
        public void TestVectorsEngine_ascon128()
        {
            ImplTestVectorsEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon128), "crypto/ascon", "LWC_AEAD_KAT_128_128.txt");
        }

        [Test]
        public void TestVectorsEngine_ascon128a()
        {
            ImplTestVectorsEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon128a), "crypto/ascon", "LWC_AEAD_KAT_128_128_a.txt");
        }

        [Test]
        public void TestVectorsEngine_ascon80pq()
        {
            ImplTestVectorsEngine(() => new AsconEngine(AsconEngine.AsconParameters.ascon80pq), "crypto/ascon", "LWC_AEAD_KAT_160_128.txt");
        }

        [Test]
        public void TestVectorsEngine_AsconAead128()
        {
            ImplTestVectorsEngine(() => new AsconAead128(), "crypto/ascon/asconaead128", "LWC_AEAD_KAT_128_128.txt");
        }

        [Test]
        public void TestVectorsXof_AsconXof()
        {
            ImplTestVectorsXof(new AsconXof(AsconXof.AsconParameters.AsconXof), "crypto/ascon", "asconxof_LWC_HASH_KAT_256.txt");
        }

        [Test]
        public void TestVectorsXof_AsconXofA()
        {
            ImplTestVectorsXof(new AsconXof(AsconXof.AsconParameters.AsconXofA), "crypto/ascon", "asconxofa_LWC_HASH_KAT_256.txt");
        }

        [Test]
        public void TestVectorsXof_AsconXof128()
        {
            ImplTestVectorsXof(new AsconXof128(), "crypto/ascon/asconxof128", "LWC_XOF_KAT_128_512.txt");
        }

        [Test]
        public void TestVectorsXof_AsconCXof128()
        {
            ImplTestVectorsCXof128("crypto/ascon/asconcxof128", "LWC_CXOF_KAT_128_512.txt");
        }

        private static void CheckAeadCipher(int aeadLen, int ivLen, int msgLen, int strength,
            CipherTest.CreateAeadCipher create)
        {
            Assert.True(CipherTest.TestAeadCipher(aeadLen, ivLen, msgLen, strength, create));
        }

        private static void CheckDigestReset(IDigest digest)
        {
            Assert.True(DigestTest.TestDigestReset(digest));
        }

        private static int GetKeyBytesSize(IAeadCipher ascon)
        {
            if (ascon is AsconAead128 asconAead128)
                return asconAead128.GetKeyBytesSize();

            return ((AsconEngine)ascon).GetKeyBytesSize();
        }

        private static int GetIVBytesSize(IAeadCipher ascon)
        {
            if (ascon is AsconAead128 asconAead128)
                return asconAead128.GetIVBytesSize();

            return ((AsconEngine)ascon).GetIVBytesSize();
        }

        private static void ImplBenchDigest(IDigest ascon)
        {
            byte[] data = new byte[1024];
            for (int i = 0; i < 1024; ++i)
            {
                for (int j = 0; j < 1024; ++j)
                {
                    // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                    ascon.BlockUpdate(data);
#else
                    ascon.BlockUpdate(data, 0, 1024);
#endif
                }

                // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                ascon.DoFinal(data);
#else
                ascon.DoFinal(data, 0);
#endif
            }
        }

        private static void ImplBenchEngineAuth(IAeadCipher ascon)
        {
            InitEngine(ascon, true);

            byte[] data = new byte[1024];
            for (int i = 0; i < 1024 * 1024; ++i)
            {
                // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                ascon.ProcessAadBytes(data);
#else
                ascon.ProcessAadBytes(data, 0, 1024);
#endif
            }
        }

        private static void ImplBenchEngineCrypt(IAeadCipher ascon, bool forEncryption)
        {
            InitEngine(ascon, forEncryption);

            byte[] data = new byte[1024 + 64];
            for (int i = 0; i < 1024 * 1024; ++i)
            {
                // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                ascon.ProcessBytes(data.AsSpan(0, 1024), data);
#else
                ascon.ProcessBytes(data, 0, 1024, data, 0);
#endif
            }
        }

        private static void ImplBenchXof(IXof ascon)
        {
            byte[] data = new byte[1024];
            for (int i = 0; i < 1024; ++i)
            {
                for (int j = 0; j < 1024; ++j)
                {
                    // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                    ascon.BlockUpdate(data);
#else
                    ascon.BlockUpdate(data, 0, 1024);
#endif
                }

                // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                ascon.OutputFinal(data);
#else
                ascon.OutputFinal(data, 0, data.Length);
#endif
            }
        }

        private static void ImplTestBufferingEngine(CipherTest.CreateAeadCipher create)
        {
            Random random = new Random();

            int plaintextLength = 256;
            byte[] plaintext = new byte[plaintextLength];
            random.NextBytes(plaintext);

            var ascon0 = create();
            InitEngine(ascon0, true);

            byte[] ciphertext = new byte[ascon0.GetOutputSize(plaintextLength)];
            random.NextBytes(ciphertext);

            int ciphertextLength = ascon0.ProcessBytes(plaintext, 0, plaintextLength, ciphertext, 0);
            ciphertextLength += ascon0.DoFinal(ciphertext, ciphertextLength);

            byte[] output = new byte[ciphertextLength];

            // Encryption
            for (int split = 1; split < plaintextLength; ++split)
            {
                var ascon = create();
                InitEngine(ascon, true);

                random.NextBytes(output);

                int length = ascon.ProcessBytes(plaintext, 0, split, output, 0);

                Assert.AreEqual(0, ascon.GetUpdateOutputSize(0));

                length += ascon.ProcessBytes(plaintext, split, plaintextLength - split, output, length);
                length += ascon.DoFinal(output, length);

                Assert.True(Arrays.AreEqual(ciphertext, 0, ciphertextLength, output, 0, length),
                    "encryption failed with split: " + split);
            }

            // Decryption
            for (int split = 1; split < ciphertextLength; ++split)
            {
                var ascon = create();
                InitEngine(ascon, false);

                random.NextBytes(output);

                int length = ascon.ProcessBytes(ciphertext, 0, split, output, 0);

                Assert.AreEqual(0, ascon.GetUpdateOutputSize(0));

                length += ascon.ProcessBytes(ciphertext, split, ciphertextLength - split, output, length);
                length += ascon.DoFinal(output, length);

                Assert.True(Arrays.AreEqual(plaintext, 0, plaintextLength, output, 0, length),
                    "decryption failed with split: " + split);
            }
        }

        private static void ImplTestExceptionsDigest(IDigest ascon)
        {
            try
            {
                ascon.BlockUpdate(new byte[1], 1, 1);
                Assert.Fail(ascon.AlgorithmName + ": input for BlockUpdate is too short");
            }
            catch (DataLengthException)
            {
                //expected
            }

            try
            {
                ascon.DoFinal(new byte[ascon.GetDigestSize() - 1], 2);
                Assert.Fail(ascon.AlgorithmName + ": output for DoFinal is too short");
            }
            catch (OutputLengthException)
            {
                //expected
            }
        }

        private void ImplTestExceptionsEngine(IAeadCipher ascon)
        {
            int keySize = GetKeyBytesSize(ascon), ivSize = GetIVBytesSize(ascon);
            int offset;
            byte[] k = new byte[keySize];
            byte[] iv = new byte[ivSize];
            byte[] m = Array.Empty<byte>();
            var param = new ParametersWithIV(new KeyParameter(k), iv);
            try
            {
                ascon.ProcessBytes(m, 0, m.Length, null, 0);
                Assert.Fail(ascon.AlgorithmName + " needs to be initialized before ProcessBytes");
            }
            catch (InvalidOperationException)
            {
                //expected
            }

            try
            {
                ascon.ProcessByte(0x00, null, 0);
                Assert.Fail(ascon.AlgorithmName + " needs to be initialized before ProcessByte");
            }
            catch (InvalidOperationException)
            {
                //expected
            }

            try
            {
                ascon.Reset();
                Assert.Fail(ascon.AlgorithmName + " needs to be initialized before Reset");
            }
            catch (InvalidOperationException)
            {
                //expected
            }

            try
            {
                ascon.DoFinal(null, m.Length);
                Assert.Fail(ascon.AlgorithmName + " needs to be initialized before DoFinal");
            }
            catch (InvalidOperationException)
            {
                //expected
            }

            try
            {
                ascon.GetMac();
                ascon.GetOutputSize(0);
                ascon.GetUpdateOutputSize(0);
            }
            catch (InvalidOperationException)
            {
                //expected
                Assert.Fail(ascon.AlgorithmName + " functions can be called before initialization");
            }

            Random rand = new Random();
            int randomNum;
            while ((randomNum = rand.Next(100)) == keySize) ;
            byte[] k1 = new byte[randomNum];
            while ((randomNum = rand.Next(100)) == ivSize) ;
            byte[] iv1 = new byte[randomNum];
            try
            {
                ascon.Init(true, new ParametersWithIV(new KeyParameter(k1), iv));
                Assert.Fail(ascon.AlgorithmName + " k size does not match");
            }
            catch (ArgumentException)
            {
                //expected
            }
            try
            {
                ascon.Init(true, new ParametersWithIV(new KeyParameter(k), iv1));
                Assert.Fail(ascon.AlgorithmName + "iv size does not match");
            }
            catch (ArgumentException)
            {
                //expected
            }

            ascon.Init(true, param);
            byte[] c1 = new byte[ascon.GetOutputSize(m.Length)];
            try
            {
                ascon.DoFinal(c1, m.Length);
            }
            catch (Exception)
            {
                Assert.Fail(ascon.AlgorithmName + " allows no input for AAD and plaintext");
            }
            byte[] mac2 = ascon.GetMac();
            if (mac2 == null)
            {
                Assert.Fail("mac should not be empty after DoFinal");
            }
            if (!Arrays.AreEqual(mac2, c1))
            {
                Assert.Fail("mac should be equal when calling DoFinal and GetMac");
            }

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadByte(0x00);
            byte[] mac1 = new byte[ascon.GetOutputSize(0)];
            ascon.DoFinal(mac1, 0);
            if (Arrays.AreEqual(mac1, mac2))
            {
                Assert.Fail("mac should not match");
            }

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessByte(0, null, 0);
            try
            {
                ascon.ProcessAadByte(0x00);
                Assert.Fail("ProcessAadByte cannot be called after encryption/decryption");
            }
            catch (InvalidOperationException)
            {
                //expected
            }
            try
            {
                ascon.ProcessAadBytes(new byte[1], 0, 1);
                Assert.Fail("ProcessAadBytes cannot be called after encryption/decryption");
            }
            catch (InvalidOperationException)
            {
                //expected
            }

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            try
            {
                ascon.ProcessAadBytes(new byte[1], 1, 1);
                Assert.Fail("input for ProcessAadBytes is too short");
            }
            catch (DataLengthException)
            {
                //expected
            }
            try
            {
                ascon.ProcessBytes(new byte[1], 1, 1, c1, 0);
                Assert.Fail("input for ProcessBytes is too short");
            }
            catch (DataLengthException)
            {
                //expected
            }
            ascon.Init(true, param);
            try
            {
                int need = ascon.GetUpdateOutputSize(64);
                ascon.ProcessBytes(new byte[64], 0, 64, new byte[need], 1);
                Assert.Fail("output for ProcessBytes is too short");
            }
            catch (OutputLengthException)
            {
                //expected
            }
            try
            {
                ascon.DoFinal(new byte[2], 2);
                Assert.Fail("output for DoFinal is too short");
            }
            catch (OutputLengthException)
            {
                //expected
            }

            ImplTestExceptionsGetUpdateOutputSize(ascon, false, param, 100);
            ImplTestExceptionsGetUpdateOutputSize(ascon, true, param, 100);

            mac1 = new byte[ascon.GetOutputSize(0)];
            mac2 = new byte[ascon.GetOutputSize(0)];

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadBytes(new byte[2], 0, 2);
            ascon.DoFinal(mac1, 0);

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadByte(0x00);
            ascon.ProcessAadByte(0x00);
            ascon.DoFinal(mac2, 0);

            if (!Arrays.AreEqual(mac1, mac2))
            {
                Assert.Fail("mac should match for the same AAD with different ways of inputting");
            }

            byte[] aad2 = { 0, 1, 2, 3, 4 };
            byte[] aad3 = { 0, 0, 1, 2, 3, 4, 5 };
            byte[] m2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            byte[] m3 = { 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            byte[] m4 = new byte[m2.Length];
            byte[] c2 = new byte[ascon.GetOutputSize(m2.Length)];
            byte[] c3 = new byte[ascon.GetOutputSize(m3.Length)];

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(m2, 0, m2.Length, c2, 0);
            ascon.DoFinal(c2, offset);

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadBytes(aad3, 1, aad2.Length);
            offset = ascon.ProcessBytes(m3, 1, m2.Length, c3, 1);
            ascon.DoFinal(c3, offset + 1);

            byte[] c3_partial = new byte[c2.Length];
            Array.Copy(c3, 1, c3_partial, 0, c2.Length);
            if (!Arrays.AreEqual(c2, c3_partial))
            {
                Assert.Fail("mac should match for the same AAD and message with different offset for both input and output");
            }

            ascon.Init(false, param);
            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(c2, 0, c2.Length, m4, 0);
            ascon.DoFinal(m4, offset);
            if (!Arrays.AreEqual(m2, m4))
            {
                Assert.Fail("The encryption and decryption does not recover the plaintext");
            }

            c2[c2.Length - 1] ^= 1;

            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(c2, 0, c2.Length, m4, 0);
            try
            {
                ascon.DoFinal(m4, offset);
                Assert.Fail("The decryption should fail");
            }
            catch (InvalidCipherTextException)
            {
                //expected;
            }
            c2[c2.Length - 1] ^= 1;

            byte[] m7 = new byte[32 + rand.Next(32)];
            rand.NextBytes(m7);

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            byte[] c7 = new byte[ascon.GetOutputSize(m7.Length)];
            byte[] c8 = new byte[c7.Length];
            byte[] c9 = new byte[c7.Length];
            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(m7, 0, m7.Length, c7, 0);
            offset += ascon.DoFinal(c7, offset);

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(m7, 0, m7.Length / 2, c8, 0);
            offset += ascon.ProcessBytes(m7, m7.Length / 2, m7.Length - m7.Length / 2, c8, offset);
            offset += ascon.DoFinal(c8, offset);

            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            int split = rand.Next(1, m7.Length);
            ascon.ProcessAadBytes(aad2, 0, aad2.Length);
            offset = ascon.ProcessBytes(m7, 0, split, c9, 0);
            offset += ascon.ProcessBytes(m7, split, m7.Length - split, c9, offset);
            offset += ascon.DoFinal(c9, offset);

            if (!Arrays.AreEqual(c7, c8) || !Arrays.AreEqual(c7, c9))
            {
                Assert.Fail("Splitting input of plaintext should output the same ciphertext");
            }

            // NOTE: .NET Core 3.1 has Span<T>, but is tested against our .NET Standard 2.0 assembly.
//#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            // TODO Maybe use a different IV for this
            ascon.Init(true, param);
            Span<byte> c4 = new byte[ascon.GetOutputSize(m2.Length)];
            ascon.ProcessAadBytes(aad2);
            offset = ascon.ProcessBytes(m2, c4);
            offset += ascon.DoFinal(c4[offset..]);
            if (!c4[..offset].SequenceEqual(c2))
            {
                Assert.Fail("Encryption should match for the same AAD and message with/without Span-based API");
            }

            ascon.Init(false, param);
            Span<byte> m6 = new byte[m2.Length];
            ascon.ProcessAadBytes(aad2);
            offset = ascon.ProcessBytes(c2, m6);
            offset += ascon.DoFinal(m6[offset..]);
            if (!m6[..offset].SequenceEqual(m2))
            {
                Assert.Fail("Decryption should match for the same AAD and message with/without Span-based API");
            }
#endif
        }

        private static void ImplTestExceptionsGetUpdateOutputSize(IAeadCipher ascon, bool forEncryption,
            ICipherParameters parameters, int maxInputSize)
        {
            // TODO Maybe use a different IV for this
            ascon.Init(forEncryption, parameters);

            int maxOutputSize = ascon.GetUpdateOutputSize(maxInputSize);

            byte[] input = new byte[maxInputSize];
            byte[] output = new byte[maxOutputSize];

            for (int inputSize = 0; inputSize <= maxInputSize; ++inputSize)
            {
                // TODO Maybe use a different IV for this
                ascon.Init(forEncryption, parameters);

                int outputSize = ascon.GetUpdateOutputSize(inputSize);
                if (outputSize > 0)
                {
                    try
                    {
                        ascon.ProcessBytes(input, 0, inputSize, output, maxOutputSize - outputSize + 1);
                        Assert.Fail("output for ProcessBytes is too short");
                    }
                    catch (OutputLengthException)
                    {
                        //expected
                    }
                }
                else
                {
                    ascon.ProcessBytes(input, 0, inputSize, null, 0);
                }
            }
        }

        private static void ImplTestExceptionsXof(IXof ascon)
        {
            try
            {
                ascon.BlockUpdate(new byte[1], 1, 1);
                Assert.Fail(ascon.AlgorithmName + ": input for BlockUpdate is too short");
            }
            catch (DataLengthException)
            {
                //expected
            }

            try
            {
                ascon.DoFinal(new byte[ascon.GetDigestSize() - 1], 2);
                Assert.Fail(ascon.AlgorithmName + ": output for DoFinal is too short");
            }
            catch (OutputLengthException)
            {
                //expected
            }
        }

        private static void ImplTestOutputXof(IXof ascon)
        {
            Random random = new Random();

            byte[] expected = new byte[64];
            ascon.OutputFinal(expected, 0, expected.Length);

            byte[] output = new byte[64];
            for (int i = 0; i < 64; ++i)
            {
                random.NextBytes(output);

                int pos = 0;
                while (pos <= output.Length - 16)
                {
                    int len = random.Next(0, 17);
                    ascon.Output(output, pos, len);
                    pos += len;
                }

                ascon.OutputFinal(output, pos, output.Length - pos);

                Assert.True(Arrays.AreEqual(expected, output));
            }
        }

        private static void ImplTestParametersDigest(IDigest ascon, int digestSize)
        {
            Assert.AreEqual(digestSize, ascon.GetDigestSize(), ascon.AlgorithmName + ": digest size is not correct");
        }

        private static void ImplTestParametersEngine(IAeadCipher ascon, int keySize,
            int ivSize, int macSize)
        {
            Assert.AreEqual(keySize, GetKeyBytesSize(ascon),
                "key bytes of " + ascon.AlgorithmName + " is not correct");
            Assert.AreEqual(ivSize, GetIVBytesSize(ascon),
                "iv bytes of " + ascon.AlgorithmName + " is not correct");

            var parameters = new ParametersWithIV(new KeyParameter(new byte[keySize]), new byte[ivSize]);

            ascon.Init(true, parameters);
            Assert.AreEqual(macSize, ascon.GetOutputSize(0),
                "GetOutputSize of " + ascon.AlgorithmName + " is incorrect for encryption");

            ascon.Init(false, parameters);
            Assert.AreEqual(0, ascon.GetOutputSize(macSize),
                "GetOutputSize of " + ascon.AlgorithmName + " is incorrect for decryption");
        }

        private static void ImplTestParametersXof(IXof ascon, int digestSize)
        {
            Assert.AreEqual(digestSize, ascon.GetDigestSize(),
                ascon.AlgorithmName + ": digest size is not correct");
        }

        private static void ImplTestVectorsDigest(IDigest ascon, string path, string filename)
        {
            Random random = new Random();
            var map = new Dictionary<string, string>();
            using (var src = new StreamReader(SimpleTest.FindTestResource(path, filename)))
            {
                string line;
                while ((line = src.ReadLine()) != null)
                {
                    var data = line.Split(' ');
                    if (data.Length == 1)
                    {
                        string count = map["Count"];
                        byte[] ptByte = Hex.Decode(map["Msg"]);
                        byte[] expected = Hex.Decode(map["MD"]);
                        map.Clear();

                        byte[] hash = new byte[ascon.GetDigestSize()];

                        ascon.BlockUpdate(ptByte, 0, ptByte.Length);
                        ascon.DoFinal(hash, 0);
                        Assert.True(Arrays.AreEqual(expected, hash));

                        if (ptByte.Length > 1)
                        {
                            int split = random.Next(1, ptByte.Length);
                            ascon.BlockUpdate(ptByte, 0, split);
                            ascon.BlockUpdate(ptByte, split, ptByte.Length - split);
                            ascon.DoFinal(hash, 0);
                            Assert.True(Arrays.AreEqual(expected, hash));
                        }
                    }
                    else
                    {
                        if (data.Length >= 3)
                        {
                            map[data[0].Trim()] = data[2].Trim();
                        }
                        else
                        {
                            map[data[0].Trim()] = "";
                        }
                    }
                }
            }
        }

        private static void ImplTestVectorsEngine(CipherTest.CreateAeadCipher create, string path, string filename)
        {
            Random random = new Random();
            var asconEngine = create();
            var buf = new Dictionary<string, string>();
            using (var src = new StreamReader(SimpleTest.FindTestResource(path, filename)))
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                string line;
                while ((line = src.ReadLine()) != null)
                {
                    var data = line.Split(' ');
                    if (data.Length == 1)
                    {
                        string count = map["Count"];
                        byte[] key = Hex.Decode(map["Key"]);
                        byte[] nonce = Hex.Decode(map["Nonce"]);
                        byte[] ad = Hex.Decode(map["AD"]);
                        byte[] pt = Hex.Decode(map["PT"]);
                        byte[] ct = Hex.Decode(map["CT"]);
                        map.Clear();

                        var parameters = new ParametersWithIV(new KeyParameter(key), nonce);

                        // Encrypt
                        {
                            asconEngine.Init(true, parameters);

                            var rv = new byte[asconEngine.GetOutputSize(pt.Length)];
                            random.NextBytes(rv); // should overwrite any existing data

                            asconEngine.ProcessAadBytes(ad, 0, ad.Length);
                            int len = asconEngine.ProcessBytes(pt, 0, pt.Length, rv, 0);
                            len += asconEngine.DoFinal(rv, len);

                            Assert.True(Arrays.AreEqual(rv, 0, len, ct, 0, ct.Length));
                        }

                        // Decrypt
                        {
                            asconEngine.Init(false, parameters);

                            var rv = new byte[asconEngine.GetOutputSize(ct.Length)];
                            random.NextBytes(rv); // should overwrite any existing data

                            asconEngine.ProcessAadBytes(ad, 0, ad.Length);
                            int len = asconEngine.ProcessBytes(ct, 0, ct.Length, rv, 0);
                            len += asconEngine.DoFinal(rv, len);

                            Assert.True(Arrays.AreEqual(rv, 0, len, pt, 0, pt.Length));
                        }
                    }
                    else
                    {
                        if (data.Length >= 3)
                        {
                            map[data[0].Trim()] = data[2].Trim();
                        }
                        else
                        {
                            map[data[0].Trim()] = "";
                        }
                    }
                }
            }
        }

        private static void ImplTestVectorsXof(IXof ascon, string path, string filename)
        {
            Random random = new Random();

            var buf = new Dictionary<string, string>();
            using (var src = new StreamReader(SimpleTest.FindTestResource(path, filename)))
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                string line;
                while ((line = src.ReadLine()) != null)
                {
                    var data = line.Split(' ');
                    if (data.Length == 1)
                    {
                        string count = map["Count"];
                        byte[] input = Hex.Decode(map["Msg"]);
                        byte[] expected = Hex.Decode(map["MD"]);
                        map.Clear();

                        ImplTestVectorXof(random, ascon, input, expected);
                    }
                    else
                    {
                        if (data.Length >= 3)
                        {
                            map[data[0].Trim()] = data[2].Trim();
                        }
                        else
                        {
                            map[data[0].Trim()] = "";
                        }
                    }
                }
            }
        }

        private static void ImplTestVectorsCXof128(string path, string filename)
        {
            Random random = new Random();

            var buf = new Dictionary<string, string>();
            using (var src = new StreamReader(SimpleTest.FindTestResource(path, filename)))
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                string line;
                while ((line = src.ReadLine()) != null)
                {
                    var data = line.Split(' ');
                    if (data.Length == 1)
                    {
                        string count = map["Count"];
                        byte[] input = Hex.Decode(map["Msg"]);
                        byte[] z = Hex.Decode(map["Z"]);
                        byte[] expected = Hex.Decode(map["MD"]);
                        map.Clear();

                        var ascon = new AsconCXof128(z);

                        ImplTestVectorXof(random, ascon, input, expected);
                    }
                    else
                    {
                        if (data.Length >= 3)
                        {
                            map[data[0].Trim()] = data[2].Trim();
                        }
                        else
                        {
                            map[data[0].Trim()] = "";
                        }
                    }
                }
            }
        }

        private static void ImplTestVectorXof(Random random, IXof ascon, byte[] input, byte[] expected)
        {
            int outputLength = expected.Length;
            byte[] output = new byte[outputLength];

            {
                random.NextBytes(output);

                ascon.BlockUpdate(input, 0, input.Length);
                ascon.OutputFinal(output, 0, outputLength);
                Assert.True(Arrays.AreEqual(expected, output));
            }

            if (input.Length > 1)
            {
                random.NextBytes(output);

                int splitInput = random.Next(1, input.Length);
                ascon.BlockUpdate(input, 0, splitInput);
                ascon.BlockUpdate(input, splitInput, input.Length - splitInput);
                ascon.OutputFinal(output, 0, outputLength);
                Assert.True(Arrays.AreEqual(expected, output));
            }

            if (outputLength > 1)
            {
                random.NextBytes(output);

                int splitOutput = random.Next(1, outputLength);
                ascon.BlockUpdate(input, 0, input.Length);
                ascon.Output(output, 0, splitOutput);
                ascon.OutputFinal(output, splitOutput, outputLength - splitOutput);
                Assert.True(Arrays.AreEqual(expected, output));
            }
        }

        private static void InitEngine(IAeadCipher ascon, bool forEncryption)
        {
            int keySize = GetKeyBytesSize(ascon);
            int ivSize = GetIVBytesSize(ascon);
            int macSize = ivSize * 8;

            var parameters = new AeadParameters(new KeyParameter(new byte[keySize]), macSize, new byte[ivSize], null);
            ascon.Init(forEncryption, parameters);
        }
    }
}
