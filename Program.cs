using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetRT
{
    class Program
    {
        static void Main(string[] args)
        {
            Tweet tw = new Tweet();

            // スタートのTweetID
            var start_id = 111111;
            var screen_name = "<screen_name>";
            var file_path = @"<file_path>";

            var dict_date = tw.GetTweet(start_id, screen_name);

            var url_temp = "https://twitter.com/{0}/status/{1}";

            // 日付が新しい順に入っているはずなのでリバース
            foreach (var key in dict_date.Keys.Reverse())
            {
                output(file_path, string.Format("# {0}", key));

                // 同じ日付でも新しい順になっているので逆順にする
                dict_date[key].Reverse();
                foreach (var tweet in dict_date[key])
                {
                    var ScreenName = screen_name;
                    long Rt_Id = 0;
                    var Text = tweet.Text;
                    if (tweet.RetweetedStatus == null)
                    {
                        Console.WriteLine("---------");
                        Console.WriteLine("RetweetedStatus Is NULL");

                        // 自分がRTしてコメントしたものを出力したいのでそれ以外を除外
                        if(tweet.QuotedStatus == null){
                            Console.WriteLine("---------");
                            Console.WriteLine("QuotedStatus Is NULL");
                            continue;
                        }
                        Rt_Id = tweet.Id;
                    }
                    else
                    {
                        // RTしたものの情報を取得
                        ScreenName = tweet.RetweetedStatus.User.ScreenName;
                        Rt_Id = tweet.RetweetedStatus.Id;
                        Text = tweet.RetweetedStatus.Text;
                    }

                    Console.WriteLine(tweet.CreatedAt.LocalDateTime);
                    File.AppendAllText(file_path, Text + Environment.NewLine);
                    output(file_path, string.Format(url_temp, ScreenName, Rt_Id));
                }
            }
            Console.Write("");
        }

        static void output(string file_path, string text)
        {
            Console.WriteLine(text);
            File.AppendAllText(file_path, text + Environment.NewLine + Environment.NewLine);
        }
    }
}
