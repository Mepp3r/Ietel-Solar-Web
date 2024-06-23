public interface IVagasData
{
    public void AdicionaVaga(Vagas vagas);

    public void Remover(int id);
    
    public void Alterar(Vagas form);
    
    public List<Vagas> Read();
}
