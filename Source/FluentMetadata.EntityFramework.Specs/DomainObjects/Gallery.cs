using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class Gallery : Content
    {
        private Gallery()
        {
              EnsurePictures();  
        }

        public void AddPicture(Picture picture)
        {
            EnsurePictures();
            Pictures.Add(picture);
            Modified();
        }

        public virtual ICollection<Picture> Pictures { get; private set; }

        private void EnsurePictures()
        {
            if (Pictures == null)
                Pictures = new Collection<Picture>();
        }
    }
}