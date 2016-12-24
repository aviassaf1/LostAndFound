using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class FBItem : Item
    {
        private String _postUrl;
        private String _publisherName;
        private FBType _type;
    }
}
