using EvolveDb;
using MySqlConnector;
using Serilog;

namespace RestWithAspNetAPI.Data
{
    public static class MigrateDatabase 
    {
        public static void MigrateDb(string connection)
        {
            try
            {
                var evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "Data/db/migrations", "Data/db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
