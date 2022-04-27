using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using TestVK.Models;

namespace TestVK.PageObjects
{
    internal class ProfilePage : Form
    {
        public ProfilePage() : base(By.Id("profile_short"), "Profile page") { }

        public PostForm GetPost(string userId, int postId)
        {
            return new PostForm(userId, postId);
        }
    }
}
