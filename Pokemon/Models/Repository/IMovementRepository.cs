namespace Pokemon.Models.Repository
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetMovimientos(int id);
    }
}
