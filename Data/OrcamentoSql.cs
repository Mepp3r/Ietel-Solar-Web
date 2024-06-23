using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Data.SqlClient;

public class OrcamentoSql : Database, IOrcamentoData
{    
    public List<Agendamento> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "select horario from Agendamentos group by horario having COUNT(*)=1";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Agendamento> lista = new List<Agendamento>();
        while (reader.Read())
        {       
            Agendamento agendamento = new Agendamento();
            agendamento.Horario = reader.GetString(0);
            
            lista.Add(agendamento);
        }

        reader.Close();

        return lista;
    }
    
    public void Orcamento(Orcamento orcamento)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "exec sp_cadOrcamentos @nomeCand, @cpfCand, @telefone, @cidade, @cep, @bairro, @rua, @numero, @data, @horario,@status, @AdminId";

        cmd.Parameters.AddWithValue("@nomeCand",orcamento.Nome);
        cmd.Parameters.AddWithValue("@cpfCand",orcamento.CPF);
        cmd.Parameters.AddWithValue("@telefone",orcamento.Telefone);
        cmd.Parameters.AddWithValue("@cidade",orcamento.Cidade);
        cmd.Parameters.AddWithValue("@cep",orcamento.CEP);
        cmd.Parameters.AddWithValue("@bairro",orcamento.Bairro);
        cmd.Parameters.AddWithValue("@rua",orcamento.Endereco);
        cmd.Parameters.AddWithValue("@numero",orcamento.Numero);
        cmd.Parameters.AddWithValue("@data",orcamento.Data);
        cmd.Parameters.AddWithValue("@horario",orcamento.Horario);
        cmd.Parameters.AddWithValue("@status", 0);
        cmd.Parameters.AddWithValue("@AdminId",1);

        cmd.ExecuteNonQuery();
    }

    public List<Orcamento> ListaOrcamento(string data)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "select * from View_Orcamento where data=@data";

        cmd.Parameters.AddWithValue("@data", data);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Orcamento> lista = new List<Orcamento>();

        while(reader.Read())
        {
            Orcamento orcamento = new Orcamento();
            orcamento.Nome = reader.GetString(0);
            orcamento.CPF = reader.GetString(1);
            orcamento.Telefone = reader.GetString(2);
            orcamento.Endereco = reader.GetString(3);
            orcamento.Bairro = reader.GetString(4);
            orcamento.Numero = reader.GetString(5);
            orcamento.CEP = reader.GetString(6);
            orcamento.Cidade = reader.GetString(7);
            orcamento.Data = reader.GetDataTypeName(8);
            orcamento.Horario = reader.GetString(9);

            lista.Add(orcamento);
        }

        return lista;
    }
}
