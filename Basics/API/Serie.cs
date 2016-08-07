using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.API
{
    public class Serie
    {
        public string Id { get; set; }
        public string Network { get; set; }
        public string SeriesName { get; set; }
        public string Status { get; set; }
        public string Overview { get; set; }
        public List<string> Aliases{ get; set; }
        public string Banner { get; set; }
        public string FirstAired { get; set; }
    }
}
