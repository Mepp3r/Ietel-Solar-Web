public interface ICandidatoData
{
    public void TrabalheConosco(int id, int adminId, Candidato candidato);
    public List<Candidato> Read(int id);
    public Vagas ReadVaga(int id);
}