using System;
using System.Collections.Generic;
using System.Net;
using WallhavenAPI.Structs;
using WallhavenAPI.Helpers;
using System.Xml;
using System.Text.RegularExpressions;

namespace WallhavenAPI
{
    public class API
    {
        private static CookieAwareWebClient _webClient = new CookieAwareWebClient();
        private static Regex WallpaperIdRegex = new Regex("(?<=data-wallpaper-id=\").*?(?=\")");

        private const string SEARCH_URI = "https://alpha.wallhaven.cc/search";
        private const string FULL_IMAGE_BASE_URI = "https://wallpapers.wallhaven.cc/wallpapers/full/wallhaven-";

        public static void Login(string username, string password)
        {
            _webClient.Login("https://alpha.wallhaven.cc/auth/login", new System.Collections.Specialized.NameValueCollection() { { "username", username }, { "password", password } });
        }

        public static List<string> Search(Query query)
        {
            _webClient.BaseAddress = SEARCH_URI;

            _webClient.QueryString.Add("page", query.Page.ToString());
            _webClient.QueryString.Add("categories", query.Categories.ToBitString());
            _webClient.QueryString.Add("purity", query.Purities.ToBitString());
            _webClient.QueryString.Add("sorting", query.Sort.ToString().ToLowerInvariant());
            _webClient.QueryString.Add("order", query.Order.ToString().ToLowerInvariant());

            if (!String.IsNullOrEmpty(query.Keyword))
                _webClient.QueryString.Add("q", query.Keyword);

            if (query.Colors?.Length > 0)
                _webClient.QueryString.Add("colors", String.Join(",", query.Colors));

            if (!String.IsNullOrEmpty(query.Ratio))
                _webClient.QueryString.Add("ratios", query.Ratio);

            if (query.Resolutions?.Length > 0)
            {
                if (query.ExactResolution)
                    _webClient.QueryString.Add("resolutions", String.Join(",", query.Resolutions));
                else
                    _webClient.QueryString.Add("atleast", query.Resolutions[0]);
            }

            var result = _webClient.DownloadString(String.Empty);

            var matches = WallpaperIdRegex.Matches(result);

            var retval = new List<string>();

            foreach (var match in matches)
                retval.Add(FULL_IMAGE_BASE_URI + match.ToString() + ".jpg");

            return retval;
        }
    }
}
