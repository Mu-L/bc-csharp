using System;
using System.Collections.Generic;
using System.IO;

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
    /**
     * General class for generating a CMS enveloped-data message stream.
     * <p>
     * A simple example of usage.
     * <pre>
     *     CmsEnvelopedDataStreamGenerator edGen = new CmsEnvelopedDataStreamGenerator();
     *     edGen.AddKeyTransRecipient(cert);
     *
     *     MemoryStream  bOut = new MemoryStream();
     *
     *     Stream out = edGen.Open(bOut, CMSEnvelopedGenerator.AES128_CBC);
     *     out.Write(data);
     *     out.Close();
     * </pre>
     * </p>
     */
    public class CmsEnvelopedDataStreamGenerator
        : CmsEnvelopedGenerator
    {
        private object _originatorInfo = null;
        private object _unprotectedAttributes = null;
        private int _bufferSize;
        private bool _berEncodeRecipientSet;

        public CmsEnvelopedDataStreamGenerator()
        {
        }

        /// <summary>Constructor allowing specific source of randomness</summary>
        /// <param name="random">Instance of <c>SecureRandom</c> to use.</param>
        public CmsEnvelopedDataStreamGenerator(SecureRandom random)
            : base(random)
        {
        }

        /// <summary>Set the underlying string size for encapsulated data.</summary>
        /// <param name="bufferSize">Length of octet strings to buffer the data.</param>
        public void SetBufferSize(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        /// <summary>Use a BER Set to store the recipient information.</summary>
        public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
        {
            _berEncodeRecipientSet = berEncodeRecipientSet;
        }

        /// <summary>
        /// Generate an enveloped object that contains an CMS Enveloped Data
        /// object using the passed in key generator.
        /// </summary>
        private Stream Open(Stream outStream, string encryptionOid, CipherKeyGenerator keyGen)
        {
            byte[] encKeyBytes = keyGen.GenerateKey();
            KeyParameter encKey = ParameterUtilities.CreateKeyParameter(encryptionOid, encKeyBytes);

            Asn1Encodable asn1Params = GenerateAsn1Parameters(encryptionOid, encKeyBytes);

            AlgorithmIdentifier encAlgID = GetAlgorithmIdentifier(encryptionOid, encKey, asn1Params,
                out var cipherParameters);

            // TODO[cms] Do these later when we can write each one out immediately?
            Asn1EncodableVector recipientInfos = new Asn1EncodableVector(recipientInfoGenerators.Count);

            foreach (RecipientInfoGenerator rig in recipientInfoGenerators)
            {
                try
                {
                    recipientInfos.Add(rig.Generate(encKey, m_random));
                }
                catch (InvalidKeyException e)
                {
                    throw new CmsException("key inappropriate for algorithm.", e);
                }
                catch (GeneralSecurityException e)
                {
                    throw new CmsException("error making encrypted content.", e);
                }
            }

            return Open(outStream, encAlgID, cipherParameters, recipientInfos);
        }

        private Stream Open(Stream outStream, AlgorithmIdentifier encAlgID, ICipherParameters cipherParameters,
            Asn1EncodableVector recipientInfos)
        {
            try
            {
                //
                // ContentInfo
                //
                BerSequenceGenerator cGen = new BerSequenceGenerator(outStream);

                cGen.AddObject(CmsObjectIdentifiers.EnvelopedData);

                //
                // Encrypted Data
                //
                BerSequenceGenerator envGen = new BerSequenceGenerator(cGen.GetRawOutputStream(), 0, true);

                bool isV2 = _originatorInfo != null || _unprotectedAttributes != null;
                var version = isV2 ? DerInteger.Two : DerInteger.Zero;

                envGen.AddObject(version);

                Stream envRaw = envGen.GetRawOutputStream();
                using (var recipGen = _berEncodeRecipientSet
                    ? (Asn1Generator)new BerSetGenerator(envRaw)
                    : new DerSetGenerator(envRaw))
                {
                    foreach (Asn1Encodable ae in recipientInfos)
                    {
                        recipGen.AddObject(ae);
                    }
                }

                BerSequenceGenerator eiGen = new BerSequenceGenerator(envRaw);
                eiGen.AddObject(CmsObjectIdentifiers.Data);
                eiGen.AddObject(encAlgID);

                BerOctetStringGenerator octGen = new BerOctetStringGenerator(eiGen.GetRawOutputStream(), 0, false);
                Stream octetOutputStream = octGen.GetOctetOutputStream(_bufferSize);

                IBufferedCipher cipher = CipherUtilities.GetCipher(encAlgID.Algorithm);
                cipher.Init(true, new ParametersWithRandom(cipherParameters, m_random));
                CipherStream cOut = new CipherStream(octetOutputStream, null, cipher);

                return new CmsEnvelopedDataOutputStream(this, cOut, cGen, envGen, eiGen, octGen);
            }
            catch (SecurityUtilityException e)
            {
                throw new CmsException("couldn't create cipher.", e);
            }
            catch (InvalidKeyException e)
            {
                throw new CmsException("key invalid in message.", e);
            }
            catch (IOException e)
            {
                throw new CmsException("exception decoding algorithm parameters.", e);
            }
        }

        /**
         * generate an enveloped object that contains an CMS Enveloped Data object
         * @throws IOException
         */
        public Stream Open(Stream outStream, string encryptionOid)
        {
            CipherKeyGenerator keyGen = GeneratorUtilities.GetKeyGenerator(encryptionOid);

            keyGen.Init(new KeyGenerationParameters(m_random, keyGen.DefaultStrength));

            return Open(outStream, encryptionOid, keyGen);
        }

        /**
         * generate an enveloped object that contains an CMS Enveloped Data object
         * @throws IOException
         */
        public Stream Open(Stream outStream, string encryptionOid, int keySize)
        {
            CipherKeyGenerator keyGen = GeneratorUtilities.GetKeyGenerator(encryptionOid);

            keyGen.Init(new KeyGenerationParameters(m_random, keySize));

            return Open(outStream, encryptionOid, keyGen);
        }

        private class CmsEnvelopedDataOutputStream
            : BaseOutputStream
        {
            private readonly CmsEnvelopedGenerator _outer;

            private readonly CipherStream _out;
            private readonly BerSequenceGenerator _cGen;
            private readonly BerSequenceGenerator _envGen;
            private readonly BerSequenceGenerator _eiGen;
            private readonly BerOctetStringGenerator _octGen;

            public CmsEnvelopedDataOutputStream(
                CmsEnvelopedGenerator outer,
                CipherStream outStream,
                BerSequenceGenerator cGen,
                BerSequenceGenerator envGen,
                BerSequenceGenerator eiGen,
                BerOctetStringGenerator octGen)
            {
                _outer = outer;
                _out = outStream;
                _cGen = cGen;
                _envGen = envGen;
                _eiGen = eiGen;
                _octGen = octGen;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _out.Write(buffer, offset, count);
            }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            public override void Write(ReadOnlySpan<byte> buffer)
            {
                _out.Write(buffer);
            }
#endif

            public override void WriteByte(byte value)
            {
                _out.WriteByte(value);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _out.Dispose();

                    // TODO Parent context(s) should really be closed explicitly

                    _octGen.Dispose();
                    _eiGen.Dispose();

                    if (_outer.unprotectedAttributeGenerator != null)
                    {
                        Asn1.Cms.AttributeTable attrTable = _outer.unprotectedAttributeGenerator.GetAttributes(
                            new Dictionary<CmsAttributeTableParameter, object>());

                        Asn1Set unprotectedAttrs = BerSet.FromCollection(attrTable);

                        _envGen.AddObject(new DerTaggedObject(false, 1, unprotectedAttrs));
                    }

                    _envGen.Dispose();
                    _cGen.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}
