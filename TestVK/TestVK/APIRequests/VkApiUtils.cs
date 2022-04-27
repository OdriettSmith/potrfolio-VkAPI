using RestSharp;
using System.Collections.Generic;
using TestVK.Models;
using TestVK.Utils;

namespace TestVK.ApiRequests
{
    internal static class VkApiUtils
    {
        public static PostResponse CreatePost(User user, string text)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"message", text},
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken}
                };
            IRestResponse createPostResponse = ApiUtils.PostRequest("wall.post", queryParams);
            return JsonUtils.GetPost(createPostResponse.Content);
        }

        public static string GetUploadServer(User user)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken}
                };
            IRestResponse editPostResponse = ApiUtils.PostRequest("photos.getWallUploadServer", queryParams);
            UploadServerResponse response = JsonUtils.GetUploadInfo(editPostResponse.Content);
            return response.Response.Upload_url;
        }

        public static ImageResponse UploadPicture(string uploadUrl)
        {
            IRestResponse editPostResponse = ApiUtils.PostRequestFormData(uploadUrl, "LocalSource//4282792.jpg");
            return JsonUtils.GetImage(editPostResponse.Content);
        }

        public static SavedImageResponse SaveImage(User user, ImageResponse image)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken},
                    {"photo", image.Photo.Replace("\\", "")},
                    {"server", image.Server.ToString()},
                    {"hash", image.Hash},
                };
            IRestResponse editPostResponse = ApiUtils.PostRequest("photos.saveWallPhoto", queryParams);
            return JsonUtils.GetImageFromServer(editPostResponse.Content);
        }

        public static PostResponse EditPost(User user, string text, string postId, string photoId)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"post_id", postId},
                    {"message", text},
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken},
                    {"attachment", photoId}
                };
            IRestResponse editPostResponse = ApiUtils.PostRequest("wall.edit", queryParams);
            return JsonUtils.GetPost(editPostResponse.Content);
        }

        public static void UpdatePostWithImage(User user, string newText, string postId, out string postImageId)
        {
            postImageId = "photo" + user.UserId + "_" + SaveImage(user, UploadPicture(GetUploadServer(user))).Response[0].Id;
            EditPost(user, newText, postId, postImageId);
        }

        public static PostResponse CreateComment(User user, string text, string postId)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"post_id", postId},
                    {"message", text},
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken}
                };
            IRestResponse editPostResponse = ApiUtils.PostRequest("wall.createComment", queryParams);
            return JsonUtils.GetPost(editPostResponse.Content);
        }

        public static void DeletePost(User user, string postId)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"post_id", postId},
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken}
                };
            ApiUtils.PostRequest("wall.delete", queryParams);
        }

        public static LikesResponse CheckLikedUser(User user, string postId)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
                {
                    {"post_id", postId},
                    {"v", user.ApiVersion},
                    {"access_token", user.AccessToken}
                };
            IRestResponse editPostResponse = ApiUtils.PostRequest("wall.getLikes", queryParams);
            return JsonUtils.GetLikedUsers(editPostResponse.Content);
        }
    }
}
