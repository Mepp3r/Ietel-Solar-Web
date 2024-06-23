using Microsoft.Data.SqlClient;

public class CandidatoSql : Database, ICandidatoData
{
    public void TrabalheConosco(int id, int adminId, Candidato candidato)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"
        exec sp_cadCandidatos @nome, @cpf, @telefone, @cidade, @email, @linkedin, @curriculo, @mensagem, @vagaid, @adminid, @status";

        cmd.Parameters.AddWithValue("@nome",     candidato.Nome);
        cmd.Parameters.AddWithValue("@cpf",      candidato.CPF);
        cmd.Parameters.AddWithValue("@telefone", candidato.Telefone);
        cmd.Parameters.AddWithValue("@cidade",   candidato.Cidade);
        cmd.Parameters.AddWithValue("@email",    candidato.Email);
        cmd.Parameters.AddWithValue("@linkedin", candidato.Linkedin);
        cmd.Parameters.AddWithValue("@curriculo",candidato.FileName);
        cmd.Parameters.AddWithValue("@mensagem", candidato.Mensagem);
        cmd.Parameters.AddWithValue("@vagaid",   id);
        cmd.Parameters.AddWithValue("@adminid",  adminId);
        cmd.Parameters.AddWithValue("@status",   0);

        cmd.ExecuteNonQuery();
    }

    public List<Candidato> Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM View_Candidato WHERE VagaId = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Candidato> lista = new List<Candidato>();

        while(reader.Read())
        {
            Candidato candidato = new Candidato();
            candidato.Nome = reader.GetString(0);
            candidato.CPF = reader.GetString(1);
            candidato.Telefone = reader.GetString(2);
            candidato.Cidade = reader.GetString(3);
            candidato.Email = reader.GetString(4);
            candidato.Linkedin = reader.GetString(5);
            candidato.FileName = reader.GetString(6);
            candidato.Mensagem = reader.GetString(7);

            lista.Add(candidato);
        }

        return lista;
    }
    
    public Vagas ReadVaga(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT id, adminId from Vagas WHERE id = @id";

        cmd.Parameters.AddWithValue("@id", id);
        
        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Vagas vagas = new Vagas();
            vagas.Id = reader.GetInt32(0);
            vagas.AdminId = reader.GetInt32(1);
           
            return vagas;
        }

        return null;
    }
}
