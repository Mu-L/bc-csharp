using System;

namespace Org.BouncyCastle.Asn1.BC
{
    // TODO[api] Make static
    public abstract class BCObjectIdentifiers
	{
        /**
         * iso.org.dod.internet.private.enterprise.legion-of-the-bouncy-castle
         * <p>1.3.6.1.4.1.22554</p>
         */
        public static readonly DerObjectIdentifier bc = new DerObjectIdentifier("1.3.6.1.4.1.22554");

        /**
         * pbe(1) algorithms
         * <p>1.3.6.1.4.1.22554.1</p>
         */
        public static readonly DerObjectIdentifier bc_pbe        = bc.Branch("1");

        /**
         * SHA-1(1)
         * <p>1.3.6.1.4.1.22554.1.1</p>
         */
        public static readonly DerObjectIdentifier bc_pbe_sha1   = bc_pbe.Branch("1");

        /** SHA-2.SHA-256; 1.3.6.1.4.1.22554.1.2.1 */
        public static readonly DerObjectIdentifier bc_pbe_sha256 = bc_pbe.Branch("2.1");
        /** SHA-2.SHA-384; 1.3.6.1.4.1.22554.1.2.2 */
        public static readonly DerObjectIdentifier bc_pbe_sha384 = bc_pbe.Branch("2.2");
        /** SHA-2.SHA-512; 1.3.6.1.4.1.22554.1.2.3 */
        public static readonly DerObjectIdentifier bc_pbe_sha512 = bc_pbe.Branch("2.3");
        /** SHA-2.SHA-224; 1.3.6.1.4.1.22554.1.2.4 */
        public static readonly DerObjectIdentifier bc_pbe_sha224 = bc_pbe.Branch("2.4");

        /**
         * PKCS-5(1)|PKCS-12(2)
         */
        /** SHA-1.PKCS5;  1.3.6.1.4.1.22554.1.1.1 */
        public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs5    = bc_pbe_sha1.Branch("1");
        /** SHA-1.PKCS12; 1.3.6.1.4.1.22554.1.1.2 */
        public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12   = bc_pbe_sha1.Branch("2");

        /** SHA-256.PKCS5; 1.3.6.1.4.1.22554.1.2.1.1 */
        public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs5  = bc_pbe_sha256.Branch("1");
        /** SHA-256.PKCS12; 1.3.6.1.4.1.22554.1.2.1.2 */
        public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12 = bc_pbe_sha256.Branch("2");

        /**
         * AES(1) . (CBC-128(2)|CBC-192(22)|CBC-256(42))
         */
        /** 1.3.6.1.4.1.22554.1.1.2.1.2 */
        public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes128_cbc   = bc_pbe_sha1_pkcs12.Branch("1.2");
        /** 1.3.6.1.4.1.22554.1.1.2.1.22 */
        public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes192_cbc   = bc_pbe_sha1_pkcs12.Branch("1.22");
        /** 1.3.6.1.4.1.22554.1.1.2.1.42 */
        public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes256_cbc   = bc_pbe_sha1_pkcs12.Branch("1.42");

        /** 1.3.6.1.4.1.22554.1.1.2.2.2 */
        public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes128_cbc = bc_pbe_sha256_pkcs12.Branch("1.2");
        /** 1.3.6.1.4.1.22554.1.1.2.2.22 */
        public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes192_cbc = bc_pbe_sha256_pkcs12.Branch("1.22");
        /** 1.3.6.1.4.1.22554.1.1.2.2.42 */
        public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes256_cbc = bc_pbe_sha256_pkcs12.Branch("1.42");

        /**
         * signature(2) algorithms
         */
        public static readonly DerObjectIdentifier bc_sig        = bc.Branch("2");

        /**
         * Sphincs-256
         */
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincs256                      = bc_sig.Branch("1");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincs256_with_BLAKE512        = sphincs256.Branch("1");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincs256_with_SHA512          = sphincs256.Branch("2");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincs256_with_SHA3_512        = sphincs256.Branch("3");

        /**
         * XMSS
         */
        public static readonly DerObjectIdentifier xmss = bc_sig.Branch("2");
        public static readonly DerObjectIdentifier xmss_SHA256ph = xmss.Branch("1");
        public static readonly DerObjectIdentifier xmss_SHA512ph = xmss.Branch("2");
        public static readonly DerObjectIdentifier xmss_SHAKE128_512ph = xmss.Branch("3");
        public static readonly DerObjectIdentifier xmss_SHAKE256_1024ph = xmss.Branch("4");
        public static readonly DerObjectIdentifier xmss_SHA256 = xmss.Branch("5");
        public static readonly DerObjectIdentifier xmss_SHA512 = xmss.Branch("6");
        public static readonly DerObjectIdentifier xmss_SHAKE128 = xmss.Branch("7");
        public static readonly DerObjectIdentifier xmss_SHAKE256 = xmss.Branch("8");
        public static readonly DerObjectIdentifier xmss_SHAKE128ph = xmss.Branch("9");
        public static readonly DerObjectIdentifier xmss_SHAKE256ph = xmss.Branch("10");

