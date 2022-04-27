using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System.Collections.Generic;
using TestVK.ApiRequests;
using TestVK.Models;
using TestVK.PageObjects;
using TestVK.Utils;

namespace TestVK.Tests
{
    internal class VkApiTest : BaseTest 
    {
        public static User user = JsonUtils.GetUser();
        public static AuthorizationPage authorizationPage = new AuthorizationPage();
        public static FeedsPage feedsPage = new FeedsPage();
        public static ProfilePage profilePage = new ProfilePage();
        
        Dictionary<string, string> menuItems = new Dictionary<string, string>()
                {
                    {"Profile", "l_pr"},
                    {"Feeds", "l_nwsf"},
                    {"Messages", "l_msg"},
                };

        [Test]
        public void Test()
        {
            AqualityServices.Logger.Info("Step 2 - performing authorization");
            authorizationPage.PerformAuthorization(user);
            feedsPage.State.WaitForDisplayed();

            AqualityServices.Logger.Info("Step 3 - going to MyPage");
            feedsPage.MenuNavigate(menuItems["Profile"]);
            profilePage.State.WaitForDisplayed();

            AqualityServices.Logger.Info("Step 4 - creating new post by request");
            string postsText = Util.RandomString(10);
            Post post = VkApiUtils.CreatePost(user, postsText).Response;
            PostForm postForm = profilePage.GetPost(user.UserId, post.Post_id);

            AqualityServices.Logger.Info("Step 5 - checking new post by text and user");
            Assert.AreEqual(postsText, postForm.GetPostsText(user.UserId, post.Post_id), "Sending text is not equal to text from page");
            Assert.AreEqual(user.UserName, postForm.GetPostsAuthor(user.UserId, post.Post_id), "Posts author is not equal to user");

            AqualityServices.Logger.Info("Step 6 - editing post");
            VkApiUtils.UpdatePostWithImage(user, Util.RandomString(10), post.Post_id.ToString(), out string postsImageId);

            AqualityServices.Logger.Info("Step 7 - checking updated post");
            Assert.AreNotEqual(postsText, postForm.GetPostsText(user.UserId, post.Post_id), "Posts text is not equal to updated text");
            Assert.True(Util.CompareImages(Util.LoadBitmap("LocalSource//4282792.jpg"), Util.LoadBitmap(postForm.SaveImageAndGetPath(user.UserId, post.Post_id))), "Images id is not equal");

            AqualityServices.Logger.Info("Step 8 - adding comment");
            VkApiUtils.CreateComment(user, Util.RandomString(10), post.Post_id.ToString());

            AqualityServices.Logger.Info("Step 9 - checking comment");
            Assert.AreEqual(user.UserName, postForm.GetCommentAuthor(user.UserId, post.Post_id), "Comments author is not equal to user");

            AqualityServices.Logger.Info("Step 10 - sending like");
            postForm.LikePost(user.UserId, post.Post_id);

            AqualityServices.Logger.Info("Step 11 - checking like");
            Assert.AreEqual(user.UserId, VkApiUtils.CheckLikedUser(user, post.Post_id.ToString()).Response.users[0].Uid.ToString(), "Liked user id is not equal to user id");
            
            AqualityServices.Logger.Info("Step 12 - deleting post");
            VkApiUtils.DeletePost(user, post.Post_id.ToString());
            postForm.WaitPostRemove(user.UserId, post.Post_id);

            AqualityServices.Logger.Info("Step 13 - checking deleted post");
            Assert.False(postForm.IsPostDisplayed(user.UserId, post.Post_id), "Post is not deleted");
        }
    }
}