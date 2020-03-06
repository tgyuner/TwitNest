using System;
using System.Collections.Generic;

namespace APIEntegrasyon.Models.WindowsService
{
    public class Alarm
    {
        public static List<Alarm> postAlarms;

        static Alarm()
        {
            postAlarms = new List<Alarm>();
        }

        public DateTime DeadLine { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }

        public static void AddList(Alarm postAlarm)
        {
            postAlarms.Add(postAlarm);
        }

        public static void RemoveList(Alarm postAlarm)
        {
            postAlarms.Remove(postAlarm);
        }

        public static List<DateTime> GetDateList()
        {
            List<DateTime> postDates = new List<DateTime>();

            foreach (var item in Alarm.postAlarms)
            {
                postDates.Add(item.DeadLine);
            }

            return postDates;
        }
    }

    public class Comment
    {
        public int Id { get; set; }
        public int ContentItemId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public bool IsPublished { get; set; }
        public CommentLike CommentLike { get; set; }
    }

    public class CommentLike
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int Like { get; set; }
        public string Dislike { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
        public Comment Comment { get; set; }
    }

}