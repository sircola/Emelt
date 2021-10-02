using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZTF2Zh1
{
    class InvalidYearNumberException:Exception
    {
        public string Id { get; set; }
        public InvalidYearNumberException(StarFighter starFighter) :base("Invalid year number")
        {
            Id = starFighter.ID;
        }
    }
}
