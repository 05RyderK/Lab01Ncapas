using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class CustomerExceptions : Exception
    {
        //You can add more static methods here to throw other customer-related exceptions
        
        private CustomerExceptions(string message) : base(message) 
        {
           //Optional: Add constructor logico for logging or custom error handling 
        }

        public static void TrhowCustomerAlreadyExistsException(String firstname,  String lastname)
        {
            throw new CustomerExceptions($"A client with the name Already exists{firstname} {lastname}.");
        }

        public static void ThrowInvalidCustomerDataException(string message)
        {
            throw new CustomerExceptions(message);
        }
    }
}
