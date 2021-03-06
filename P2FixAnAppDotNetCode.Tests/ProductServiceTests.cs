﻿using System.Collections.Generic;
using System.Linq;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Repositories;
using P2FixAnAppDotNetCode.Models.Services;
using Xunit;

namespace P2FixAnAppDotNetCode.Tests
{
    /// <summary>
    /// The ProductService test class
    /// </summary>
    public class ProductServiceTests
    {

        [Fact]
        public void Product()
        {
            IProductRepository productRepository = new ProductRepository();
            IProductService productService = new ProductService(productRepository);

            var products = productService.GetAllProducts();

            Assert.IsType<List<Product>>(products);
        }

        [Fact]
        public void UpdateProductQuantities()
        {
            Cart cart = new Cart();
            IProductRepository productRepository = new ProductRepository();
            IProductService productService = new ProductService(productRepository);

            IEnumerable<Product> products = productService.GetAllProducts();
            cart.AddItem(products.Where(p => p.Id == 1).First(), 1);
            cart.AddItem(products.Where(p => p.Id == 3).First(), 2);
            cart.AddItem(products.Where(p => p.Id == 5).First(), 3);

            productService.UpdateProductQuantities(cart);

            Assert.Equal(9, products.Where(p => p.Id == 1).First().Stock);
            Assert.Equal(28, products.Where(p => p.Id == 3).First().Stock);
            Assert.Equal(47, products.Where(p => p.Id == 5).First().Stock);

            //do a second run adding items to cart. 
            //will simulate the process from the front end perspective
            //here testing that product stock values are decreasing for each cart checkout, not just a single time
            cart = new Cart();

            cart.AddItem(products.Where(p => p.Id == 1).First(), 1);
            cart.AddItem(products.Where(p => p.Id == 3).First(), 2);
            cart.AddItem(products.Where(p => p.Id == 5).First(), 3);

            productService.UpdateProductQuantities(cart);

            Assert.Equal(8, products.Where(p => p.Id == 1).First().Stock);
            Assert.Equal(26, products.Where(p => p.Id == 3).First().Stock);
            Assert.Equal(44, products.Where(p => p.Id == 5).First().Stock);
        }

        [Fact]
        public void GetProductById()
        {
            IProductRepository productRepository = new ProductRepository();
            IProductService productService = new ProductService(productRepository);
            int id = 3;

            Product product = productService.GetProductById(id);

            Assert.Same("JVC HAFX8R Headphone", product.Name);
            Assert.Equal(69.99, product.Price);
        }
    }
}
