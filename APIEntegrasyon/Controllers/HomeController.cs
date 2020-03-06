using TweetSharp;
using System.Web.Mvc;
using APIEntegrasyon.Models;
using System.Collections.Generic;
using APIEntegrasyon.Models.WindowsService;

namespace APIEntegrasyon.Controllers
{
    public class HomeController : Controller
    {
        //TODO Bu Uygulamanın çalışması için Twitter üzerinden apı için key almalısınız!!!
        //TODO For this app to work, you must get the key of the API on Twitter!!!
        private readonly TwitterService _tweetService = new TwitterService("consumerKey", "consumerSecret", "token", "tokenSecret");

        [HttpGet]
        public ActionResult Index()
        {
            var timeLine = _tweetService.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions() { Count = 15 });
            var userTimeLine = _tweetService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = 15 });
            var mentioningMe = _tweetService.ListTweetsMentioningMe(new ListTweetsMentioningMeOptions() { Count = 15 });
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

            return View(postViewModelList);
        }

        [HttpGet]
        public JsonResult SendDirectMessage(long receiverId, string message)
        {
            if (receiverId != default(long))
            {
                _tweetService.SendDirectMessage(new SendDirectMessageOptions { UserId = receiverId, Text = message });
                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Başarısız", JsonRequestBehavior.DenyGet);
            }
        }

        [HttpGet]
        public ActionResult DeleteTweet(long id)
        {
            bool isSuccess = false;
            if (id != default(long))
            {
                _tweetService.DeleteTweet(new DeleteTweetOptions() { Id = id });
                isSuccess = true;
            }

            return View();
        }

        [HttpGet]
        public ActionResult FollowerListResult()
        {
            // TODO bu method TgyUNER kullanıcısı için yazılmıştır başka kullanıcı için kendi kullanıcı adınızı girmelisiniz.
            // TODO This method is written for the TgyUNER user. You must enter your own user name for another user.

            var user = _tweetService.GetUserProfileFor(new GetUserProfileForOptions() { ScreenName = "TgyUNER" });

            var followers = _tweetService.ListFollowers(new ListFollowersOptions()
            {
                ScreenName = user.ScreenName,
                UserId = user.Id
            });

            var model = new List<MemberList>();
            foreach (var item in followers)
            {
                model.Add(new MemberList
                {
                    Id = item.Id,
                    ProfilImage = item.ProfileImageUrl,
                    ScreenName = item.ScreenName,
                    UserName = item.Name
                });
            }

            return PartialView("~/Views/Home/_FollowerListResult.cshtml", model);
        }

        [HttpGet]
        public ActionResult AddNotification()
        {
            return PartialView("~/Views/Home/_AddNotification.cshtml");
        }

        [HttpPost]
        public ActionResult AddNotification(Alarm model)
        {
            //TODO bu method TgyUNER kullanıcısı için yazılmıştır başka kullanıcı için kendi kullanıcı adınızı girmelisiniz.
            var user = _tweetService.GetUserProfileFor(new GetUserProfileForOptions() { ScreenName = "TgyUNER" });
            model.UserName = user.ScreenName;

            Alarm.AddList(model);

            return RedirectToAction("Index");
        }
    }
}