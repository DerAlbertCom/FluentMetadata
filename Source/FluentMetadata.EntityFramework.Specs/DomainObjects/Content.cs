using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentMetadata.EntityFramework.Specs.DomainObjects
{
    public abstract class Content : DomainObject, IWebSiteEntity
    {
        public override void Initialize()
        {
            base.Initialize();
            Comments = new Collection<Comment>();
            Tags = new Collection<Tag>();
            DisplayStart = new DateTime(1800, 1, 1);
            DisplayEnd = new DateTime(4000, 1, 1);
            Title = string.Empty;
            Abstract = string.Empty;
            Show = false;
        }

        public string Title { get; private set; }
        public string Abstract { get; private set; }
        public virtual WebUser Author { get; private set; }

        public DateTime DisplayStart { get; private set; }
        public DateTime DisplayEnd { get; private set; }
        public bool Show { get; private set; }

        public virtual WebSite WebSite { get; private set; }

        public bool CanShow
        {
            get
            {
                if (!Show)
                {
                    return false;
                }
                if (DateTime.Now >= DisplayStart)
                {
                    if (DisplayEnd >= DateTime.Now)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public virtual ICollection<Comment> Comments { get; private set; }

        public virtual ICollection<Tag> Tags { get; private set; }

        public void SetVisibility(DateTime displayStart, DateTime displayEnd)
        {
            if (DisplayStart != displayStart)
            {
                DisplayStart = displayStart;
                Modified();
            }
            if (!Equals(DisplayEnd, displayEnd))
            {
                DisplayEnd = displayEnd;
                Modified();
            }
        }

        public void SetShow(bool hidden)
        {
            if (Show != hidden)
            {
                Show = hidden;
                Modified();
            }
        }

        public void AddComment(Comment comment)
        {
            if (Comments.Contains(comment))
            {
                return;
            }
            Comments.Add(comment);
            Modified();
        }

        public void AddTag(Tag tag)
        {
            if (Tags.Contains(tag))
            {
                return;
            }
            Tags.Add(tag);
            Modified();
        }

        public void SetTitle(string titel)
        {
            Title = titel;
            Modified();
        }

        public void SetAbstract(string text)
        {
            Abstract = text;
            Modified();
        }

        public void SetVisibility(DateTime displayStart)
        {
            SetVisibility(displayStart, DateTime.MaxValue);
        }

        public void SetWebsite(WebSite webSite)
        {
            WebSite = webSite;
        }
    }
}