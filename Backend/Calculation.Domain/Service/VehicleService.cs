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

            var basicBuyerFee = await CalculateBasicBuyerFee(basePrice, type, token);
            var sellersSpecialFee = await  CalculateSellersSpecialFee(basePrice, type, token);
            var associationCost = await CalculateAssociationFee(basePrice, token);
            token.ThrowIfCancellationRequested();

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

        protected async Task<decimal> CalculateBasicBuyerFee(decimal price, Common.VehicleType type, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
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
            }, cancellationToken);
        }


        protected async Task<decimal> CalculateSellersSpecialFee(decimal price, Common.VehicleType type, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return type == Common.VehicleType.Luxury ? price * Common.LuxuryFee : price * Common.CommonFee;
            }, cancellationToken);
        }

        protected async Task<decimal> CalculateAssociationFee(decimal price, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

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
            }, cancellationToken);
        }



    }
}
