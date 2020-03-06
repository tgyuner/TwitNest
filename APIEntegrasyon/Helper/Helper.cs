using TweetSharp;
using APIEntegrasyon.Helper;
using System.Collections.Generic;

namespace APIEntegrasyon.Models
{
    public class Helper : IHelper
    {
        //TODO Bu Uygulamanın çalışması için Twitter üzerinden, API için key almalısınız!!!
        //TODO For this app to work, you must get the key of the API on Twitter!!!
        //("consumerKey", "consumerSecret", "token", "tokenSecret");
        readonly TwitterService _tweetService = new TwitterService
            ("consumerKey", "consumerSecret", "token", "tokenSecret");

        public bool SendTweet(string data)
        {
            bool isSuccess = false;
            if (data != default(string))
            {
                _tweetService.SendTweet(new SendTweetOptions() { Status = data });
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }

            return (isSuccess);
        }

        public bool DeleteTweet(long id)
        {
            bool isSuccess = false;
            if (id != default(long))
            {
                _tweetService.DeleteTweet(new DeleteTweetOptions() { Id = id });
                isSuccess = true;
            }

            return isSuccess;
        }

        public List<BaseViewModel> TimeLine()
        {
            var timeLine = _tweetService.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 15 });
            var postViewModelList = new List<BaseViewModel>();

            foreach (var post in timeLine)
            {
                if (post.Id != default(int))
                {
                    postViewModelList
                        .Add(new BaseViewModel
                        {
                            PostViewModel = new PostViewModel
                            {
                                Id = post.Id,
                                Content = post.TextAsHtml,
                                UserName = post.Author.ScreenName,
                                AuthorProfilPhoto = post.Author.ProfileImageUrl,
                                Date = post.CreatedDate,
                            }
                        });
                }
            }

            return postViewModelList;
        }
    }
}