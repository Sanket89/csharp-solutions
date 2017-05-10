using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPicker
{
    public class RestuarantPicker
    {
        private class Restaurant
        {
            public int RestaurantId { get; set; }
            public Dictionary<string, decimal> Menu { get; set; }
        }

        private readonly List<Restaurant> _restaurants = new List<Restaurant>();

        /// <summary>
        /// Reads the file specified at the path and populates the restaurants list
        /// </summary>
        /// <param name="filePath">Path to the comma separated restuarant menu data</param>
        public void ReadRestaurantData(string filePath)
        {
            try
            {
                var records = File.ReadLines(filePath);

                foreach(var record in records)
                {
                    var data = record.Split(',');
                    var restaurantId = int.Parse(data[0].Trim());
                    var restaurant = _restaurants.Find(r => r.RestaurantId == restaurantId);

                    if(restaurant == null)
                    {
                        restaurant = new Restaurant {Menu = new Dictionary<string, decimal>()};
                        _restaurants.Add(restaurant);
                    }

                    restaurant.RestaurantId = restaurantId;
                    restaurant.Menu.Add(data.Skip(2).Select(s=>s.Trim()).Aggregate((a,b)=>a.Trim()+","+b.Trim()), decimal.Parse(data[1].Trim()));
                }

            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Takes in items you would like to eat and returns the best restaurant that serves them.
        /// </summary>
        /// <param name="items">Items you would like to eat (seperated by ',')</param>
        /// <returns>Restaurant Id and price tuple</returns>
        public Tuple<int,decimal> PickBestRestaurant(string items)
        {
            // Complete this method
            Tuple<int, decimal> tuple = new Tuple<int, decimal>(0, 0);
            var restaurantPicker = new RestuarantPicker();
            restaurantPicker.ReadRestaurantData(@"../../../restaurant_data.csv");
            foreach ( var i in restaurantPicker._restaurants)
            {
                if (i.Menu.Keys.Contains(items))
                {
                    tuple = new Tuple<int, decimal>(i.RestaurantId, (from e in i.Menu
                    where e.Key == items
                    select e.Value).FirstOrDefault());
                }                   
            }
            return new Tuple<int, decimal>(tuple.Item1, tuple.Item2);
        }

        static void Main(string[] args)
        {
            var restaurantPicker = new RestuarantPicker();

            restaurantPicker.ReadRestaurantData(@"../../../restaurant_data.csv");

            // Item is found in restaurant 2 at price 6.50
            var bestRestaurant = restaurantPicker.PickBestRestaurant("tofu_log");

            Console.WriteLine(bestRestaurant.Item1 + ", " + bestRestaurant.Item2);
            
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
