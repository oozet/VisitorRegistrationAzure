using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VR.Models;

namespace VR.Services;

public interface IDBService
{
    Task RegisterVisitor(RegisterVisitorDTO visitorDTO);
    Task<IEnumerable<VisitorDTO>> GetAllVisitors();
}

public class DBService : IDBService
{
    private readonly ILogger _logger;
    private readonly string _connectionString;

    public DBService(ILogger<DBService> logger)
    {
        _logger = logger;
        _connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SqlConnectionString") ?? throw new InvalidOperationException("ConnectionString is empty.");
    }

    public async Task<IEnumerable<VisitorDTO>> GetAllVisitors()
    {
        try
        {
            var visitors = new List<VisitorDTO>();
            await using SqlConnection connection = new SqlConnection(_connectionString);
            await using SqlCommand command = new SqlCommand("SELECT * FROM Visitors", connection);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                visitors.Add(new VisitorDTO
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                });
            }
            return visitors;
        }

        catch (Exception ex)
        {
            // Potential security issue - Only for development. Remove connectionstring logging."
            _logger.LogError(ex, "Problems connection to database with connectionstring: {connectionstring}", _connectionString);
            throw;
        }
    }

    public async Task RegisterVisitor(RegisterVisitorDTO visitorDTO)
    {
        try
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);
            await using SqlCommand command = new SqlCommand("INSERT INTO Visitors (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)", connection);
            command.Parameters.AddWithValue("FirstName", visitorDTO.FirstName);
            command.Parameters.AddWithValue("LastName", visitorDTO.LastName);
            command.Parameters.AddWithValue("Email", visitorDTO.Email);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            // Potential security issue - Only for development. Remove connectionstring logging."
            _logger.LogError(ex, "Problems connection to database with connectionstring: {connectionstring}", _connectionString);
            throw;
        }
    }
}
