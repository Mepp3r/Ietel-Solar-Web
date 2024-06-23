public interface IAgendamentoData
{
    
    public void Agendamento(Agendamento agendamento);
    public List<Agendamento> Read();
    public Task<string> ReadAgendamento(string date);
    
}
