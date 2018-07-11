namespace Business.Entities
{
    using System;
    using System.Collections.Generic;

    public class RegistrationParent : IHasId
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}