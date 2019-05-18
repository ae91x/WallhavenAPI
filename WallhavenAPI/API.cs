using System;
using System.Collections.Generic;
using System.Net;
using WallhavenAPI.Helpers;
using System.Text.RegularExpressions;

namespace WallhavenAPI
{
    public class API
    {
        private static CookieAwareWebClient _webClient = new CookieAwareWebClient();
        private static Regex WallpaperIdRegex = new Regex("(?<=data-wallpaper-id=\").*?(?=\")");
        private static string[] AlternativeExtensions = new string[] { "png", "jpeg" };

        private const string COOKIE_URI = "https://alpha.wallhaven.cc";
        private const string LOGIN_URI = "https://alpha.wallhaven.cc/auth/login";
        private const string SEARCH_URI = "https://alpha.wallhaven.cc/search";
        private const string FULL_IMAGE_URI_TEMPLATE = "https://wallpapers.wallhaven.cc/wallpapers/full/wallhaven-{0}.jpg";

        public static bool IsLoggedIn()
        {
            foreach (Cookie cookie in _webClient.CookieContainer.GetCookies(new Uri(COOKIE_URI)))
            {
                if (cookie.Name.StartsWith("remember_") && !cookie.Expired)
                    return true;
            }

            return false;
        }

        public static bool Login(string username, string password)
        {
            _webClient.Login(LOGIN_URI, new System.Collections.Specialized.NameValueCollection() { { "username", username }, { "password", password } });

            return IsLoggedIn();
        }

        public static List<string> Search(Query query)
        {   
            _webClient.QueryString.Add("page", query.Page.ToString());
            _webClient.QueryString.Add("categories", query.Categories.ToBitString());
            _webClient.QueryString.Add("purity", query.Purities.ToBitString());
            _webClient.QueryString.Add("sorting", query.Sort.ToString().ToLowerInvariant());
            _webClient.QueryString.Add("order", query.Order.ToString().ToLowerInvariant());

            if (!String.IsNullOrEmpty(query.Keyword))
                _webClient.QueryString.Add("q", query.Keyword);

            if (query.Colors?.Length > 0)
                _webClient.QueryString.Add("colors", String.Join(",", query.Colors));

            if (query.Ratios?.Length > 0)
                _webClient.QueryString.Add("ratios", String.Join(",", query.Ratios));

            if (query.Resolutions?.Length > 0)
            {
                if (query.ExactResolution)
                    _webClient.QueryString.Add("resolutions", String.Join(",", query.Resolutions));
                else
                    _webClient.QueryString.Add("atleast", query.Resolutions[0]);
            }

            var result = _webClient.DownloadString(SEARCH_URI);
            _webClient.QueryString.Clear();

            var matches = WallpaperIdRegex.Matches(result);

            var retval = new List<string>();

            foreach (var match in matches)
                retval.Add(String.Format(FULL_IMAGE_URI_TEMPLATE, match));

            return retval;
        }

        public static byte[] GetFile(string url)
        {
            byte[] data = null;
            int alternativeExtensionsIx = 0;

            while(data == null)
            {
                try
                {
                    data = _webClient.DownloadData(url);
                }
                catch(WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null && response.StatusCode == HttpStatusCode.NotFound && alternativeExtensionsIx < AlternativeExtensions.Length)
                        {
                            url = url.Substring(0, url.Length - (System.IO.Path.GetExtension(url).Length - 1)) + AlternativeExtensions[alternativeExtensionsIx++];
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return data;
        }
    }
}