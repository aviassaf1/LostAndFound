using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    class FBItem : Item
    {

        private String _postUrl;
        private String _publisherName;
        private FBType _type;

        public FBItem(int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description,
            String postUrl, String publisherName, FBType fbType)
        {
            _itemID = itemID;
            _colors = colors;
            _itemType = itemType;
            _date = date;
            _location = location;
            _description = description;
            _postUrl = postUrl;
            _publisherName = publisherName;
            _type = fbType;
        }

        public string PostUrl
        {
            get
            {
                return _postUrl;
            }
        }

        public string PublisherName
        {
            get
            {
                return _publisherName;
            }
        }

        internal FBType Type
        {
            get
            {
                return _type;
            }
        }

        protected override void updateItem()
        {
            cache.updateFacebbokItem(this);
        }

        public void addToDB()
        {
            cache.addNewFBItemToDB(this);
        }
    }
}
