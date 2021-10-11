using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseActionReusables.GeneralUtilities.TypeUtilities;
using System.Drawing;

namespace CommonDatabaseActionReusables.AnnouncementManager
{
    public class Announcement
    {


        private Announcement(int id, string title, byte[] content, byte[] mainImage, DateTime dateTimeCreated, DateTime dateTimeLastModified)
        {
            Id = id;
            Title = title;
            Content = content;
            MainImage = mainImage;
            DateTimeCreated = dateTimeCreated;
            DateTimeLastModified = dateTimeLastModified;
        }

        public int Id { get; }

        public string Title { get; }

        public byte[] Content { get; } //varbinary (maxlength)

        public byte[] MainImage { get; } //varbinary (maxlength)

        public DateTime DateTimeCreated { get; }

        public DateTime DateTimeLastModified { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Content as string, assuming that content is assigned with only string.</returns>
        public string GetContentAsString()
        {
            return StringUtilities.ConvertByteArrayToString(Content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>MainImage as <see cref="Image"/> instead of byte array.</returns>
        public Image GetMainImageAsImage()
        {
            return ImageUtilities.ConvertBytesToImage(MainImage);
        }



        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                return ((Announcement)(obj)).Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public Builder ConstructBuilderUsingSelf()
        {
            var builder = new Builder();
            builder.Title = Title;
            builder.Content = Content;
            builder.MainImage = MainImage;
            builder.DateTimeCreated = DateTimeCreated;
            builder.DateTimeLastModified = DateTimeLastModified;

            return builder;
        }


        public class Builder
        {

            public string Title { set; get; }

            public byte[] Content { set; get; }

            public byte[] MainImage { set; get; }

            public DateTime DateTimeCreated { set; get; }

            public DateTime DateTimeLastModified { set; get; }



            public void SetContentUsingString(string contentAsString)
            {
                Content = StringUtilities.ConvertStringToByteArray(contentAsString);
            }

            public void SetMainImageUsingImage(Image image)
            {
                MainImage = ImageUtilities.ConvertImageToBytes(image);
            }


            internal Announcement Build(int id)
            {
                return new Announcement(id, Title, Content, MainImage, DateTimeCreated, DateTimeLastModified);
            }

        }


    }
}