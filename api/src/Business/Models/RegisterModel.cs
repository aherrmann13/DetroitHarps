namespace Business.Models
{
    using System;
    using System.Collections.Generic;

    public class RegisterModel
    {
        public RegisterModel()
        {
            this.Children = new List<RegisterChildModel>();
        }

        public RegisterContactInformationModel ContactInformation { get; set; }
        
        public RegisterParentModel Parent { get; set; }

        public IList<RegisterChildModel> Children { get; set; }
    }
}