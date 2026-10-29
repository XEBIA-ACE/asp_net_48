using MicroProjectApplication.Models;
using MicroProjectApplication.Repositories.Interfaces;
using MicroProjectApplication.Services.Interfaces;
using System.Collections.Generic;

namespace MicroProjectApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public Customer Create(Customer customer)
        {
            if (customer == null)
                return null;

            return _customerRepository.Add(customer);
        }

        public Customer Update(int id, Customer customer)
        {
            if (customer == null)
                return null;

            return _customerRepository.Update(id, customer);
        }

        // Partial update: only non-null Name/City values are applied.
        public Customer Patch(int id, Customer customer)
        {
            if (customer == null)
                return null;

            var existing = _customerRepository.GetById(id);
            if (existing == null)
                return null;

            return _customerRepository.Update(id, new Customer
            {
                Name = customer.Name ?? existing.Name,
                City = customer.City ?? existing.City
            });
        }

        public bool Delete(int id)
        {
            return _customerRepository.Delete(id);
        }
    }
}