        /**
         * XMSS^MT
         */
        public static readonly DerObjectIdentifier xmss_mt = bc_sig.Branch("3");
        public static readonly DerObjectIdentifier xmss_mt_SHA256ph = xmss_mt.Branch("1");
        public static readonly DerObjectIdentifier xmss_mt_SHA512ph = xmss_mt.Branch("2");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE128_512ph = xmss_mt.Branch("3");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE256_1024ph = xmss_mt.Branch("4");
        public static readonly DerObjectIdentifier xmss_mt_SHA256 = xmss_mt.Branch("5");
        public static readonly DerObjectIdentifier xmss_mt_SHA512 = xmss_mt.Branch("6");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE128 = xmss_mt.Branch("7");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE256 = xmss_mt.Branch("8");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE128ph = xmss_mt.Branch("9");
        public static readonly DerObjectIdentifier xmss_mt_SHAKE256ph = xmss_mt.Branch("10");

        [Obsolete("Use 'xmss_SHA256ph' instead")]
        public static readonly DerObjectIdentifier xmss_with_SHA256 = xmss_SHA256ph;
        [Obsolete("Use 'xmss_SHA512ph' instead")]
        public static readonly DerObjectIdentifier xmss_with_SHA512 = xmss_SHA512ph;
        [Obsolete("Use 'xmss_SHAKE128ph' instead")]
        public static readonly DerObjectIdentifier xmss_with_SHAKE128 = xmss_SHAKE128ph;
        [Obsolete("Use 'xmss_SHAKE256ph' instead")]
        public static readonly DerObjectIdentifier xmss_with_SHAKE256 = xmss_SHAKE256ph;

        [Obsolete("Use 'xmss_mt_SHA256ph' instead")]
        public static readonly DerObjectIdentifier xmss_mt_with_SHA256 = xmss_mt_SHA256ph;
        [Obsolete("Use 'xmss_mt_SHA512ph' instead")]
        public static readonly DerObjectIdentifier xmss_mt_with_SHA512 = xmss_mt_SHA512ph;
        [Obsolete("Use 'xmss_mt_SHAKE128ph' instead")]
        public static readonly DerObjectIdentifier xmss_mt_with_SHAKE128 = xmss_mt_SHAKE128ph;
        [Obsolete("Use 'xmss_mt_SHAKE256ph' instead")]
        public static readonly DerObjectIdentifier xmss_mt_with_SHAKE256 = xmss_mt_SHAKE256ph;

        /**
         * qTESLA
         */
        public static readonly DerObjectIdentifier qTESLA = bc_sig.Branch("4");

        public static readonly DerObjectIdentifier qTESLA_Rnd1_I = qTESLA.Branch("1");
        public static readonly DerObjectIdentifier qTESLA_Rnd1_III_size = qTESLA.Branch("2");
        public static readonly DerObjectIdentifier qTESLA_Rnd1_III_speed = qTESLA.Branch("3");
        public static readonly DerObjectIdentifier qTESLA_Rnd1_p_I = qTESLA.Branch("4");
        public static readonly DerObjectIdentifier qTESLA_Rnd1_p_III = qTESLA.Branch("5");

        public static readonly DerObjectIdentifier qTESLA_p_I = qTESLA.Branch("11");
        public static readonly DerObjectIdentifier qTESLA_p_III = qTESLA.Branch("12");

