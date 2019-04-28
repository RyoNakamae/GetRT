using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tweet
{
    public Dictionary<string, List<CoreTweet.Status>> GetTweet(long start_id, string screen_name)
    {
        var API_Key = "<API_Key>";
        var API_Secret = "<API_Secret>";
        var Access_Token = "<Access_Token>";
        var Access_Token_Secret = "<Access_Token_Secret>";

        var tokens = CoreTweet.Tokens.Create(API_Key, API_Secret, Access_Token, Access_Token_Secret);

        // とりあえず指定したユーザの指定ID以降のツィートを最大200件取得：日付ごとに辞書に格納
        var parm = new Dictionary<string, object>();
        parm["screen_name"] = screen_name;
        parm["since_id"] = start_id;
        parm["include_rts"] = true;
        parm["count"] = 200;
        var tweets = tokens.Statuses.UserTimeline(parm);

        var dict_date = new Dictionary<string, List<CoreTweet.Status>>();
        foreach (var tweet in tweets)
        {
            var localtime_date = tweet.CreatedAt.LocalDateTime.Date.ToString("yyyy/MM/dd");
            if (! dict_date.ContainsKey(localtime_date))
            {
                dict_date[localtime_date] = new List<CoreTweet.Status>();
            }

            dict_date[localtime_date].Add(tweet);
        }
        return dict_date;
    }
}


