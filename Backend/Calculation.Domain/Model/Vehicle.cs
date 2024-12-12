﻿using static Calculation.CommonData.Common;

namespace Calculation.Domain.Model
{
    public class Vehicle
    {
        public decimal BasePrice { get; set; }
        public VehicleType Type { get; set; }
        public decimal BasicBuyerFee { get; set; }
        public decimal SellersSpecialFee { get; set; }
        public decimal AssociationCost { get; set; }
        public decimal StorageFee { get; set; }
        public decimal TotalCost { get; set; }
    }
}
