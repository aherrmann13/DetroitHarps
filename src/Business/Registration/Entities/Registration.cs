namespace DetroitHarps.Business.Registration.Entities
{
    using System;
    using System.Collections.Generic;

    public class Registration : IHasId
    {
        public int Id { get; set; }

        public int SeasonYear { get; set; }

        public RegistrationParent Parent { get; set; }

        public RegistrationContactInformation ContactInformation { get; set; }

        public RegistrationPaymentInformation PaymentInformation { get; set; }

        public DateTimeOffset RegistrationTimestamp { get; set; }

        public IList<RegistrationChild> Children { get; set; } = new List<RegistrationChild>();
    }
}