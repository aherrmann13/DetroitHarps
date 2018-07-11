namespace Business.Abstractions
{
    using Business.Entities;

    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}