using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class Item
    {
        int _itemID;
        List<Color> _colors;
        ItemType _itemType;
        DateTime _date;
        String _location;
        String _description;
    }
}
