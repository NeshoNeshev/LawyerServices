using LawyerServices.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace LawyerServices.Services.Data
{
    public class TimeService : ITimeService
    {

        private readonly IConfiguration configuration;

        public TimeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public TimeSpan GetTimeOffset(DateTime date)
        {
            var sqlDate = GetServerUtcNow();
            TimeSpan ts = date - sqlDate;

            return ts;

        }
        private DateTime GetServerUtcNow()
        {
            
            using (var connection = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT getdate()";
                return (DateTime)command.ExecuteScalar();
            }
        }
    }
}
