using System.Collections.Generic;

namespace Basics.COM
{
    public interface ISearchEngine
    {
        List<SearchResult> Search(string query);
    }
}