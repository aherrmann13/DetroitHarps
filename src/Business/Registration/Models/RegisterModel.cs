namespace DetroitHarps.Business.Registration.Models
{
    using System.Collections.Generic;

    public class RegisterModel
    {
        public RegisterContactInformationModel ContactInformation { get; set; } =
            new RegisterContactInformationModel();

        public RegisterParentModel Parent { get; set; } = new RegisterParentModel();

        public RegisterPaymentModel Payment { get; set; } = new RegisterPaymentModel();

        public IList<RegisterChildModel> Children { get; set; } =
            new List<RegisterChildModel>();
    }
}