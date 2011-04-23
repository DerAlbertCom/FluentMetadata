using System;

namespace FluentMetadata.Specs.SampleClasses.MetaData
{
    public class WebUserMetadata : DomainObjectMetadata<WebUser>
    {
        public WebUserMetadata()
        {
            Property(x => x.Username).Length(3, 256).Is.Required().Is.ReadOnly()
                .Display.Name("Benutzername").Description("Name des Benutzers");
            Property(x => x.EMail).Length(128).Is.Required().As.EmailAddress()
                .Display.Name("E-Mail").Display.Format("<a href='mailto:{0}'>{0}</a>");
            Property(x => x.PasswordHash).Length(32, null).Is.Required()
                .Should.Not.ShowInDisplay().Should.Not.ShowInEditor();
            Property(x => x.Role).UIHint("Roles").Length(256).Is.Required()
                .Display.Name("Rolle");
            Property(x => x.PasswordHash).Should.Not.ShowInDisplay().Should.Not.ShowInEditor();
            Property(x => x.ConfirmationKey).Should.Not.ShowInEditor().Should.Not.ShowInDisplay();
            Property(x => x.LastLogin).Should.Not.ShowInEditor()
                .Display.Name("Letzte Anmeldung")
                .Display.NullText("<nie angemeldet>")
                .Range(new DateTime(2010, 1, 23), DateTime.MaxValue); //support ends on doomsday
            Property(x => x.BounceCount).Should.Not.ShowInEditor()
                .Display.Name("E-Mail Fehler");
            Property(x => x.Confirmed).Should.Not.HiddenInput()
                .Display.Name("Bestätigt");
            Property(x => x.Active)
                .Display.Name("Aktiv");
            Class.Display.Name("Benutzer");
        }
    }

    public class AutorMetadata : DomainObjectMetadata<Autor>
    {
        public AutorMetadata()
        {
            Property(e => e.Name).Display.Name("emaN");
        }
    }
}