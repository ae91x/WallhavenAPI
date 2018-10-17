using System;
using System.Collections.Generic;
using System.Text;

namespace WallhavenAPI
{
    #region Enums

    [Flags]
    public enum Category : byte
    {
        People = 1
        , Anime = 1 << 1
        , General = 1 << 2
    }

    [Flags]
    public enum Purity : byte
    {
        NSFW = 1
        , Sketchy = 1 << 1
        , SFW = 1 << 2
    }

    public enum SortMethod : byte
    {
        Relevance
        , Random
        , Date_Added
        , Views
        , Favorites
        , Toplist
    }

    public enum SortOrder : byte
    {
        Asc
        , Desc
    }

    #endregion

    public static class Options
    {
        #region Data

        public static string[] AvailableRatios = new string[] {
            "4x3"
            , "5x4"
            , "16x9"
            , "16x10"
            , "21x9"
            , "32x9"
            , "48x9"
            , "9x16"
            , "10x16"
        };

        public static string[] AvailableRanges = new string[] {
            "1d"
            , "3d"
            , "1w"
            , "1M"
            , "3M"
            , "6M"
            , "1Y"
        };

        public static string[] AvailableColors = new string[] {
            "660000"
            , "990000"
            , "cc0000"
            , "cc3333"
            , "ea4c88"
            , "993399"
            , "663399"
            , "333399"
            , "0066cc"
            , "0099cc"
            , "66cccc"
            , "77cc33"
            , "669900"
            , "336600"
            , "666600"
            , "999900"
            , "cccc33"
            , "ffff00"
            , "ffcc33"
            , "ff9900"
            , "ff6600"
            , "cc6633"
            , "996633"
            , "663300"
            , "000000"
            , "999999"
            , "cccccc"
            , "ffffff"
            , "424153"
        };

        #endregion
    }
}
