using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Org.BouncyCastle.Crypto;
using Org.Bouncycastle.Pqc.Crypto.Sike;
using Org.BouncyCastle.Pqc.Crypto.Utilities;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.Test;

namespace Org.BouncyCastle.Pqc.Crypto.Tests
{
    [TestFixture]
    public class SikeVectorTest
    {
        
        private static readonly Dictionary<string, SIKEParameters> parameters = new Dictionary<string, SIKEParameters>()
        {
            { "PQCkemKAT_374.rsp" , SIKEParameters.sikep434 },
            { "PQCkemKAT_434.rsp" , SIKEParameters.sikep503 },
            { "PQCkemKAT_524.rsp" , SIKEParameters.sikep610 },
            { "PQCkemKAT_644.rsp" , SIKEParameters.sikep751 },
            { "PQCkemKAT_350.rsp" , SIKEParameters.sikep434_compressed },
            { "PQCkemKAT_407.rsp" , SIKEParameters.sikep503_compressed },
            { "PQCkemKAT_491.rsp" , SIKEParameters.sikep610_compressed },
            { "PQCkemKAT_602.rsp" , SIKEParameters.sikep751_compressed }
        };

        private static readonly string[] TestVectorFilesBasic =
        {
            "PQCkemKAT_374.rsp",
            "PQCkemKAT_434.rsp",
            "PQCkemKAT_524.rsp",
            "PQCkemKAT_644.rsp",
        };

        private static readonly string[] TestVectorFilesCompressed =
        {
            "PQCkemKAT_350.rsp",
            "PQCkemKAT_407.rsp",
            "PQCkemKAT_491.rsp",
            "PQCkemKAT_602.rsp",
        };

        [TestCaseSource(nameof(TestVectorFilesBasic))]
        [Parallelizable(ParallelScope.All)]
        public void TestVectorsBasic(string testVectorFile)
        {
            RunTestVectorFile(testVectorFile);
        }

        [TestCaseSource(nameof(TestVectorFilesCompressed))]
        [Parallelizable(ParallelScope.All)]
        public void TestVectorCompressed(string testVectorFile)
        {
            RunTestVectorFile(testVectorFile);
        }

        private static void RunTestVector(string name, IDictionary<string, string> buf)
        {
            string count = buf["count"];
            byte[] seed = Hex.Decode(buf["seed"]);      // seed for SIKE secure random
            byte[] pk = Hex.Decode(buf["pk"]);          // public key
            byte[] sk = Hex.Decode(buf["sk"]);          // private key
            byte[] ct = Hex.Decode(buf["ct"]);          // cipher text
            byte[] ss = Hex.Decode(buf["ss"]);          // session key

            NistSecureRandom random = new NistSecureRandom(seed, null);
            SIKEParameters SIKEParameters = parameters[name];

            SIKEKeyPairGenerator kpGen = new SIKEKeyPairGenerator();
            SIKEKeyGenerationParameters genParams = new SIKEKeyGenerationParameters(random, SIKEParameters);

            //
            // Generate keys and test.
            //
            kpGen.Init(genParams);
            AsymmetricCipherKeyPair kp = kpGen.GenerateKeyPair();

            // todo
            // SIKEPublicKeyParameters pubParams = (SIKEPublicKeyParameters)PublicKeyFactory.CreateKey(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(kp.Public));
            // SIKEPrivateKeyParameters privParams = (SIKEPrivateKeyParameters)PrivateKeyFactory.CreateKey(PrivateKeyInfoFactory.CreatePrivateKeyInfo(kp.Private));

            SIKEPublicKeyParameters pubParams = (SIKEPublicKeyParameters)kp.Public;
            SIKEPrivateKeyParameters privParams = (SIKEPrivateKeyParameters)kp.Private;

            // Console.WriteLine(Hex.ToHexString(pk));
            // Console.WriteLine(Hex.ToHexString(pubParams.GetEncoded()));
            Assert.True(Arrays.AreEqual(pk, pubParams.GetEncoded()), name + " " + count + ": public key");
            Assert.True(Arrays.AreEqual(sk, privParams.GetEncoded()), name + " " + count + ": secret key");

            // KEM Enc
            SIKEKEMGenerator sikeEncCipher = new SIKEKEMGenerator(random);
            ISecretWithEncapsulation secWenc = sikeEncCipher.GenerateEncapsulated(pubParams);
            byte[] generated_cipher_text = secWenc.GetEncapsulation();


//                        System.out.println(Hex.toHexString(ct));
//                        System.out.println(Hex.toHexString(generated_cipher_text));

            Assert.True(Arrays.AreEqual(ct, generated_cipher_text), name + " " + count + ": kem_enc cipher text");
            byte[] secret = secWenc.GetSecret();

//                        System.out.println(Hex.toHexString(ss).toUpperCase());
//                        System.out.println(Hex.toHexString(secret).toUpperCase());
            Assert.True(Arrays.AreEqual(ss, secret), name + " " + count + ": kem_enc key");

            // KEM Dec
            SIKEKEMExtractor sikeDecCipher = new SIKEKEMExtractor(privParams);

            byte[] dec_key = sikeDecCipher.ExtractSecret(generated_cipher_text);

//                        System.out.println(Hex.toHexString(dec_key).toUpperCase());
//                        System.out.println(Hex.toHexString(ss).toUpperCase());

            Assert.True(Arrays.AreEqual(dec_key, ss), name + " " + count + ": kem_dec ss" );
            Assert.True(Arrays.AreEqual(dec_key, secret), name + " " + count + ": kem_dec key" );
        
        }
        
        
        private static void RunTestVectorFile(string name)
        {
            var buf = new Dictionary<string, string>();

            using (var src = new StreamReader(SimpleTest.GetTestDataAsStream("pqc.sike." + name)))
            {
                string line;
                while ((line = src.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("#"))
                        continue;

                    if (line.Length > 0)
                    {
                        int a = line.IndexOf('=');
                        if (a > -1)
                        {
                            buf[line.Substring(0, a).Trim()] = line.Substring(a + 1).Trim();
                        }
                        continue;
                    }

                    if (buf.Count > 0)
                    {
                        RunTestVector(name, buf);
                        buf.Clear();
                    }
                }

                if (buf.Count > 0)
                {
                    RunTestVector(name, buf);
                }
            }
        }
    }
}