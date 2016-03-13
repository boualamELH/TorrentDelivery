using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Basics.API
{
    class HtmlUtilities
    {
        public void GetList()
        {
            string response = "<div><i>span</i> text <div><a href=\"/mok\">click here fucker!</a></div><a href=\"/404\"/></div>";
            //Make get request

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);
            foreach(HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                Console.WriteLine(link.Attributes[0].Value);
            }
            throw new NotImplementedException();
        }
        static int Main(string[] args)
        {
            new HtmlUtilities().GetList();
            return 0;
        }
    }
}
