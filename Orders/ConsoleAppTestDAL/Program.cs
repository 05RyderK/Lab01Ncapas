// See https://aka.ms/new-console-template for more information
using DAL;
using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

//CreateAsync().GetAwaiter().GetResult();
//RetreiveAsync().GetAwaiter().GetResult();
//UpdateAsync().GetAwaiter().GetResult();
FilterAsync().GetAwaiter().GetResult();

Console.ReadKey();

//Crear un objeto 

static async Task CreateAsync()
{
    //Add Customer 
    Customer cstomer = new Customer()
    {
        FirstName = "Vladimir",
        LastName = "Cortes",
        City = "Bogota",
        Country = "Colombia",
        Phone = "3144427602"
    };

    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(cstomer);
            Console.WriteLine($"Added Customer: {createdCustomer.LastName} {createdCustomer.FirstName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }
}

static async Task RetreiveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository()) 
    {
        try
        {
            Expression<Func<Customer, bool>> criteria = c => c.FirstName == "Vladimir" && c.LastName == "Cortes";
            var customer = await repository.RetreiveAsync(criteria);
            if (customer != null)
            {

                Console.WriteLine($"Retrived cstomer: {customer.FirstName} \t{customer.LastName} \t Country: {customer.Country} \t City: {customer.City}");
            }
            else 
            {
                Console.WriteLine("Customer not exist");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }
}

static async Task UpdateAsync()
{
    //Supuesto: Existe el objeto a modificar 

    using (var repository = RepositoryFactory.CreateRepository())
    {
        var customerToUpdate = await repository.RetreiveAsync<Customer>(c => c.Id == 78);

        if (customerToUpdate != null) 
        {
            customerToUpdate.FirstName = "Liu";
            customerToUpdate.LastName = "Wong";
            customerToUpdate.City = "Toronto";
            customerToUpdate.Country = "Canada";
            customerToUpdate.Phone = "+14337 6353039";
        }
        try 
        {
            bool update = await repository.UpdateAsync(customerToUpdate);
            if (update)
            {
                Console.WriteLine("Customer updated successfully.");
            }else
            {
                Console.WriteLine("Custome update failed.");
            }    
            
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
        
}


static async Task FilterAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        Expression<Func<Customer, bool>> criteria = c => c.Country == "USA";

        var customers = await repository.FilterAsync(criteria);

        foreach (var customer in customers) 
        {
            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}\t from {customer.City}");
        }
    }
}