using System.Runtime.Serialization;

namespace Calculation.CommonData
{
    public class Common
    {
        public enum VehicleType
        {
            Common = 1,
            Luxury = 2
        }

        public static readonly decimal BasicMinimum = 10;
        public static readonly decimal BasicMaximum = 50;
        public static readonly decimal FeeMinimum = 25;
        public static readonly decimal FeeMaximum = 200;

        public static readonly decimal BasicBuyerFee  = 0.1m;
        public static readonly decimal CommonFee = 0.02m;
        public static readonly decimal LuxuryFee = 0.04m;
    }
}
