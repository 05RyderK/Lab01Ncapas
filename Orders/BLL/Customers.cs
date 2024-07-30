using Entities.Models;
using BLL.Exceptions;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Customers
    {
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                //Buscar si el nombre cliente existe 
                Customer customerSearch = await repository.RetreiveAsync<Customer>(c => c.FirstName == customer.FirstName);
                if (customerSearch != null)
                {
                    //No existe, no podemos crearlo 
                    customerResult = await repository.CreateAsync(customer);
                }
                else
                {
                    //Podriamos aqui lanzar una excepcion 
                    //para notificar que el cliente ya existe. 
                    //Podriamos incluso crear una capa de Excepciones 
                    //personalizadas y consumirla desde otras 
                    //capas.
                    CustomerExceptions.TrhowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return customerResult!;
        }
    }

    public async Task<Customer> RetreiveByIDAsync(int id)
    {
            Customer result = null; 

        using (var repository = RepositoryFactory.CreateRepository()) 
        {
            Customer customer = await repository.RetreiveAsync<Customer>(c => c.Id == id);

            //check if customer was found 
            if (customer == null) 
            {
                // Throw a CustomerNotFoundException (assuming you have this class)
                CustomerExceptions.ThrowInvalidCustomerIdException(id);
            }

            return customer!;
        }
           
    }

    public async Task<List<Customer>> RetreiveAllAsync()
    {
        List<Customer> Result = null;

        using (var r = RepositoryFactory.CreateRepository()) 
        {
            Expression<Func<Customer, bool>> allCustomersCriteria = x => true;
            Result = await r.FilterAsync<Customer>(allCustomersCriteria);
        }
        return Result;
    }

    public async Task<bool> UpdateAsync(Customer customer) 
    {
        bool Result = false;
        using (var repository = RepositoryFactory.CreateRepository()) 
        {
            //Validar que el nombre del cliente no exista 
            Customer customerSearch =
            await repository.RetreiveAsync<Customer>
            (customer => c.FirstName == customer.FirstName
            && c.Id != customer.Id);
            if (customerSearch == null) 
            {
                // No existe 
                Result == await repository.UpdateAsync(customer);
            }
            else 
            {
                // Podemos implementar alguna logica para 
                // Indicar que no se pudo modificar 
                CustomerExceptions.TrhowCustomerAlreadyExistsException(customerSearch.FirstName, customerSearch.LastName);
            } 
        }
        return Result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        bool Result = false;
        // Buscar un cliente para ver si tiene Orders (Ordenes de Compra)
        var customer = await RetreiveByIDAsync(id);
        if (customer != null)
        {
            //Eliminar el cliente 
            using (var repository = RepositoryFactory.CreateRepository())
            {
                Result = await repository.DeleteAsync(customer);
            }
        }
        else
        {
            CustomerExceptions.ThrowInvalidCustomerIdException(id);
        }
        return Result;
    }
}
