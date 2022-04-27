using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace TestVK.PageObjects
{
    internal class FeedsPage : Form
    {
        private IButton menuItem(string menuItem) => ElementFactory.GetButton(By.Id(menuItem), "'My page' button");

        public FeedsPage() : base(By.Id("ui_rmenu_news"), "Feeds page") { }

        public void MenuNavigate(string menuItem)
        {
            this.menuItem(menuItem).Click();
        }
    }
}
