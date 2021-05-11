using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookUpModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            
            if (request.UserId == 1)
            {
                output.FirstName = "G";
                output.LastName = "K";
                output.Age = 21;
            }
            else if (request.UserId == 3)
            {
                output.FirstName = "N";
                output.LastName = "D";
                output.Age = 30;
            }
            else
            {
                output.FirstName = "O";
                output.LastName = "T";
                output.Age = 99;
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>()
            {
                new CustomerModel 
                { 
                    FirstName = "a",
                    LastName = "b",
                    EmailAddress = "c",
                    IsAlive = true,
                    Age = 20
                },
                new CustomerModel
                {
                    FirstName = "d",
                    LastName = "e",
                    EmailAddress = "f",
                    IsAlive = true,
                    Age = 30
                },
                new CustomerModel
                {
                    FirstName = "g",
                    LastName = "h",
                    EmailAddress = "i",
                    IsAlive = true,
                    Age = 40
                }
            };

            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
