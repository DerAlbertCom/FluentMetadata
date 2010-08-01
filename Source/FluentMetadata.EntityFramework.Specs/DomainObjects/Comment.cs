namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public class Comment : Content
    {
        private Comment()
        {
            
        }
        public string EMail { get; private set; }
        public string Text { get; private set; }

        public void SetComment(string title, string text, string eMail)
        {
            SetTitle(title);
            Text = text;
            EMail = eMail;
            Modified();
        }
    }
}