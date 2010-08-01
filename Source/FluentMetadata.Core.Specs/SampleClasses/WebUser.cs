using System;

namespace FluentMetadata.Specs.SampleClasses
{
    public class Autor : DomainObject
    {
        public string Name { get; set; }
    }

    public class WebUser : DomainObject
    {
        private WebUser()
        {
        }

        public override  void Initialize()
        {
            base.Initialize();
            PasswordHash = string.Empty;
            Confirmed = false;
            Active = false;
        }

        public string Username { get; private set; }

        public string EMail { get; private set; }

        public string PasswordHash { get; private set; }
        public bool Confirmed { get; private set; }
        public bool Active { get; private set; }

        public int BounceCount { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public Guid? ConfirmationKey { get; private set; }
        public string Role { get; private set; }
        public Autor Autor { get; private set; }

        public void SetEMailAddress(string emailAddress)
        {
            emailAddress = emailAddress.ToLower();
            if (string.CompareOrdinal(EMail, emailAddress) != 0)
            {
                EMail = emailAddress;
                Modified();
            }
        }


        private string GetUserSalt()
        {
            return (Created.Second*Created.DayOfYear).ToString();
        }

        public void MailHasBounced()
        {
            BounceCount++;
            Modified();
        }

        public void Activate()
        {
            Active = true;
            ClearConfirmationKey();
            Modified();
        }

        public void ConfirmUser()
        {
            Confirmed = true;
            Activate();
        }

        public void Deactivate()
        {
            Active = false;
            ClearConfirmationKey();
            Modified();
        }

        public void ClearConfirmationKey()
        {
            ConfirmationKey = null;
            Modified();
        }

        public void EnsureConfirmationKey()
        {
            if (ConfirmationKey.HasValue)
            {
                return;
            }
            ConfirmationKey = Guid.NewGuid();
        }

        public void LoggedIn()
        {
            LastLogin = DateTime.UtcNow;
            Modified();
        }

        public bool ConfirmationKeyIsValid(Guid confirmationKey)
        {
            bool isValid = false;
            if (ConfirmationKey.HasValue)
            {
                isValid = ConfirmationKey.Value == confirmationKey;
            }
            return isValid;
        }
    }
}