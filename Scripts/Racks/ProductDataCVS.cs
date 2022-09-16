using System.Collections.Generic;

public static class ProductDataCVS
{
        private static string path = "Assets/Resources/productData.csv";
        public static List<ProductData> product_data;

        public static void DeserializeProductData()
        {
            product_data = new List<ProductData>();
            //read CSV and parse into product data
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                product_data.Add(new ProductData(int.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]), split[4], split[5], bool.Parse(split[6]), bool.Parse(split[7])));
            }

        }

    //static constructor
    static ProductDataCVS()
    {
        DeserializeProductData();
    }

}