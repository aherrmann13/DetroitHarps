namespace Business.Entities
{
    using System;
    using System.Collections.Generic;

    public class Registration : IHasId
    {
        public Registration()
        {
            this.Children = new List<RegistrationChild>();
        }

        public int Id { get; set; }

        public int SeasonYear { get; set; }

        public RegistrationParent Parent { get; set; }

        public RegistrationContactInformation ContactInformation { get; set; }

        public RegistrationPaymentInformation PaymentInformation { get; set; }

        public DateTimeOffset RegistrationTimestamp { get; set; }

        public IList<RegistrationChild> Children { get; set; }
    }
}