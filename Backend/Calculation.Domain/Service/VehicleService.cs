using Calculation.CommonData;
using Calculation.Domain.Model;
 
namespace Calculation.Domain.Service
{
    public class VehicleService : IVehicleService
    {
        private const short StorageFee = 100;

        public async Task<Vehicle> CalculateFees(decimal basePrice, CommonData.Common.VehicleType type, CancellationToken token = default)
        {
            if (basePrice <= 0)
            {
                return new Vehicle { StorageFee = 0 };
            }

            var basicBuyerFeeTask = Task.Run(() => CalculateBasicBuyerFee(basePrice, type), token);
            var sellersSpecialFeeTask = Task.Run(() => CalculateSellersSpecialFee(basePrice, type), token);
            var associationCostTask = Task.Run(() => CalculateAssociationFee(basePrice), token);
            await Task.WhenAll(basicBuyerFeeTask, sellersSpecialFeeTask, associationCostTask);

            var basicBuyerFee = basicBuyerFeeTask.Result;
            var sellersSpecialFee = sellersSpecialFeeTask.Result;
            var associationCost = associationCostTask.Result;

            var totalCost = basePrice + basicBuyerFee + sellersSpecialFee + associationCost + StorageFee;

            Vehicle vehicle = new()
            {
                BasePrice = basePrice,
                BasicBuyerFee = basicBuyerFee,
                AssociationCost = associationCost,
                TotalCost = totalCost,
                SellersSpecialFee = sellersSpecialFee,
                Type = type,
                StorageFee = StorageFee
            };

            return vehicle;
        }

        protected virtual decimal CalculateBasicBuyerFee(decimal price, Common.VehicleType type, CancellationToken token = default)
        {
            decimal fee = price * Common.BasicBuyerFee;

            if (type == Common.VehicleType.Common)
            {
                fee = Math.Clamp(fee, Common.BasicMinimum, Common.BasicMaximum);
            }
            else if (type == Common.VehicleType.Luxury)
            {
                fee = Math.Clamp(fee, Common.FeeMinimum, Common.FeeMaximum);
            }

            return fee;
        }

        protected decimal CalculateSellersSpecialFee(decimal price, Common.VehicleType type)
        {
            return type == Common.VehicleType.Luxury ? price * Common.LuxuryFee : price * Common.CommonFee;
        }

        protected decimal CalculateAssociationFee(decimal price)
        {
            if (price < 0)
            {
                return 0;
            }
            else if (price is >= 0 and <= 500)
            {
                return 5;
            }
            else if (price <= 1000)
            {
                return 10;
            }
            else if (price <= 3000)
            {
                return 15;
            }
            else
            {
                return 20;
            }
        }


    }
}
