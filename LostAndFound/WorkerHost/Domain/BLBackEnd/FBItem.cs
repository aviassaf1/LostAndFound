using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.BLBackEnd
{
    public class FBItem : Item
    {

        private String _postID;
        private String _publisherName;
        private FBType _type;

        public FBItem(int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description,
            String postID, String publisherName, FBType fbType)
        {
            _itemID = itemID;
            _colors = colors;
            _itemType = itemType;
            _date = date;
            _location = location;
            _description = description;
            _postID = postID;
            _publisherName = publisherName;
            _type = fbType;
        }

        public FBItem(List<Color> colors, ItemType itemType, DateTime date, String location, String description,
                    String postID, String publisherName, FBType fbType)
        {
            _itemID = -1;
            _colors = colors;
            _itemType = itemType;
            _date = date;
            _location = location;
            _description = description;
            _postID = postID;
            _publisherName = publisherName;
            _type = fbType;
        }

        public string PostID
        {
            get
            {
                return _postID;
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
            Cache.getInstance.updateFacebbokItem(this);
        }

        public override void addToDB()
        {
            if (ItemID == -1)
            {
                Cache.getInstance.addNewFBItemToDB(this);
            }
        }
    }
}
