using System;

namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class RemoteAction : IInitializable
    {
        private RemoteAction()
        {
        }

        public int ObjectId { get; private set; }

        public string Action { get; private set; }

        public Guid ConfirmationKey { get; private set; }

        public DateTime Expiration { get; private set; }

        public bool Expired
        {
            get { return DateTime.UtcNow > Expiration; }
        }

        public void SetAction(string action, int objectId)
        {
            Action = action;
            ObjectId = objectId;
        }

        public void SetExpirationHours(double  hoursToExpiration)
        {
            Expiration = DateTime.UtcNow.AddHours(hoursToExpiration);
        }

        public void Initialize()
        {
            ConfirmationKey = Guid.NewGuid();
            Expiration = new DateTime(4000,1,1);
        }
    }
}