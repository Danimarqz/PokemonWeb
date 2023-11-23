namespace Pokemon.Models.Repository
{
    public interface ITipoRepository
    {
        Task<IEnumerable<Tipo>> GetTipos(int id);
    }
}
