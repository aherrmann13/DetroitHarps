namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IUserManager
    {
        int? GetUserId(UserCredentialsModel model);
    }
}