using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class Item
    {
        private int _itemID;
        private List<Color> _colors;
        private ItemType _itemType;
        private DateTime _date;
        private String _location;
        private String _description;
    }
}
