using System.Collections.Generic;
using APIEntegrasyon.Models;

namespace APIEntegrasyon.Helper
{
    public interface IHelper
    {
        bool SendTweet(string data);
        bool DeleteTweet(long id);
        List<BaseViewModel> TimeLine();
    }
}