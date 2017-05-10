using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RestaurantPicker.Tests
{
    [TestClass]
    public class RestuarantPickerTests
    {
        public RestuarantPicker RestaurantPicker;

        [TestInitialize]
        public void InitializeTests()
        {
            RestaurantPicker = new RestuarantPicker();
            RestaurantPicker.ReadRestaurantData(@"../../../restaurant_data.csv");
        }

        [TestMethod]
        public void PickBestRestaurantTest_OneItem()
        {
            var result = RestaurantPicker.PickBestRestaurant("tofu_log");
            Assert.AreEqual(result, new Tuple<int, decimal>(6, (decimal)11.0));
        }

        [TestMethod]
        public void PickBestRestaurantTest_TwoItems()
        {
            var result = RestaurantPicker.PickBestRestaurant("burger,tofu_log");
            Assert.AreEqual(result, new Tuple<int, decimal>(6, (decimal)11.0));
        }

		[TestMethod]
		public void PickBestRestaurantTest_TwoItems_2()
		{
		    var result = RestaurantPicker.PickBestRestaurant("almond_biscuit,joe_frogger");
		    Assert.AreEqual(result, new Tuple<int, decimal>(6, (decimal)11.0));
		}

        [TestMethod]
        public void PickBestRestaurantTest_CombinationItems()
        {
            var result = RestaurantPicker.PickBestRestaurant("fancy_european_water,extreme_fajita");
            Assert.AreEqual(result, new Tuple<int, decimal>(1, (decimal)8.0));
        }

        [TestMethod]
        public void PickBestRestaurantTest_NotFound()
        {
            var result = RestaurantPicker.PickBestRestaurant("chef_salad,wine_spritzer");
            Assert.IsNull(result, "null");
        }
    }
}