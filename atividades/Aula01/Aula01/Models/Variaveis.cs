namespace Aula1.Models
{
    public class Variaveis
    {
        // Existing properties
        public int userCount = 10;
        public int totalCount;
        public const int interestRate = 10;

        // Numeric type ranges
        public byte MinValue = 0;
        public byte MaxValue = 255;
        public short MiniValue = -32768;
        public short MaxiValue = 32767;
        public int MinimValue = -2147483648;
        public int MaximValue = 2147483647;
        public uint MinimoValue = 0;
        public uint MaximoValue = 4294967295;
        public long MiniiValue = -9223372036854775808;
        public long MaxiiValue = 9223372036854775807;

        // Newly added numeric types
        public sbyte MinSByte = sbyte.MinValue;
        public sbyte MaxSByte = sbyte.MaxValue;
        public ushort MinUShort = ushort.MinValue;
        public ushort MaxUShort = ushort.MaxValue;
        public ulong MinULong = ulong.MinValue;
        public ulong MaxULong = ulong.MaxValue;
        public float MinFloat = float.MinValue;
        public float MaxFloat = float.MaxValue;
        public double MinDouble = double.MinValue;
        public double MaxDouble = double.MaxValue;
        public decimal MinDecimal = decimal.MinValue;
        public decimal MaxDecimal = decimal.MaxValue;

        public Variaveis()
        {
            totalCount = 0;
            var signalStrength = 20;
            var companyName = "ACME";
        }
    }
}