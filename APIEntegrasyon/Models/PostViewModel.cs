using System;

namespace APIEntegrasyon.Models
{
    // TODO
    public class BaseViewModel
    {
        public PostViewModel PostViewModel { get; set; }
    }

    public class PostViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string AuthorProfilPhoto { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class MemberList
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string ScreenName { get; set; }
        public string ProfilImage { get; set; }
    }
}