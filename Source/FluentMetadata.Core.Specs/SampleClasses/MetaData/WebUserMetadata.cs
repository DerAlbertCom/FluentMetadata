using System;

namespace FluentMetadata.Specs.SampleClasses.MetaData
{
    public class WebUserMetadata : DomainObjectMetadata<WebUser>
    {
        public WebUserMetadata()
        {
            Property(x => x.Username)
                .Length(3, 256)
                .Is.Required()
                .Is.ReadOnly()
                .Display.Name(() => "Benutzername")
                .Description(() => "Name des Benutzers")
                .Is.Not.ConvertEmptyStringToNull();
            Property(x => x.EMail)
                .Length(128)
                .Is.Required()
                .As.EmailAddress()
                .Display.Name("E-Mail")
                .Display.Format("<a href='mailto:{0}'>{0}</a>")
                .Editor.Format("{0}")
                .Editor.Watermark(() => "john@doe.com");
            Property(x => x.PasswordHash)
                .Length(32, null)
                .Is.Required()
                .Should.Not.ShowInDisplay()
                .Should.Not.ShowInEditor();
            Property(x => x.Role)
                .UIHint("Roles")
                .Length(256)
                .Is.Required()
                .Display.Name("Rolle");
            Property(x => x.ConfirmationKey)
                .Should.Not.ShowInEditor()
                .Should.Not.ShowInDisplay();
            Property(x => x.LastLogin)
                .Should.Not.ShowInEditor()
                .Display.Name("Letzte Anmeldung")
                .Display.NullText(() => "<nie angemeldet>")
                .Range(new DateTime(2010, 1, 23), DateTime.MaxValue); //support ends on doomsday
            Property(x => x.BounceCount)
                .Should.Not.ShowInEditor()
                .Display.Name("E-Mail Fehler")
                .AssertThat(
                    bc => ValidateBounceCountAgainstSomeConfiguration(bc),
                    () => "{0} is too high. Email address is considered invalid.");
            Property(x => x.Confirmed)
                .Should.Not.HiddenInput()
                .Display.Name("Bestätigt");
            Property(x => x.Active)
                .Display.Name("Aktiv");
            Property(x => x.Comment)
                .Is.Not.RequestValidationEnabled();
            Class
                .Display.Name(() => "Benutzer")
                .AssertThat(
                    u => u.Username != u.Autor.Name,
                    () => "{0}.Username and {0}.Autor.Name must not be equal");
        }

        bool ValidateBounceCountAgainstSomeConfiguration(int bounceCount)
        {
            return bounceCount < 3;
        }
    }

    public class AutorMetadata : DomainObjectMetadata<Autor>
    {
        public AutorMetadata()
        {
            Property(e => e.Name)
                .Display.Name("emaN")
                .Display.NullText("Anonymous Autor");
        }
    }

    public class WebUserIndexModelMetadata : ClassMetadata<WebUserIndexModel>
    {
        public WebUserIndexModelMetadata()
        {
            CopyMetadataFrom<WebUser>();
            Property(x => x.SecondaryRoles)
                .Display.Name("Secondaly lores");
        }
    }

    public class WebUserIndexGetModelMetadata : ClassMetadata<WebUserIndexGetModel>
    {
        public WebUserIndexGetModelMetadata()
        {
            CopyMetadataFrom<WebUserIndexModel>();
            Property(x => x.PasswordHash)
               .Is.Not.Required();
        }
    }
}