        /**
         * SPHINCS+
         */
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus = bc_sig.Branch("5");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128s_r3 = sphincsPlus.Branch("1");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128f_r3 = sphincsPlus.Branch("2");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128s_r3 = sphincsPlus.Branch("3");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128f_r3 = sphincsPlus.Branch("4");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_128s_r3 = sphincsPlus.Branch("5");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_128f_r3 = sphincsPlus.Branch("6");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192s_r3 = sphincsPlus.Branch("7");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192f_r3 = sphincsPlus.Branch("8");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192s_r3 = sphincsPlus.Branch("9");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192f_r3 = sphincsPlus.Branch("10");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_192s_r3 = sphincsPlus.Branch("11");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_192f_r3 = sphincsPlus.Branch("12");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256s_r3 = sphincsPlus.Branch("13");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256f_r3 = sphincsPlus.Branch("14");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256s_r3 = sphincsPlus.Branch("15");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256f_r3 = sphincsPlus.Branch("16");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_256s_r3 = sphincsPlus.Branch("17");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_256f_r3 = sphincsPlus.Branch("18");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128s_r3_simple = sphincsPlus.Branch("19");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128f_r3_simple = sphincsPlus.Branch("20");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128s_r3_simple = sphincsPlus.Branch("21");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128f_r3_simple = sphincsPlus.Branch("22");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_128s_r3_simple = sphincsPlus.Branch("23");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_128f_r3_simple = sphincsPlus.Branch("24");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192s_r3_simple = sphincsPlus.Branch("25");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192f_r3_simple = sphincsPlus.Branch("26");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192s_r3_simple = sphincsPlus.Branch("27");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192f_r3_simple = sphincsPlus.Branch("28");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_192s_r3_simple = sphincsPlus.Branch("29");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_192f_r3_simple = sphincsPlus.Branch("30");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256s_r3_simple = sphincsPlus.Branch("31");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256f_r3_simple = sphincsPlus.Branch("32");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256s_r3_simple = sphincsPlus.Branch("33");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256f_r3_simple = sphincsPlus.Branch("34");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_256s_r3_simple = sphincsPlus.Branch("35");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_haraka_256f_r3_simple = sphincsPlus.Branch("36");

        // Interop OIDs.
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_interop = new DerObjectIdentifier("1.3.9999.6");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128f = new DerObjectIdentifier("1.3.9999.6.4.13");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_128s = new DerObjectIdentifier("1.3.9999.6.4.16");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192f = new DerObjectIdentifier("1.3.9999.6.5.10");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_192s = new DerObjectIdentifier("1.3.9999.6.5.12");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256f = new DerObjectIdentifier("1.3.9999.6.6.10");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_sha2_256s = new DerObjectIdentifier("1.3.9999.6.6.12");

        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128f = new DerObjectIdentifier("1.3.9999.6.7.13");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_128s = new DerObjectIdentifier("1.3.9999.6.7.16");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192f = new DerObjectIdentifier("1.3.9999.6.8.10");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_192s = new DerObjectIdentifier("1.3.9999.6.8.12");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256f = new DerObjectIdentifier("1.3.9999.6.9.10");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256s = new DerObjectIdentifier("1.3.9999.6.9.12");

        [Obsolete("Will be removed - name is erroneous")]
        public static readonly DerObjectIdentifier sphincsPlus_shake_256 = sphincsPlus.Branch("1");
        [Obsolete("Will be removed - name is erroneous")]
        public static readonly DerObjectIdentifier sphincsPlus_sha_256 = sphincsPlus.Branch("2");
        [Obsolete("Will be removed - name is erroneous")]
        public static readonly DerObjectIdentifier sphincsPlus_sha_512 = sphincsPlus.Branch("3");

        /**
         * Picnic
         */
        public static readonly DerObjectIdentifier picnic = bc_sig.Branch("6");

        public static readonly DerObjectIdentifier picnic_key = picnic.Branch("1");

        public static readonly DerObjectIdentifier picnicl1fs = picnic_key.Branch("1");
        public static readonly DerObjectIdentifier picnicl1ur = picnic_key.Branch("2");
        public static readonly DerObjectIdentifier picnicl3fs = picnic_key.Branch("3");
        public static readonly DerObjectIdentifier picnicl3ur = picnic_key.Branch("4");
        public static readonly DerObjectIdentifier picnicl5fs = picnic_key.Branch("5");
        public static readonly DerObjectIdentifier picnicl5ur = picnic_key.Branch("6");
        public static readonly DerObjectIdentifier picnic3l1 = picnic_key.Branch("7");
        public static readonly DerObjectIdentifier picnic3l3 = picnic_key.Branch("8");
        public static readonly DerObjectIdentifier picnic3l5 = picnic_key.Branch("9");
        public static readonly DerObjectIdentifier picnicl1full = picnic_key.Branch("10");
        public static readonly DerObjectIdentifier picnicl3full = picnic_key.Branch("11");
        public static readonly DerObjectIdentifier picnicl5full = picnic_key.Branch("12");

        public static readonly DerObjectIdentifier picnic_signature = picnic.Branch("2");
    
        public static readonly DerObjectIdentifier picnic_with_sha512 = picnic_signature.Branch("1");
        public static readonly DerObjectIdentifier picnic_with_shake256 = picnic_signature.Branch("2");
        public static readonly DerObjectIdentifier picnic_with_sha3_512 = picnic_signature.Branch("3");

