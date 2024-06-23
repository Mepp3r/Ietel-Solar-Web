using Microsoft.Data.SqlClient;

public class AgendamentoSql : Database, IAgendamentoData
{
    public void Agendamento(Agendamento agendamento)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Agendamentos (AdminId,data,horario,status) values(@adminId, @data,@horario,@status)";

        cmd.Parameters.AddWithValue("@adminId", 1);
        cmd.Parameters.AddWithValue("@data", agendamento.Data);
        cmd.Parameters.AddWithValue("@horario", agendamento.Horario);
        cmd.Parameters.AddWithValue("@status", 0);

        cmd.ExecuteNonQuery();
    }

    public async Task<string> ReadAgendamento(string date)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT distinct horario FROM Agendamentos WHERE data = @date";

        cmd.Parameters.AddWithValue("@date", date);

        List<string> lista = new List<string>();

        SqlDataReader reader = await cmd.ExecuteReaderAsync();

        while(reader.Read())
        {
            lista.Add(reader["horario"].ToString());
        }

        if(lista.Count == 0)
        {
            return "Não há horários cadastrados !";
        }
        else
        {
            return String.Join("\n", lista.ToArray());
        }
    }
    public List<Agendamento> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * from Agendamentos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Agendamento> lista = new List<Agendamento>();
        while (reader.Read())
        {
        
            Agendamento agendamento = new Agendamento();
            agendamento.Id = reader.GetInt32(0);
            agendamento.Data = reader.GetString(1);
            agendamento.Horario = reader.GetString(2);
            agendamento.Status = reader.GetInt32(3);
            
            lista.Add(agendamento);

        }

        return lista;  
    }
}
