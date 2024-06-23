using Microsoft.Data.SqlClient;

public class VagasSql: Database, IVagasData
{
    public void AdicionaVaga(Vagas vagas)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Vagas Values(1, @cargo,@modelo,@tipo,@local,@descricao)";

        cmd.Parameters.AddWithValue("@cargo",vagas.Cargo);
        cmd.Parameters.AddWithValue("@modelo",vagas.Modelo);
        cmd.Parameters.AddWithValue("@tipo",vagas.Tipo);
        cmd.Parameters.AddWithValue("@local",vagas.Local);
        cmd.Parameters.AddWithValue("@descricao",vagas.Descricao);

        cmd.ExecuteNonQuery();
    }
    
    public void Remover(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_delVagas @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
    
    public void Alterar(Vagas form)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;

        cmd.CommandText = "UPDATE Vagas SET Cargo = @cargo, Modelo = @modelo, Tipo = @tipo, Local = @local, Descricao = @descricao";

        cmd.Parameters.AddWithValue("@cargo", form.Cargo);
        cmd.Parameters.AddWithValue("@modelo", form.Modelo);
        cmd.Parameters.AddWithValue("@tipo", form.Tipo);
        cmd.Parameters.AddWithValue("@local", form.Local);
        cmd.Parameters.AddWithValue("@descricao", form.Descricao);
        
        cmd.ExecuteNonQuery();
    }
    public List<Vagas> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Vagas";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Vagas> lista = new List<Vagas>();

        while(reader.Read())
        {
            Vagas vagas = new Vagas();
            vagas.Id = reader.GetInt32(0);
            vagas.AdminId = reader.GetInt32(1);
            vagas.Cargo =  reader.GetString(2);
            vagas.Modelo = reader.GetString(3);
            vagas.Tipo = reader.GetString(4);
            vagas.Local = reader.GetString(5);
            vagas.Descricao = reader.GetString(6);

            lista.Add(vagas);
        }

        return lista;
    }

     public Vagas ReadUpdate(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT cargo, modelo, tipo, local, descricao FROM Vagas WHERE id = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Vagas vagas = new Vagas();
            vagas.Cargo =  reader.GetString(0);
            vagas.Modelo = reader.GetString(1);
            vagas.Tipo = reader.GetString(2);
            vagas.Local = reader.GetString(3);
            vagas.Descricao = reader.GetString(4);

            return vagas;
        }

        return null;
    }
}
