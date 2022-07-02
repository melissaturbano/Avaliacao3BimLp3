using Avaliacao3BimLp3.Database;
using Microsoft.Data.Sqlite;
using Dapper;
using Avaliacao3BimLp3.Models;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    //insere produto na tabela
    public Product Save (Product product)  
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        //Create
        connection.Execute("INSERT INTO Products VALUES(@Id, @Name, @Price, @Active)", product);

        connection.Close();  
        return product; 
    }


    //Deleta o produto na tabela
    public void Delete (int id) 
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Products WHERE id = @Id", new {Id = id} );

    }


    // Retorna todos os produto na tabela
    public IEnumerable<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        //Read
        var products = connection.Query<Product>("SELECT * FROM Products");

        return products;
    }

    public bool existById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Products WHERE id = @Id", new {Id = id});

        return result;
    }

    // habilita um produto
    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = true WHERE id = @Id", new {Id = id});

    }


    // desabilita um peoduto
    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Products SET active = false WHERE id = @Id", new {Id = id} );

    }

    // retorna os produtos dentro de um intervalo de preço
    public IEnumerable<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var faixaDePreco = connection.Query<Product>("SELECT * FROM Products WHERE price BETWEEN @initialPrice AND @endPrice", new {initialPrice = initialPrice, endPrice = endPrice});

        return faixaDePreco;

    }



    // Retorna os produtos com preço acima de um preço especificado
    public IEnumerable<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var precoMaior = connection.Query<Product>("SELECT * FROM Products WHERE price > @Price", new {Price = price});

        return precoMaior;

    }

    // Retorna os produtos com preço abaixo de um preço especificado
    public IEnumerable<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var precoMenor = connection.Query<Product>("SELECT * FROM Products WHERE price < @Price", new {Price = price});

        return precoMenor;
    }





    // Retorna a média dos preços dos produtos
    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var precoMedia = connection.ExecuteScalar<double>("SELECT AVG(price) FROM Products");

        return precoMedia;
    }





    

}