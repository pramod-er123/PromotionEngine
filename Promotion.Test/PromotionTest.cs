using NUnit.Framework;
using Promotion.Core.Domain;
using System.Collections.Generic;
using Promotion.Core.Interface;
using Promotion.Business.PromotionStrategy;
using Promotion.Business;
using System;

namespace Promotion.Test
{
    public class PromotionTest
    {
        List<OrderItem> items;
        List<IPromotionStrategy> promotions;
        [SetUp]
        public void Setup()
        {

            promotions = new List<IPromotionStrategy>()
                        {
                            new CandDSkuPromotion(){IsActive = true, OfferedPrice = 30 },
                            new ThreeOfASkuPromotion(){IsActive = true, OfferedPrice = 130 },
                            new TwoOfBSkuPromotion(){IsActive = true, OfferedPrice = 45 }
                        };
        }

        [Test]
        public void ScenarioA()
        {
            //Arrange
            items = new List<OrderItem>()
            {
                new OrderItem(){ product = new Product() {Name = "Product1", SKU = "A" },   Quantity = 1, Price = 50},
                new OrderItem(){product = new Product() {Name = "Product2", SKU = "B" },  Quantity = 1, Price = 30},
                new OrderItem(){product = new Product() {Name = "Product3", SKU = "C" },  Quantity = 1, Price = 20},
                //new OrderItem(){product = new Product() {Name = "Product4", SKU = "D" },  Quantity = 2, Price = 15}
            };
            Order order = new Order() { UserId = 123, Items = items, OrderNo = "O0000001", OrderDate = DateTime.Now };
            OrderManager manager = new OrderManager(order, promotions, true);
            order = manager.CalculateTotal();

            Assert.AreEqual(0, order.DiscountedAmount);
            Assert.AreEqual(100, order.NetAmount);
        }

        [Test]
        public void ScenarioB()
        {
            //Arrange
            items = new List<OrderItem>()
            {
                new OrderItem(){ product = new Product() {Name = "Product1", SKU = "A" },   Quantity = 5, Price = 50},
                new OrderItem(){product = new Product() {Name = "Product2", SKU = "B" },  Quantity = 5, Price = 30},
                new OrderItem(){product = new Product() {Name = "Product3", SKU = "C" },  Quantity = 1, Price = 20},
            };
            Order order = new Order() { UserId = 123, Items = items, OrderNo = "O0000001", OrderDate = DateTime.Now };
            OrderManager manager = new OrderManager(order, promotions, true);
            order = manager.CalculateTotal();

            //Assert
            Assert.AreEqual(370, order.DiscountedAmount);
            Assert.AreEqual(420, order.NetAmount);
        }

        [Test]
        public void ScenarioC()
        {
            //Arrange
            items = new List<OrderItem>()
            {
                new OrderItem(){ product = new Product() {Name = "Product1", SKU = "A" },   Quantity = 3, Price = 50},
                new OrderItem(){product = new Product() {Name = "Product2", SKU = "B" },  Quantity = 5, Price = 30},
                new OrderItem(){product = new Product() {Name = "Product3", SKU = "C" },  Quantity = 1, Price = 20},
                new OrderItem(){product = new Product() {Name = "Product4", SKU = "D" },  Quantity = 1, Price = 15}
            };
            Order order = new Order() { UserId = 123, Items = items, OrderNo = "O0000001", OrderDate = DateTime.Now };
            OrderManager manager = new OrderManager(order, promotions, true);
            order = manager.CalculateTotal();

            //Assert
            Assert.AreEqual(370, order.DiscountedAmount);
            Assert.AreEqual(420, order.NetAmount);
        }
    }
}