        /*
         * Falcon
         */
        public static readonly DerObjectIdentifier falcon = bc_sig.Branch("7");
        /** 1.3.9999.3.11 OQS_OID_FALCON512 */
        public static readonly DerObjectIdentifier falcon_512 = new DerObjectIdentifier("1.3.9999.3.11");
        /** 1.3.9999.3.12 OQS_OID_P256_FALCON512 */
        public static readonly DerObjectIdentifier p256_falcon_512 = new DerObjectIdentifier("1.3.9999.3.12");
        /** 1.3.9999.3.13 OQS_OID_RSA3072_FALCON512 */
        public static readonly DerObjectIdentifier rsa_3072_falcon_512 = new DerObjectIdentifier("1.3.9999.3.13");
        /** 1.3.9999.3.14 OQS_OID_FALCON1024 */
        public static readonly DerObjectIdentifier falcon_1024 = new DerObjectIdentifier("1.3.9999.3.14");
        /** 1.3.9999.3.15 OQS_OID_P521_FALCON1024 */
        public static readonly DerObjectIdentifier p521_falcon1024 = new DerObjectIdentifier("1.3.9999.3.15");
        /** 1.3.9999.3.16 OQS_OID_FALCONPADDED512 */
        public static readonly DerObjectIdentifier falcon_padded_512 = new DerObjectIdentifier("1.3.9999.3.16");
        /** 1.3.9999.3.17 OQS_OID_P256_FALCONPADDED512 */
        public static readonly DerObjectIdentifier p256_falcon_padded512 = new DerObjectIdentifier("1.3.9999.3.17");
        /** 1.3.9999.3.18 OQS_OID_RSA3072_FALCONPADDED512 */
        public static readonly DerObjectIdentifier rsa_3072_falconpadded512 = new DerObjectIdentifier("1.3.9999.3.18");
        /** 1.3.9999.3.19 OQS_OID_FALCONPADDED1024 */
        public static readonly DerObjectIdentifier falcon_padded_1024 = new DerObjectIdentifier("1.3.9999.3.19");
        /** 1.3.9999.3.20 OQS_OID_P521_FALCONPADDED1024 */
        public static readonly DerObjectIdentifier p521_falcon_padded_1024 = new DerObjectIdentifier("1.3.9999.3.20");

        /*
         * Dilithium
         */
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium = bc_sig.Branch("8");

        // OpenSSL OIDs
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium2 = new DerObjectIdentifier("1.3.6.1.4.1.2.267.12.4.4"); // dilithium.branch("1");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium3 = new DerObjectIdentifier("1.3.6.1.4.1.2.267.12.6.5"); // dilithium.branch("2");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium5 = new DerObjectIdentifier("1.3.6.1.4.1.2.267.12.8.7"); // dilithium.branch("3");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium2_aes = new DerObjectIdentifier("1.3.6.1.4.1.2.267.11.4.4"); // dilithium.branch("4");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium3_aes = new DerObjectIdentifier("1.3.6.1.4.1.2.267.11.6.5"); // dilithium.branch("5");
        [Obsolete("Will be removed")]
        public static readonly DerObjectIdentifier dilithium5_aes = new DerObjectIdentifier("1.3.6.1.4.1.2.267.11.8.7"); // dilithium.branch("6");

