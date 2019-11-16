using SisProdutos.Config;
using SisProdutos.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisProdutos.Service
{
    class CategoryService
    {
        DbContextProduct context = new DbContextProduct();

        public Category AddCategory(Category category)
        {
            category.DateCreate = DateTime.Now;

            context.Categorys.Add(category);


            return category;
        }

        public bool CategoryTest(Category category)
        {
            var result = context.Categorys.Where(x => x.Name == category.Name).FirstOrDefault();
            if (result != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
