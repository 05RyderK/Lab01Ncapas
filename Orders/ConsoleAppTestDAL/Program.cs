// See https://aka.ms/new-console-template for more information
using DAL;
using Entities.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();
RetreiveAsync().GetAwaiter().GetResult();

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