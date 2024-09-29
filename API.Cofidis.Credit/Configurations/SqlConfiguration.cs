using Cofidis.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Cofidis.Credit.Configurations
{
    static class SqlConfiguration
    {
        public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<DbContext>(options =>
            {
                options.UseSqlServer(database);

            });

        }
    }
}
