using System.Collections.Generic;

namespace FluentMetadata.Specs.SampleClasses
{
    public class WebUserIndexModel : DomainModel
    {
        public string Username { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public string AutorName { get; set; }
    }

    public class WebUserIndexGetModel : WebUserIndexModel
    {
        public IEnumerable<string> AllRoles { get; set; }
        public new UserRole Role { get; private set; }

        public class UserRole { }
    }
}