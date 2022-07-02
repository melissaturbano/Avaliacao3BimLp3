using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);




//Routing 
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product") 
{
        var productRepository = new ProductRepository(databaseConfig);
        
        if(modelAction == "List")
        {
                Console.WriteLine("Product List");
                if (productRepository.GetAll().Any())
                {
                        foreach (var product in productRepository.GetAll())
                        {
                        Console.WriteLine("{product.Id}, {product.Name}, {product.Price}, {product.Active}");
                        }

                } 
                else 
                {
                        Console.WriteLine($"Nenhum produto cadastrado");
                }
                 
        }

        if(modelAction == "New")
        {
                int id = Convert.ToInt32(args[2]);
                var name = args[3];
                var price = Convert.ToDouble(args[4]);
                var active = Convert.ToBoolean(args[5]);

                if(productRepository.existById(id))
                {
                        Console.WriteLine($"O produto {id} já existe");
                        
                } else {
                        var product = new Product(id, name, price, active);
                        productRepository.Save(product);
                        Console.WriteLine("Produto {product.Name} cadastrado com sucesso");
                }

        }

        if (modelAction == "Delete") 
        {
                int id = Convert.ToInt32(args[2]);

                if(productRepository.existById(id))
                {
                        productRepository.Delete(id);
                        Console.WriteLine($"Produto {id} removido com sucesso");
                        
                } else {
                        Console.WriteLine($"Produto {id} não encontrado");
                }

        
        }

        if (modelAction == "Enable") 
        {
                int id = Convert.ToInt32(args[2]);

                if(productRepository.existById(id))
                {
                        productRepository.Enable(id);
                        Console.WriteLine($"Produto {id} habilitado com sucesso");
                        
                } else {
                        Console.WriteLine($"Produto {id} não encontrado");
                }
        }


        if (modelAction == "Disable") 
        {
                int id = Convert.ToInt32(args[2]);

                if(productRepository.existById(id))
                {
                        productRepository.Disable(id);
                        Console.WriteLine($"Produto {id} desabilitado com sucesso");
                        
                } else {
                        Console.WriteLine($"Produto {id} não encontrado");
                }
        }

        if (modelAction == "PriceBetween" )
        {
            var initialPrice = Convert.ToDouble(args[2]);
            var endPrice = Convert.ToDouble(args[3]);

            if (productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
                {
                        Console.WriteLine("{0}, {1}, {2}, {3}", product.Id, product.Name, product.Price, product.Active);
                }
            }
            else 
            {
                Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço {initialPrice} e {endPrice}");
            }       

        }


        if (modelAction == "PriceHigherThan" )
        {
            var price = Convert.ToDouble(args[2]);

            if (productRepository.GetAllWithPriceHigherThan(price).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
                {
                        Console.WriteLine("{product.Id}, {product.Name}, {product.Price}, {product.Active}");
                }
            }
            else 
            {
                Console.WriteLine($"Nenhum produto encontrado com preço maior que {price}");
            }

        }


        if (modelAction == "PriceLowerThan" )
        {
            var price = Convert.ToDouble(args[2]);

            if (productRepository.GetAllWithPriceLowerThan(price).Any())
            {
                foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
                {
                        Console.WriteLine("{product.Id}, {product.Name}, {product.Price}, {product.Active}");
                }
            }
            else 
            {
                Console.WriteLine($"Nenhum produto encontrado com preço menor que {price}."); 
            }

            
        }


        if (modelAction == "AveragePrice") 
        {
                if (productRepository.GetAll().Any())
                {
                        Console.WriteLine($"A média dos preços é {productRepository.GetAveragePrice()}");
                }
                else 
                {
                        Console.WriteLine($"Nenhum produto cadastrado");
                }
                
                         
        }

}
