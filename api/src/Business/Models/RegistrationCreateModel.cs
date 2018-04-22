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

        public string StripeToken { get; set; }

        public string Comments { get; set; }

        public RegistrationType RegistrationType { get; set;  }
    }
}