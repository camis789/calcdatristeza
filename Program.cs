using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SisProdutos.models;
using SisProdutos.Service;

namespace SisProdutos
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

            UserService userService = new UserService();

            ProductService productService = new ProductService();

            CategoryService categoryService = new CategoryService();
            string res;
            int menu;
            Console.WriteLine("1 - Adicionar Usuário");
            Console.WriteLine("2 - Listar Usuários");
            Console.WriteLine("3 - Fazer Login");
            menu = Convert.ToInt32(Console.ReadLine());
            menuu:
            switch (menu)
            {

                case 1:
                    //Adicionar usuario

                    Console.WriteLine("Digite nome");
                    user.Name = Console.ReadLine();

                    Console.WriteLine("Digite email");
                    user.Email = Console.ReadLine();

                    Console.WriteLine("Digite Password");
                    user.Password = Console.ReadLine();

                    user.Access = Access.admin;

                    

                    Console.WriteLine("Retorno ok");
                    
                     try
                                {

                                    userService.AddUser(user);

                                }
                                catch (SqlException Ex)
                                {
                                    if (Ex.Number == 2627)
                                    {
                                        Console.WriteLine("Erro ao Cadastrar");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cadastrado com sucesso");
                                    }

                                }


                    break;

                case 2:
                    //Listar usuarios
                    var resultListUser = userService.ListUsers();

                    foreach (var item in resultListUser)
                    {
                        Console.WriteLine("Nome: " + item.Name);
                    }
                    break;

                case 3:
                    //Login
                    //obs dentro de fazer login tem que ter o listar produtos(membro e adm) e cadastrar produtos(adm)
                    Console.Clear();
                    Console.WriteLine("Digite email");
                    user.Email = Console.ReadLine();

                    Console.WriteLine("Digite senha");
                    user.Password = Console.ReadLine();

                    var resultAuth = userService.Auth(user);

                    if (resultAuth)
                    {
                        Console.Clear();
                        Console.WriteLine("Logado");
                        

                    }
                    else
                    {
                        Console.WriteLine("Incorreto");
                       
                    }

                    while (resultAuth == true)
                    {
                        Product product = new Product();
                        Category category = new Category();
                        int opcao;
                    menu1:
                        Console.WriteLine("Bem Vindo");

                        Console.WriteLine(user.Email);

                        Console.WriteLine("1 - Cadastro de Produtos");
                        Console.WriteLine("2 - Cadastrar Categoria");
                        Console.WriteLine("3 - Listar Produtos");
                        opcao = Convert.ToInt32(Console.ReadLine());

                        switch (opcao)
                        {
                            case 1:
                                //ADICIONAR PRODUTO
                                

                                Console.WriteLine("Digite Nome");
                                product.Name = Console.ReadLine();

                                string pattern = @"(?i)[^0-9a-zA-Z]";
                                string replacement = "";
                                Regex rgx = new Regex(pattern);
                                string result = rgx.Replace(product.Name, replacement);

                                
                                Console.WriteLine("String tratada : {0}", result);
                                price:
                                string texto;
                                
                                Console.WriteLine("Digite Preço");
                                texto = Console.ReadLine();
                               var resultString = Regex.Match(texto, @"\d\w,+").Value;


                                if (Regex.IsMatch(texto, @"\d\w,+"))
                                {

                                    product.Price = string convert.(texto);
                                    
                                      product.Price = Convert.ToDecimal(Console.ReadLine());
                                }
                                else
                                {
                                    Console.WriteLine("Somente números e números decimais são suportados");
                                        Console.WriteLine("Exemplo: 12 ou 12,30");
                                    goto price;
                                }

                                Console.WriteLine("Digite Quantidade");
                                product.Stock = Convert.ToInt32(Console.ReadLine());

                               

                                Console.WriteLine("Digite a Categoria");
                                category.Name = Console.ReadLine();

                                category.Name = Regex.Replace(category.Name, "[\\(\\)\\-\\ ]", "");
                                Console.WriteLine(category.Name);



                                product.Category = category;

                                categoryService.AddCategory(category);
                                productService.AddProduct(product);
                               

                                Console.WriteLine("Retorno ok");
                                Console.WriteLine("Deseja voltar s/n");

                                res = Console.ReadLine();

                                if (res == "s")
                                {
                                    Console.Clear();
                                    goto menu1;
                                }


                                break;

                            case 2:

                                
                                Console.WriteLine("Digite Categoria");
                                category.Name = Console.ReadLine();

                                
                                


                                categoryService.AddCategory(category);
                                Console.WriteLine("Retorno ok");
                                Console.ReadLine();

                                Console.WriteLine("Deseja voltar s/n");

                                res = Console.ReadLine();

                                if (res == "s")
                                {
                                    Console.Clear();
                                    goto menu1;
                                }

                                break;

                            case 3:
                                var resultListProduct = productService.ListProducts();

                                foreach (var item in resultListProduct)
                                {
                                    Console.WriteLine("Id: " + item.Id);
                                    Console.WriteLine("Nome: " + item.Name);
                                    Console.WriteLine("Preço: " + item.Price);
                                    Console.WriteLine("Quantidade no Estoque: " + item.Stock);
                                }
                                Console.WriteLine("Deseja voltar s/n");
                               
                                res = Console.ReadLine();
                                
                                if (res == "s")
                                {
                                    Console.Clear();
                                    goto menu1;
                                }
                                
                                break;

                               
                        }
                        break;

                      
                    }
                    while (resultAuth == false)
                    {

                        Console.WriteLine("Deseja voltar s/n");

                        res = Console.ReadLine();

                        if (res == "s")
                        {
                            Console.Clear();
                            goto menuu;
                        }
                        break;
                    }
                    break;
            }







        }
    }
}
