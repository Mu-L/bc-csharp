using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	public class PkiFreeText
		: Asn1Encodable
	{
		public static PkiFreeText GetInstance(object obj)
		{
			if (obj == null)
				return null;
			if (obj is PkiFreeText pkiFreeText)
				return pkiFreeText;
            return new PkiFreeText(Asn1Sequence.GetInstance(obj));
		}

        public static PkiFreeText GetInstance(Asn1TaggedObject taggedObject, bool declaredExplicit) =>
            new PkiFreeText(Asn1Sequence.GetInstance(taggedObject, declaredExplicit));

        public static PkiFreeText GetOptional(Asn1Encodable element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element is PkiFreeText pkiFreeText)
                return pkiFreeText;

            Asn1Sequence asn1Sequence = Asn1Sequence.GetOptional(element);
            if (asn1Sequence != null)
                return new PkiFreeText(asn1Sequence);

            return null;
        }

        public static PkiFreeText GetTagged(Asn1TaggedObject taggedObject, bool declaredExplicit) =>
            new PkiFreeText(Asn1Sequence.GetTagged(taggedObject, declaredExplicit));

        private readonly Asn1Sequence m_strings;

        internal PkiFreeText(Asn1Sequence seq)
		{
			foreach (var element in seq)
			{
				if (!(element is DerUtf8String))
					throw new ArgumentException("attempt to insert non UTF8 STRING into PkiFreeText");
			}

			m_strings = seq;
		}

		public PkiFreeText(DerUtf8String p)
		{
			m_strings = new DerSequence(p);
		}

		public PkiFreeText(string p)
			: this(new DerUtf8String(p))
		{
		}

		public PkiFreeText(DerUtf8String[] strs)
		{
			m_strings = new DerSequence(strs);
		}

        public PkiFreeText(string[] strs)
        {
            m_strings = DerSequence.Map(strs, str => new DerUtf8String(str));
        }

		public virtual int Count => m_strings.Count;

		/**
		 * Return the UTF8STRING at index.
		 *
		 * @param index index of the string of interest
		 * @return the string at index.
		 */
		public DerUtf8String this[int index] => (DerUtf8String)m_strings[index];

		/**
		 * <pre>
		 * PkiFreeText ::= SEQUENCE SIZE (1..MAX) OF UTF8String
		 * </pre>
		 */
		public override Asn1Object ToAsn1Object() => m_strings;
	}
}
