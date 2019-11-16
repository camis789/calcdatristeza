using System;
using SisProdutos.models;
using SisProdutos.Config;
using System.Linq;
using System.Collections.Generic;

namespace SisProdutos.Service
{
    public class ProductService
    {

        DbContextProduct context = new DbContextProduct();

        public Product AddProduct(Product product)
        {
            product.DateCreate = DateTime.Now;

            context.Products.Add(product);
            context.SaveChanges();

            return product;
        }

        public List<Product> ListProducts()
        {
            var result = context.Products.ToList();

            return result;
        }

        public void verifica(string texto)
        {
            if (this.contemLetras(texto) && this.contemNumeros(texto))
            {
                //Contem Letras e Números
            }
            else if (this.contemLetras(texto))
            {
                //Contem somente letras
            }
            else if (this.contemNumeros(texto))
            {
                //Contem somente numeros
            }
        }

        public bool contemLetras(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

        public bool contemNumeros(string texto)
        {
            if (texto.Where(c => char.IsNumber(c)).Count() > 0)
                return true;
            else
                return false;
        }

    }
}