        /*
         * ML-DSA
         */
        ///** 2.16.840.1.101.3.4.3.17 OQS_OID_MLDSA44 */
        /** 1.3.9999.7.5 OQS_OID_P256_MLDSA44 */
        public static readonly DerObjectIdentifier p256_mldsa44 = new DerObjectIdentifier("1.3.9999.7.5");
        /** 1.3.9999.7.6 OQS_OID_RSA3072_MLDSA44 */
        public static readonly DerObjectIdentifier rsa3072_mldsa44 = new DerObjectIdentifier("1.3.9999.7.6");
        /** 2.16.840.1.114027.80.8.1.1 OQS_OID_MLDSA44_pss2048 */
        public static readonly DerObjectIdentifier mldsa44_pss2048 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.1");
        /** 2.16.840.1.114027.80.8.1.2 OQS_OID_MLDSA44_rsa2048 */
        public static readonly DerObjectIdentifier mldsa44_rsa2048 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.2");
        /** 2.16.840.1.114027.80.8.1.3 OQS_OID_MLDSA44_ed25519 */
        public static readonly DerObjectIdentifier mldsa44_ed25519 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.3");
        /** 2.16.840.1.114027.80.8.1.4 OQS_OID_MLDSA44_p256 */
        public static readonly DerObjectIdentifier mldsa44_p256 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.4");
        /** 2.16.840.1.114027.80.8.1.5 OQS_OID_MLDSA44_bp256 */
        public static readonly DerObjectIdentifier mldsa44_bp256 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.5");
        ///** 2.16.840.1.101.3.4.3.18 OQS_OID_MLDSA65 */
        /** 1.3.9999.7.7 OQS_OID_P384_MLDSA65 */
        public static readonly DerObjectIdentifier p384_mldsa65 = new DerObjectIdentifier("1.3.9999.7.7");
        /** 2.16.840.1.114027.80.8.1.6 OQS_OID_MLDSA65_pss3072 */
        public static readonly DerObjectIdentifier mldsa65_pss3072 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.6");
        /** 2.16.840.1.114027.80.8.1.7 OQS_OID_MLDSA65_rsa3072 */
        public static readonly DerObjectIdentifier mldsa65_rsa3072 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.7");
        /** 2.16.840.1.114027.80.8.1.8 OQS_OID_MLDSA65_p256 */
        public static readonly DerObjectIdentifier mldsa65_p256 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.8");
        /** 2.16.840.1.114027.80.8.1.9 OQS_OID_MLDSA65_bp256 */
        public static readonly DerObjectIdentifier mldsa65_bp256 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.9");
        /** 2.16.840.1.114027.80.8.1.10 OQS_OID_MLDSA65_ed25519 */
        public static readonly DerObjectIdentifier mldsa65_ed25519 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.10");
        ///** 2.16.840.1.101.3.4.3.19 OQS_OID_MLDSA87 */
        /** 1.3.9999.7.8 OQS_OID_P521_MLDSA87 */
        public static readonly DerObjectIdentifier p521_mldsa87 = new DerObjectIdentifier("1.3.9999.7.8");
        /** 2.16.840.1.114027.80.8.1.11 OQS_OID_MLDSA87_p384 */
        public static readonly DerObjectIdentifier mldsa87_p384 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.11");
        /** 2.16.840.1.114027.80.8.1.12 OQS_OID_MLDSA87_bp384 */
        public static readonly DerObjectIdentifier mldsa87_bp384 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.12");
        /** 2.16.840.1.114027.80.8.1.13 OQS_OID_MLDSA87_ed448 */
        public static readonly DerObjectIdentifier mldsa87_ed448 = new DerObjectIdentifier("2.16.840.1.114027.80.8.1.13");

        /*
         * Rainbow
         */
        public static readonly DerObjectIdentifier rainbow = bc_sig.Branch("9");

        public static readonly DerObjectIdentifier rainbow_III_classic = rainbow.Branch("1");
        public static readonly DerObjectIdentifier rainbow_III_circumzenithal = rainbow.Branch("2");
        public static readonly DerObjectIdentifier rainbow_III_compressed = rainbow.Branch("3");
        public static readonly DerObjectIdentifier rainbow_V_classic = rainbow.Branch("4");
        public static readonly DerObjectIdentifier rainbow_V_circumzenithal = rainbow.Branch("5");
        public static readonly DerObjectIdentifier rainbow_V_compressed = rainbow.Branch("6");

        /**
         * key_exchange(3) algorithms
         */
        public static readonly DerObjectIdentifier bc_exch = bc.Branch("3");

        /**
         * NewHope
         */
        public static readonly DerObjectIdentifier newHope = bc_exch.Branch("1");

        /**
         * X.509 extension/certificate types
         * <p/>
         * 1.3.6.1.4.1.22554.4
         */
        public static readonly DerObjectIdentifier bc_ext = bc.Branch("4");

        public static readonly DerObjectIdentifier linkedCertificate = bc_ext.Branch("1");
        public static readonly DerObjectIdentifier external_value = bc_ext.Branch("2");

        /**
         * KEM(5) algorithms
         */
        public static readonly DerObjectIdentifier bc_kem = bc.Branch("5");

        /**
         * Classic McEliece
         */
        public static readonly DerObjectIdentifier pqc_kem_mceliece = bc_kem.Branch("1");

