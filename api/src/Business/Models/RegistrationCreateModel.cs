namespace Business.Models
{
    using System.Collections.Generic;

    public class RegistrationCreateModel : RegistrationModelBase
    {
        public RegistrationCreateModel()
        {
            Children = new List<ChildInformationCreateModel>();
        }

        public IList<ChildInformationCreateModel> Children { get; set; }
    }
}