namespace DetroitHarps.Business.Registration.Models
{
    using System.Collections.Generic;

    public class RegisterModel
    {
        public RegisterContactInformationModel ContactInformation { get; set; }

        public RegisterParentModel Parent { get; set; }

        public IList<RegisterChildModel> Children { get; set; } = new List<RegisterChildModel>();
    }
}