        public static readonly DerObjectIdentifier mceliece348864_r3 = pqc_kem_mceliece.Branch("1");
        public static readonly DerObjectIdentifier mceliece348864f_r3 = pqc_kem_mceliece.Branch("2");
        public static readonly DerObjectIdentifier mceliece460896_r3 = pqc_kem_mceliece.Branch("3");
        public static readonly DerObjectIdentifier mceliece460896f_r3 = pqc_kem_mceliece.Branch("4");
        public static readonly DerObjectIdentifier mceliece6688128_r3 = pqc_kem_mceliece.Branch("5");
        public static readonly DerObjectIdentifier mceliece6688128f_r3 = pqc_kem_mceliece.Branch("6");
        public static readonly DerObjectIdentifier mceliece6960119_r3 = pqc_kem_mceliece.Branch("7");
        public static readonly DerObjectIdentifier mceliece6960119f_r3 = pqc_kem_mceliece.Branch("8");
        public static readonly DerObjectIdentifier mceliece8192128_r3 = pqc_kem_mceliece.Branch("9");
        public static readonly DerObjectIdentifier mceliece8192128f_r3 = pqc_kem_mceliece.Branch("10");

        /**
         * Frodo
         */
        public static readonly DerObjectIdentifier pqc_kem_frodo = bc_kem.Branch("2");

        public static readonly DerObjectIdentifier frodokem640aes = pqc_kem_frodo.Branch("1");
        public static readonly DerObjectIdentifier frodokem640shake = pqc_kem_frodo.Branch("2");
        public static readonly DerObjectIdentifier frodokem976aes = pqc_kem_frodo.Branch("3");
        public static readonly DerObjectIdentifier frodokem976shake = pqc_kem_frodo.Branch("4");
        public static readonly DerObjectIdentifier frodokem1344aes = pqc_kem_frodo.Branch("5");
        public static readonly DerObjectIdentifier frodokem1344shake = pqc_kem_frodo.Branch("6");

        /**
         * SABER
         */
        public static readonly DerObjectIdentifier pqc_kem_saber = bc_kem.Branch("3");

        public static readonly DerObjectIdentifier lightsaberkem128r3 = pqc_kem_saber.Branch("1");
        public static readonly DerObjectIdentifier saberkem128r3 = pqc_kem_saber.Branch("2");
        public static readonly DerObjectIdentifier firesaberkem128r3 = pqc_kem_saber.Branch("3");
        public static readonly DerObjectIdentifier lightsaberkem192r3 = pqc_kem_saber.Branch("4");
        public static readonly DerObjectIdentifier saberkem192r3 = pqc_kem_saber.Branch("5");
        public static readonly DerObjectIdentifier firesaberkem192r3 = pqc_kem_saber.Branch("6");
        public static readonly DerObjectIdentifier lightsaberkem256r3 = pqc_kem_saber.Branch("7");
        public static readonly DerObjectIdentifier saberkem256r3 = pqc_kem_saber.Branch("8");
        public static readonly DerObjectIdentifier firesaberkem256r3 = pqc_kem_saber.Branch("9");
        public static readonly DerObjectIdentifier ulightsaberkemr3 = pqc_kem_saber.Branch("10");
        public static readonly DerObjectIdentifier usaberkemr3 = pqc_kem_saber.Branch("11");
        public static readonly DerObjectIdentifier ufiresaberkemr3 = pqc_kem_saber.Branch("12");
        public static readonly DerObjectIdentifier lightsaberkem90sr3 = pqc_kem_saber.Branch("13");
        public static readonly DerObjectIdentifier saberkem90sr3 = pqc_kem_saber.Branch("14");
        public static readonly DerObjectIdentifier firesaberkem90sr3 = pqc_kem_saber.Branch("15");
        public static readonly DerObjectIdentifier ulightsaberkem90sr3 = pqc_kem_saber.Branch("16");
        public static readonly DerObjectIdentifier usaberkem90sr3 = pqc_kem_saber.Branch("17");
        public static readonly DerObjectIdentifier ufiresaberkem90sr3 = pqc_kem_saber.Branch("18");

        /**
         * NTRU
         */
        public static readonly DerObjectIdentifier pqc_kem_ntru = bc_kem.Branch("5");

        public static readonly DerObjectIdentifier ntruhps2048509 = pqc_kem_ntru.Branch("1");
        public static readonly DerObjectIdentifier ntruhps2048677 = pqc_kem_ntru.Branch("2");
        public static readonly DerObjectIdentifier ntruhps4096821 = pqc_kem_ntru.Branch("3");
        public static readonly DerObjectIdentifier ntruhrss701 = pqc_kem_ntru.Branch("4");
        public static readonly DerObjectIdentifier ntruhps40961229 = pqc_kem_ntru.Branch("5");
        public static readonly DerObjectIdentifier ntruhrss1373 = pqc_kem_ntru.Branch("6");

        /**
         * NTRUPrime
         */
        public static readonly DerObjectIdentifier pqc_kem_ntruprime = bc_kem.Branch("7");

