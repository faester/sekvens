using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sekvens
{
    public class Sequence
    {
        private int[] _bits;
        private int _basesStored;
        private readonly string _bases = "";

        private static readonly int AMask = 0b0001;
        private static readonly int CMask = 0b0010;
        private static readonly int GMask = 0b0100;
        private static readonly int TMask = 0b1000;

        public Sequence(string bases)
        {
            if (!bases.All(x => x == 'C' || x == 'A' || x == 'T' || x == 'G'))
            {
                throw new ArgumentException("");
            }

            _bits = new int[bases.Length / 8 + 1];
            _basesStored = bases.Length;

            PopulateBitArray(bases);
        }

        private void PopulateBitArray(string bases)
        {
            for (var i = 0; i < _basesStored; i++)
            {
                var ap = i / 8;
                var ip = i % 8;
                var mask = CMask;
                switch (bases[i])
                {
                    case 'C':
                        mask = CMask;
                        break;
                    case 'A':
                        mask = AMask;
                        break;
                    case 'T':
                        mask = TMask;
                        break;
                    case 'G':
                        mask = GMask;
                        break;
                    default: 
                        throw new ArgumentException("God forgot " + bases[i]);
                }

                mask = mask << (ip * 4);
                _bits[ap] = _bits[ap] | mask;
            }
        }

        private GetMaskForBase(int i)
        {
            for (var i = 0; i < _basesStored; i++)
            {
                var ap = i / 8;
                var ip = i % 8;
                var tmp = _bits[ap] >> (ip * 4);
                if ((tmp & GMask) > 0)
                {
                }
                else if ((tmp & AMask) > 0)
                {
                }
                else if ((tmp & TMask) > 0)
                {
                }
                else if ((tmp & CMask) > 0)
                {
                }
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < _basesStored; i++)
            {
                var ap = i / 8;
                var ip = i % 8;
                var tmp = _bits[ap] >> (ip * 4);
                if ((tmp & GMask) > 0)
                {
                    sb.Append('G');
                }
                else if ((tmp & AMask) > 0)
                {
                    sb.Append('A');
                }
                else if ((tmp & TMask) > 0)
                {
                    sb.Append('T');
                }
                else if ((tmp & CMask) > 0)
                {
                    sb.Append('C');
                }
            }

            return sb.ToString();
        }

        public IEnumerable<int> GetPositions(Sequence other)
        {
            int match = -1;

            do
            {
                match = _bases.IndexOf(other._bases, match + 1);
                if (match > -1)
                {
                    yield return match;
                }
            } while (match != -1);

        }
    }
}
