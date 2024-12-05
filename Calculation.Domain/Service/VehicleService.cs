using Calculation.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.Domain.Service
{
    public class VehicleService : IVehicleService
    {
        private const short StorageFee = 100;

        public async Task<Vehicle> CalculateFees(decimal basePrice, Common.Common.VehicleType type, CancellationToken token = default)
        {
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

        private decimal CalculateBasicBuyerFee(decimal price, Common.Common.VehicleType type, CancellationToken token = default)
        {
            decimal fee = price * 0.10m;

            if (type == Common.Common.VehicleType.Common)
            {
                fee = Math.Clamp(fee, 10, 50);
            }
            else if (type == Common.Common.VehicleType.Luxury)
            {
                fee = Math.Clamp(fee, 25, 200);
            }

            return fee;
        }

        private decimal CalculateSellersSpecialFee(decimal price, Common.Common.VehicleType type)
        {
            return type == Common.Common.VehicleType.Luxury ? price * 0.04m : price * 0.02m;
        }

        private decimal CalculateAssociationFee(decimal price)
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
