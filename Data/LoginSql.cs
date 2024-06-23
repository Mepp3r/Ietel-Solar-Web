using Microsoft.Data.SqlClient;

public class LoginSql: Database, ILoginData, IDisposable
{
    public bool Verificacao(Login login)
    {
        string query = "SELECT COUNT(*) FROM Administradores WHERE email = @UserName AND senha = @Password";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            // Adicione parâmetros à consulta para evitar injeção de SQL.
            command.Parameters.AddWithValue("@UserName", login.Usuario);
            command.Parameters.AddWithValue("@Password", login.Senha);

            int count = (int)command.ExecuteScalar();

            // Se count for maior que zero, significa que o usuário foi autenticado com sucesso.
            return count > 0;
        }
    }
}