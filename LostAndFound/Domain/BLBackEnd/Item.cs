using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    public abstract class Item
    {
        protected static Cache cache = Cache.getInstance;
        protected int _itemID;
        protected List<Color> _colors;
        protected ItemType _itemType;
        protected DateTime _date;
        protected String _location;
        protected String _description;

        protected abstract void updateItem();

        public int ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                this._itemID = value;
            }
        }

        public List<Color> Colors
        {
            get
            {
                return _colors;
            }
        }
        public void addColor(Color color)
        {
            if (!_colors.Contains(color))
            {
                _colors.Add(color);
                this.updateItem();
            }
        }
        public void romoveColor(Color color)
        {
            if (_colors.Contains(color))
            {
                _colors.Remove(color);
                this.updateItem();
            }
        }

        public ItemType ItemType
        {
            get
            {
                return _itemType;
            }

            set
            {
                _itemType = value;
                this.updateItem();
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
                this.updateItem();
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                this.updateItem();
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                this.updateItem();
            }
        }
        public List<string> getColorsList()
        {
            List<string> colors = new List<string>();
            foreach(Color color in _colors)
            {
                colors.Add(color.ToString());
            }
            return colors;
        }
    }
}
