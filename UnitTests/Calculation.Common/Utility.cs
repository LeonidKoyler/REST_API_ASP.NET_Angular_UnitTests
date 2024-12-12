using Calculation.Domain.Model;
using Calculation_Tool.Models;

namespace Calculation.CommonTests
{
    [TestClass]
    public static class Utility
    {
        private static void AssertVehicleProperties(dynamic responce, decimal basePrice,
                                            decimal basicBuyerFee, decimal sellersSpecialFee,
                                            decimal associationCost, decimal totalCost)
        {
            Assert.IsNotNull(responce);
            Assert.AreEqual(basePrice, responce.BasePrice);
            Assert.AreEqual(basicBuyerFee, responce.BasicBuyerFee);
            Assert.AreEqual(sellersSpecialFee, responce.SellersSpecialFee);
            Assert.AreEqual(associationCost, responce.AssociationCost);
            Assert.AreEqual(totalCost, responce.TotalCost);
        }

        public static void AssertData(Vehicle responce, decimal basePrice,
                                      decimal basicBuyerFee, decimal sellersSpecialFee,
                                      decimal associationCost, decimal totalCost)
        {
            AssertVehicleProperties(responce, basePrice, basicBuyerFee, sellersSpecialFee, associationCost, totalCost);
        }

        public static void AssertFrontEnd(VehicleModel responce, decimal basePrice,
                                           decimal basicBuyerFee, decimal sellersSpecialFee,
                                           decimal associationCost, decimal totalCost)
        {
            AssertVehicleProperties(responce, basePrice, basicBuyerFee, sellersSpecialFee, associationCost, totalCost);
        }


    }
}