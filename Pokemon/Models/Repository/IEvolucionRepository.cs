namespace Pokemon.Models.Repository
{
    public interface IEvolucionRepository
    {
        Task<Evolucion> GetEvolucion(int id);
        Task<Evolucion> GetOrigen (int id);
    }
}
