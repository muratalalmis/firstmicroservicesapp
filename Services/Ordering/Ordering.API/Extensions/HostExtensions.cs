using System;
using System.Runtime.ExceptionServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0)
            where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var context = services.GetService<TContext>();

                var connectionString = configuration.GetConnectionString("OrderingConnectionString");
                try
                {
                    var masterConnectionString = connectionString.Replace("Database=OrderDb", "Database=master");
                    RunSqlCommand(masterConnectionString, "Create Database OrderDb");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unhandled error occurred.");
                }

                try
                {
                    RunSqlCommand(connectionString, @"

CREATE TABLE Orders(
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](75) NOT NULL,
	[TotalPrice] [decimal] NOT NULL,

    [FirstName] [nvarchar](75) NULL,
    [LastName] [nvarchar](75) NULL,
    [EmailAddress] [nvarchar](75) NULL,
    [AddressLine] [nvarchar](75) NULL,
    [Country] [nvarchar](75) NULL,
    [State] [nvarchar](75) NULL,
    [ZipCode] [nvarchar](75) NULL,
    [CardName] [nvarchar](75) NULL,
    [CardNumber] [nvarchar](75) NULL,
    [Expiration] [nvarchar](75) NULL,
    [CVV] [nvarchar](75) NULL,
	[PaymentMethod] [int] NULL,

	[CreatedBy] [nvarchar](75) NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[LastModifiedBy] [nvarchar](75) NULL,
	[LastModifiedDate] [DateTime] NULL

 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
        ([Id] ASC )
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An unhandled error occurred.");
                }

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occured while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            // TODO : Code first migrations not supported on azure sql edge
            //context.Database.Migrate();
            seeder(context, services);
        }

        private static void RunSqlCommand(string connectionString, string queryString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                var result = command.ExecuteNonQuery();
            }
        }
    }
}

