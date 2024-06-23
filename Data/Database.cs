using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection connection;

    public Database()
    {
        connection = new SqlConnection("Data Source=localhost; Initial Catalog=IETELSolar; Integrated Security=True; TrustServerCertificate=true;");
        connection.Open();
    }

    public void Dispose()
    {
        connection.Close();
    }
}
