using System.IO;

namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class Picture : Content
    {
        private Picture()
        {
            
        }

        public int Position { get; private set; }
        public string OriginalFilename { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            SetOrder(1);
        }

        private void SetOrder(int order)
        {
            Position = order;
            Modified();
        }

        public void SetOriginalFilename(string originalFilename)
        {
            OriginalFilename = originalFilename;
            Modified();
        }

        public string GetFilename()
        {
            return string.Format("{0}.jpg", Id);
        }

        public string GetOriginalSaveFilename()
        {
            return string.Format("{0}{1}", Id,Path.GetExtension(OriginalFilename));
        }
    }
}