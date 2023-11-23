namespace Pokemon.Models.Repository
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<Movimiento>> GetMovimientos(int id);
    }
}
