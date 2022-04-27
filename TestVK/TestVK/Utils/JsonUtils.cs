using Newtonsoft.Json;
using System.IO;
using TestVK.Models;

namespace TestVK.Utils
{
    internal static class JsonUtils
    {
        private const string UrlsFilePath = "Configs//urls.json";
        private const string TestDataFilePath = "TestData//user.json";

        public static Urls GetUrl()
        {
            Urls jsonData = JsonConvert.DeserializeObject<Urls>(File.ReadAllText(UrlsFilePath));
            return jsonData;
        }

        public static User GetUser()
        {
            User user = JsonConvert.DeserializeObject<User>(File.ReadAllText(TestDataFilePath));
            return user;
        }

        public static PostResponse GetPost(string stringJson)
        {
            PostResponse post = JsonConvert.DeserializeObject<PostResponse>(stringJson);
            return post;
        }

        public static LikesResponse GetLikedUsers(string stringJson)
        {
            LikesResponse post = JsonConvert.DeserializeObject<LikesResponse>(stringJson);
            return post;
        }

        public static UploadServerResponse GetUploadInfo(string stringJson)
        {
            UploadServerResponse uploadInfo = JsonConvert.DeserializeObject<UploadServerResponse>(stringJson);
            return uploadInfo;
        }

        public static ImageResponse GetImage(string stringJson)
        {
            ImageResponse imageResponse = JsonConvert.DeserializeObject<ImageResponse>(stringJson);
            return imageResponse;
        }

        public static SavedImageResponse GetImageFromServer(string stringJson)
        {
            SavedImageResponse imageResponse = JsonConvert.DeserializeObject<SavedImageResponse>(stringJson);
            return imageResponse;
        }
    }
}
