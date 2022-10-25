using System.Collections.Generic;
using System.IO;

namespace RackScene
{
    public static class ProductDataCVS
    {
        private static readonly string path = "Assets/Resources/productData.csv";
        public static List<ProductData> product_data;

        public static void DeserializeProductData()
        {
            product_data = new List<ProductData>();
            //read CSV and parse into product data
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var split = line.Split(',');
                //parse all variables
                int id = int.Parse(split[0]);
                float base_width = float.Parse(split[1]);
                float base_height = float.Parse(split[2]);
                float base_depth = float.Parse(split[3]);
                int pinX = int.Parse(split[4]);
                int pinY = int.Parse(split[5]);
                string name = split[6];
                string tag = split[7];
                bool canBePlacedOnTop = bool.Parse(split[9]);
                bool canBePinned = bool.Parse(split[8]);

                product_data.Add(new ProductData(id, base_width, base_height, base_depth, canBePinned, name, tag, canBePlacedOnTop, pinX, pinY));

            }
        }

        //static constructor
        static ProductDataCVS()
        {
            DeserializeProductData();
        }
    }
}