        public static readonly DerObjectIdentifier pqc_kem_ntrulprime = pqc_kem_ntruprime.Branch("1");
        public static readonly DerObjectIdentifier ntrulpr653 = pqc_kem_ntrulprime.Branch("1");
        public static readonly DerObjectIdentifier ntrulpr761 = pqc_kem_ntrulprime.Branch("2");
        public static readonly DerObjectIdentifier ntrulpr857 = pqc_kem_ntrulprime.Branch("3");
        public static readonly DerObjectIdentifier ntrulpr953 = pqc_kem_ntrulprime.Branch("4");
        public static readonly DerObjectIdentifier ntrulpr1013 = pqc_kem_ntrulprime.Branch("5");
        public static readonly DerObjectIdentifier ntrulpr1277 = pqc_kem_ntrulprime.Branch("6");

        public static readonly DerObjectIdentifier pqc_kem_sntruprime = pqc_kem_ntruprime.Branch("2");
        public static readonly DerObjectIdentifier sntrup653 = pqc_kem_sntruprime.Branch("1");
        public static readonly DerObjectIdentifier sntrup761 = pqc_kem_sntruprime.Branch("2");
        public static readonly DerObjectIdentifier sntrup857 = pqc_kem_sntruprime.Branch("3");
        public static readonly DerObjectIdentifier sntrup953 = pqc_kem_sntruprime.Branch("4");
        public static readonly DerObjectIdentifier sntrup1013 = pqc_kem_sntruprime.Branch("5");
        public static readonly DerObjectIdentifier sntrup1277 = pqc_kem_sntruprime.Branch("6");

        /**
         * BIKE
         */
        public static readonly DerObjectIdentifier pqc_kem_bike = bc_kem.Branch("8");

        public static readonly DerObjectIdentifier bike128 = pqc_kem_bike.Branch("1");
        public static readonly DerObjectIdentifier bike192 = pqc_kem_bike.Branch("2");
        public static readonly DerObjectIdentifier bike256 = pqc_kem_bike.Branch("3");

        /**
         * HQC
         */
        public static readonly DerObjectIdentifier pqc_kem_hqc = bc_kem.Branch("9");

        public static readonly DerObjectIdentifier hqc128 = pqc_kem_hqc.Branch("1");
        public static readonly DerObjectIdentifier hqc192 = pqc_kem_hqc.Branch("2");
        public static readonly DerObjectIdentifier hqc256 = pqc_kem_hqc.Branch("3");

        /**
         * Mayo
         */
        public static readonly DerObjectIdentifier mayo = bc_sig.Branch("10");
        public static readonly DerObjectIdentifier mayo1 = mayo.Branch("1");
        public static readonly DerObjectIdentifier mayo2 = mayo.Branch("2");
        public static readonly DerObjectIdentifier mayo3 = mayo.Branch("3");
        public static readonly DerObjectIdentifier mayo5 = mayo.Branch("4");
        /** 1.3.9999.8.1.3 OQS_OID_MAYO1 */
        public static readonly DerObjectIdentifier mayo_1 = new DerObjectIdentifier("1.3.9999.8.1.3");
        /** 1.3.9999.8.1.4 OQS_OID_P256_MAYO1 */
        public static readonly DerObjectIdentifier p256_mayo1 = new DerObjectIdentifier("1.3.9999.8.1.4");
        /** 1.3.9999.8.2.3 OQS_OID_MAYO2 */
        public static readonly DerObjectIdentifier mayo_2 = new DerObjectIdentifier("1.3.9999.8.2.3");
        /** 1.3.9999.8.2.4 OQS_OID_P256_MAYO2 */
        public static readonly DerObjectIdentifier p256_mayo2 = new DerObjectIdentifier("1.3.9999.8.2.4");
        /** 1.3.9999.8.3.3 OQS_OID_MAYO3 */
        public static readonly DerObjectIdentifier mayo_3 = new DerObjectIdentifier("1.3.9999.8.3.3");
        /** 1.3.9999.8.3.4 OQS_OID_P384_MAYO3 */
        public static readonly DerObjectIdentifier p384_mayo3 = new DerObjectIdentifier("1.3.9999.8.3.4");
        /** 1.3.9999.8.5.3 OQS_OID_MAYO5 */
        public static readonly DerObjectIdentifier mayo_5 = new DerObjectIdentifier("1.3.9999.8.5.3");
        /** 1.3.9999.8.5.4 OQS_OID_P521_MAYO5 */
        public static readonly DerObjectIdentifier p521_mayo5 = new DerObjectIdentifier("1.3.9999.8.5.4");

