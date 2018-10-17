using System;
using System.Collections.Generic;
using System.Text;

namespace WallhavenAPI
{
    public class Query
    {
        public string Keyword;
        public int Page;

        public SortMethod Sort;
        public SortOrder Order;
        public Category Categories;
        public Purity Purities;

        public string[] Colors;
        public string[] Ratios;
        public string[] Resolutions;
        public bool ExactResolution;

        public Query()
        {
            Page = 1;
            Sort = SortMethod.Relevance;
            Order = SortOrder.Desc;
            Categories = Category.Anime | Category.People | Category.General;
            Purities = Purity.SFW;
        }
    }
}
