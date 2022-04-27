using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using TestVK.Models;

namespace TestVK.PageObjects
{
    internal class AuthorizationPage : Form
    {
        private ITextBox emailTextBox = ElementFactory.GetTextBox(By.Id("index_email"), "Email textbox");
        private ITextBox passwordTextBox => ElementFactory.GetTextBox(By.Id("index_pass"), "Password textbox");
        private IButton signInBtn => ElementFactory.GetButton(By.Id("index_login_button"), "Sign in button");

        public AuthorizationPage() : base(By.CssSelector(".login_header"), "Authorization page") { }

        public void PerformAuthorization(User user)
        {
            emailTextBox.Type(user.Email);
            passwordTextBox.Type(user.Password);
            signInBtn.Click();
        }
    }
}
