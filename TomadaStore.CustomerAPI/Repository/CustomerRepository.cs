using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repository.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger,
                                    ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }

        public Task<List<Customer>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = @"INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber)
                                  VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

                await _connection.ExecuteAsync(insertSql, new { customer.FirstName,
                                                                customer.LastName,
                                                                customer.Email,
                                                                customer.PhoneNumber});
            }
            catch (SqlException sqlEx){
                _logger.LogError($"SQL Error inserting customer: {sqlEx.Message}");

                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting customer: {ex.Message}");

                throw new Exception(ex.StackTrace);
            }
        }
    }
}
