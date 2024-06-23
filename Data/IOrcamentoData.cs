public interface IOrcamentoData
{
  public List<Agendamento> Read();

  public List<Orcamento> ListaOrcamento(string data);

  public void Orcamento(Orcamento orcamento);

  
 
     
}
