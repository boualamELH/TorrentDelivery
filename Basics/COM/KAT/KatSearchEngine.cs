using System;
using System.Collections.Generic;
using System.Web;

namespace Basics.COM.KAT
{
    public class KatSearchEngine : ISearchEngine
    {
        public List<SearchResult> Search(string query="Captain america civil war")
        {
            var html = GetHtmlResult();
            var result = ConvertToSearchResult(html);
            return null;
        }
    }
}
