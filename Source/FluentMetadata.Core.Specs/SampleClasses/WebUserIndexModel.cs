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
        public int[] SecondaryRoles { get; set; }
        public string PasswordHash { get; set; }
        public string Comment { get; set; }
    }

    public class WebUserIndexGetModel : WebUserIndexModel
    {
        public IEnumerable<string> AllRoles { get; set; }
        public new UserRole Role { get; private set; }
        public new SelectList<UserRole> SecondaryRoles { get; set; }

        public class UserRole
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

    public class SelectList<T>
    {
        public IEnumerable<T> Items { get; internal set; }
    }
}