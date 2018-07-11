namespace Business.Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IContactManager
    {
        void Contact(ContactModel model);

    }
}