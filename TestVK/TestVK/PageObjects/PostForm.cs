using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace TestVK.PageObjects
{
    internal class PostForm : Form
    {
        private static ILabel postsText(string id, int postId) => ElementFactory.GetLabel(By.CssSelector($"#post{id}_{postId} .wall_post_text"), "Text from post");
        private static ILabel postAuthor(string id, int postId) => ElementFactory.GetLabel(By.CssSelector($"#post{id}_{postId} .author"), "Posts author");
        private static ILabel commentAuthor(string id, int postId) => ElementFactory.GetLabel(By.CssSelector($"#replies{id}_{postId} .author"), "Comments author");
        private static IButton likeBtn(string id, int postId) => ElementFactory.GetButton(By.CssSelector($"#post{id}_{postId} ._like_button_icon"), "Like button");
        private static ILabel postImage(string id, int postId) => ElementFactory.GetLabel(By.CssSelector($"#post{id}_{postId} .image_cover"), "Posts image");
        private static IButton showComments = ElementFactory.GetButton(By.CssSelector(".js-replies_next_label"), "Show comment button");

        public PostForm(string id, int postId) : base(By.Id($"#post{id}_{postId}"), "Post") { }

        public string GetPostsText(string userId, int postId)
        {
            return postsText(userId, postId).Text;
        }

        public string GetPostsAuthor(string userId, int postId)
        {
            return postAuthor(userId, postId).Text;
        }

        public string SaveImageAndGetPath(string userId, int postId)
        {
            string imgName = "downloadedImg.jpg";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(postImage(userId, postId).GetAttribute("href"), AppDomain.CurrentDomain.BaseDirectory + imgName);
            }
            return imgName;
        }

        public string GetCommentAuthor(string userId, int postId)
        {
            showComments.Click();
            return commentAuthor(userId, postId).Text;
        }

        public void LikePost(string userId, int postId)
        {
            likeBtn(userId, postId).Click();
        }

        public void WaitPostRemove(string userId, int postId)
        {
            postsText(userId, postId).State.WaitForNotDisplayed();
        }

        public bool IsPostDisplayed(string userId, int postId)
        {
            return postsText(userId, postId).State.IsDisplayed;
        }
    }
}
