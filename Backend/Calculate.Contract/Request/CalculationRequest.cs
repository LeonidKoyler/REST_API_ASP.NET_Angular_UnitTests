using static Calculation.CommonData.Common;

namespace Calculation.Contract.Request
{
    public class CalculationRequest
    {
        public required decimal BasePrice { get; set; }
        public required string Type { get; set; }
    }
}
