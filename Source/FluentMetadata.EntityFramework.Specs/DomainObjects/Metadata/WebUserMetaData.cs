namespace FluentMetadata.EntityFramework.Specs.DomainObjects.Metadata
{
    public class WebUserMetadata : DomainObjectMetadata<WebUser>
    {
        public WebUserMetadata()
        {
            Property(x => x.Username).Length(MetadataInfo.NameLength).Is.Required().Is.ReadOnly()
                .Display.Name("Benutzername");
            Property(x => x.EMail).Length(MetadataInfo.EMailLength).Is.Required().As.EmailAddress()
                .Display.Name("E-Mail").As.EmailAddress();
            Property(x => x.PasswordHash).Length(64).Is.Required()
                .Should.Not.ShowInDisplay().Should.Not.ShowInEditor();
            Property(x => x.Role).UIHint("Roles").Length(256).Is.Required()
                .Display.Name("Rolle");
            Property(x => x.PasswordHash).Should.Not.ShowInDisplay().Should.Not.ShowInEditor();
            Property(x => x.ConfirmationKey).Should.Not.ShowInEditor().Should.Not.ShowInDisplay();
            Property(x => x.LastLogin).Should.Not.ShowInEditor()
                .Display.Name("Letzte Anmeldung").Display.NullText("<nie angemeldet>");
            Property(x => x.BounceCount).Should.Not.ShowInEditor()
                .Display.Name("E-Mail Fehler");
            Property(x => x.Confirmed).Should.Not.HiddenInput()
                .Display.Name("Bestätigt");
            Property(x => x.Active)
                .Display.Name("Aktiv");
            Class.Display.Name("Benutzer");
        }
    }
}