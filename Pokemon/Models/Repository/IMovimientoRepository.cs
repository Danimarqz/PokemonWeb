namespace Pokemon.Models.Repository
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<movimiento>> GetMovimientos(int id);
    }
}
