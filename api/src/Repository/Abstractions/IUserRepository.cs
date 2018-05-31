namespace Repository.Abstractions
{
    using Repository.Abstractions.Models;
    
    public interface IUserRepository
    {
        int Create(UserCreateModel model);

        void Update(UserUpdateModel model);

        void Delete(int id);

        bool Exists(UserCredentialsModel model);
    }
}