        /**
         * Snova
         */
        public static readonly DerObjectIdentifier snova = bc_sig.Branch("11");
        public static readonly DerObjectIdentifier snova_24_5_4_ssk = snova.Branch("1");
        public static readonly DerObjectIdentifier snova_24_5_4_esk = snova.Branch("2");
        public static readonly DerObjectIdentifier snova_24_5_4_shake_ssk = snova.Branch("3");
        public static readonly DerObjectIdentifier snova_24_5_4_shake_esk = snova.Branch("4");
        public static readonly DerObjectIdentifier snova_24_5_5_ssk = snova.Branch("5");
        public static readonly DerObjectIdentifier snova_24_5_5_esk = snova.Branch("6");
        public static readonly DerObjectIdentifier snova_24_5_5_shake_ssk = snova.Branch("7");
        public static readonly DerObjectIdentifier snova_24_5_5_shake_esk = snova.Branch("8");
        public static readonly DerObjectIdentifier snova_25_8_3_ssk = snova.Branch("9");
        public static readonly DerObjectIdentifier snova_25_8_3_esk = snova.Branch("10");
        public static readonly DerObjectIdentifier snova_25_8_3_shake_ssk = snova.Branch("11");
        public static readonly DerObjectIdentifier snova_25_8_3_shake_esk = snova.Branch("12");
        public static readonly DerObjectIdentifier snova_29_6_5_ssk = snova.Branch("13");
        public static readonly DerObjectIdentifier snova_29_6_5_esk = snova.Branch("14");
        public static readonly DerObjectIdentifier snova_29_6_5_shake_ssk = snova.Branch("15");
        public static readonly DerObjectIdentifier snova_29_6_5_shake_esk = snova.Branch("16");
        public static readonly DerObjectIdentifier snova_37_8_4_ssk = snova.Branch("17");
        public static readonly DerObjectIdentifier snova_37_8_4_esk = snova.Branch("18");
        public static readonly DerObjectIdentifier snova_37_8_4_shake_ssk = snova.Branch("19");
        public static readonly DerObjectIdentifier snova_37_8_4_shake_esk = snova.Branch("20");
        public static readonly DerObjectIdentifier snova_37_17_2_ssk = snova.Branch("21");
        public static readonly DerObjectIdentifier snova_37_17_2_esk = snova.Branch("22");
        public static readonly DerObjectIdentifier snova_37_17_2_shake_ssk = snova.Branch("23");
        public static readonly DerObjectIdentifier snova_37_17_2_shake_esk = snova.Branch("24");
        public static readonly DerObjectIdentifier snova_49_11_3_ssk = snova.Branch("25");
        public static readonly DerObjectIdentifier snova_49_11_3_esk = snova.Branch("26");
        public static readonly DerObjectIdentifier snova_49_11_3_shake_ssk = snova.Branch("27");
        public static readonly DerObjectIdentifier snova_49_11_3_shake_esk = snova.Branch("28");
        public static readonly DerObjectIdentifier snova_56_25_2_ssk = snova.Branch("29");
        public static readonly DerObjectIdentifier snova_56_25_2_esk = snova.Branch("30");
        public static readonly DerObjectIdentifier snova_56_25_2_shake_ssk = snova.Branch("31");
        public static readonly DerObjectIdentifier snova_56_25_2_shake_esk = snova.Branch("32");
        public static readonly DerObjectIdentifier snova_60_10_4_ssk = snova.Branch("33");
        public static readonly DerObjectIdentifier snova_60_10_4_esk = snova.Branch("34");
        public static readonly DerObjectIdentifier snova_60_10_4_shake_ssk = snova.Branch("35");
        public static readonly DerObjectIdentifier snova_60_10_4_shake_esk = snova.Branch("36");
        public static readonly DerObjectIdentifier snova_66_15_3_ssk = snova.Branch("37");
        public static readonly DerObjectIdentifier snova_66_15_3_esk = snova.Branch("38");
        public static readonly DerObjectIdentifier snova_66_15_3_shake_ssk = snova.Branch("39");
        public static readonly DerObjectIdentifier snova_66_15_3_shake_esk = snova.Branch("40");
        public static readonly DerObjectIdentifier snova_75_33_2_ssk = snova.Branch("41");
        public static readonly DerObjectIdentifier snova_75_33_2_esk = snova.Branch("42");
        public static readonly DerObjectIdentifier snova_75_33_2_shake_ssk = snova.Branch("43");
        public static readonly DerObjectIdentifier snova_75_33_2_shake_esk = snova.Branch("44");
    }
}
