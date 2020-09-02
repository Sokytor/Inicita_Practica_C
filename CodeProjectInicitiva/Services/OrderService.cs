using CodeRefactoring.Models;
using System;

namespace CodeRefactoring.Services
{
        public class OrderService
    {
        private List<Offer> GetOffers()
        {
            return new List<Offer>
            {
                new Offer {
                    DiscountId = 1,
                    Discount = 0.2m,
                    ProductId = 1
                },
                new Offer {
                    DiscountId = 2,
                    Discount = 0.1m,
                    ProductId = 2
                }
            };
        }
        private int GetNextOrderNumber()
        {
            var random = new Random();
            return random.Next(1, 1000);
        }

        public Order Create(Order order)
        {
           var subTotal = 0m;
           var iva = 0m;
           var total = 0m;
           var discount = 0m;
           
           var ivaRate = 0.18m;
           var offers = GetOffers();

           var nextNumber = GetNextOrderNumber();
           order.OrderId = string.Format($"{DateTime.UtcNow.Year}-{nextNumber.ToString().PadLeft(4, '0')}");


           foreach ( item in collection)
           {
                item.Total = item.Quantity * item.UnitPrice;
                item.Iva = item.Total * ivaRate;
                item.SubTotal = item.Total - item.Iva;

                var offer = offers.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (offer != null)
                {
                    item.UnitPrice = (1 - offer.Discount) * item.UnitPrice;

                    var originalTotal = item.Total;

                    item.Total = Math.Round(item.Quantity * item.UnitPrice, 2);
                    item.SubTotal = Math.Round(item.Total / (1 + ivaRate), 2);
                    item.Iva = Math.Round(item.Total - item.SubTotal, 2);
                    item.Discount = originalTotal - item.Total;
                }
                total += item.Total;
                iva += item.Iva;
                subTotal += item.SubTotal;
                discount += item.Discount;
           }

            order.Discount = discount;
            order.SubTotal = subTotal;
            order.Iva = iva;
            order.Total = total;


            order.CreatedAd = DateTime.UtcNow;

            return order;        

        }

    }
}
