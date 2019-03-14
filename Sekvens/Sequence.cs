using System;
using System.Collections.Generic;
using System.Text;

namespace Sekvens
{
    public class Sequence
    {
        private int[] _bits;
        private int _basesStored;
        private readonly string _bases = "";

        private static readonly char[] BaseLetterRepresentation;
        static Sequence()
        {
            BaseLetterRepresentation = new char[16];
            BaseLetterRepresentation[A] = 'A';
            BaseLetterRepresentation[G] = 'G';
            BaseLetterRepresentation[C] = 'C';
            BaseLetterRepresentation[T] = 'T';
            BaseLetterRepresentation[N] = 'N';
            BaseLetterRepresentation[W] = 'W';
            BaseLetterRepresentation[B] = 'B';
            BaseLetterRepresentation[D] = 'D';
            BaseLetterRepresentation[H] = 'H';
            BaseLetterRepresentation[K] = 'K';
            BaseLetterRepresentation[M] = 'M';
            BaseLetterRepresentation[R] = 'R';
            BaseLetterRepresentation[S] = 'S';
            BaseLetterRepresentation[Y] = 'Y';
            BaseLetterRepresentation[V] = 'V';
        }


        private static readonly int T = 0b1000;
        private static readonly int A = 0b0100;
        private static readonly int C = 0b0010;
        private static readonly int G = 0b0001;
        private static readonly int W = A | T;
        private static readonly int N = A | C | G | T;
        private static readonly int B = C | G | T;
        private static readonly int D = A | G | T;
        private static readonly int H = A | C | T;
        private static readonly int K = G | T;
        private static readonly int M = A | C;
        private static readonly int R = A | G;
        private static readonly int S = C | G;
        private static readonly int Y = C | T;
        private static readonly int V = A | C | G;


        public Sequence(string bases)
        {
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
                var mask = C;
                switch (bases[i])
                {
                    case 'C': mask = C; break;
                    case 'A': mask = A; break;
                    case 'T': mask = T; break;
                    case 'G': mask = G; break;
                    case 'W': mask = W; break;
                    case 'N': mask = N; break;
                    case 'H': mask = H; break;
                    case 'K': mask = K; break;
                    case 'M': mask = M; break;
                    case 'B': mask = B; break;
                    case 'D': mask = D; break;
                    case 'R': mask = R; break;
                    case 'S': mask = S; break;
                    case 'V': mask = V; break;
                    case 'Y': mask = Y; break;
                    default:
                        throw new ArgumentException("God forgot " + bases[i]);
                }

                mask = mask << (ip * 4);
                _bits[ap] = _bits[ap] | mask;
            }
        }

        private int GetMaskForBase(int i)
        {
            var ap = i / 8;
            var ip = i % 8;
            return (_bits[ap] >> (ip * 4)) & N;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < _basesStored; i++)
            {
                var mask = GetMaskForBase(i);
                sb.Append(BaseLetterRepresentation[mask]);
            }

            return sb.ToString();
        }

        public IEnumerable<int> GetPositions(Sequence other)
        {
            if (other._basesStored > this._basesStored)
            {
                yield break;
            }

            var max = _basesStored - other._basesStored + 1;
            for (var i = 0; i < max; i++)
            {
                bool match = true;

                for (var j = 0; match && j < other._basesStored; j++)
                {
                    if ((GetMaskForBase(i + j) & other.GetMaskForBase(j)) == 0)
                    {
                        match = false;
                    }
                }

                if (match)
                {
                    yield return i;
                }
            }
        }
    }
}
