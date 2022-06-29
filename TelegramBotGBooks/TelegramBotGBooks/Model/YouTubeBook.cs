using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotGBooks.Model
{
    public class YouTubeBook
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public pageInfo pageInfo { get; set; }

        public List<item> items { get; set; }

    }
    public class pageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }

    }
    public class item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public id id { get; set; }
        public snippet snippet { get; set; }
    }
    public class id
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }
    public class snippet
    {
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string channelTitle { get; set; }
        public string liveBroadcastContent { get; set; }
        public string publishTime { get; set; }
    }

}
