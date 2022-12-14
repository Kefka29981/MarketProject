using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RackScene
{
    public abstract class ProductHolder
    {
        //reserved globalProducts list (used to store old globalProducts when changing holder)
        public List<Product> reservedProducts;

        //contains logic reflection of the product holder mono
        //methdos
        public abstract void AddProduct(Product product);

        public abstract void RemoveProduct(Product product);


        //check if the product can be placed in the holder
        public abstract bool CanAddProduct(Product product, bool containmentCheck = false);

        //recreate holder
        public abstract void RecreateHolder();

        //recreate without ghosts
        public abstract void RecreateWithoutGhosts();

        public abstract void UpdateReservedProducts();
        /*/contains logic reflection of the product holder mono
        //methdos
        public abstract void AddProduct(Product product);

        public abstract void RemoveProduct(Product product);


        //check if the product can be placed in the holder
        public abstract bool CanAddProduct(Product product, bool containmentCheck = false);

        //recreate holder
        public abstract void RecreateHolder();

        //recreate without ghosts
        public abstract void RecreateWithoutGhosts();

        public abstract void UpdateReservedProducts();*/
    }
}

