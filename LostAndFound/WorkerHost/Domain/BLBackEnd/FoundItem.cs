using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.BLBackEnd
{
    public class FoundItem : CompanyItem
    {
        private bool _delivered;


        public FoundItem(int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description,
        int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation, bool delivered)
        {
            _itemID = itemID;
            _colors = colors;
            _itemType = itemType;
            _date = date;
            _location = location;
            _description = description;
            _serialNumber = serialNumber;
            _companyName = companyName;
            _contactName = contactName;
            _contactPhone = contactPhone;
            _photoLocation = photoLocation;
            _delivered = delivered;
        }
        public FoundItem( List<Color> colors, ItemType itemType, DateTime date, String location, String description,
        int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation)
        {
            _itemID = -1;
            _colors = colors;
            _itemType = itemType;
            _date = date;
            _location = location;
            _description = description;
            _serialNumber = serialNumber;
            _companyName = companyName;
            _contactName = contactName;
            _contactPhone = contactPhone;
            _photoLocation = photoLocation;
            _delivered = false;
        }

        public override void addToDB()
        {
            if (ItemID == -1)
            {
                Cache.getInstance.addFoundItem(this);
            }
        }

        public bool Delivered
        {
            get
            {
                return _delivered;
            }

            set
            {
                _delivered = value;
                updateItem();
            }
        }

        protected override void updateItem()
        {
            Cache.getInstance.updateFoundItem(this);
        }
    }
}
