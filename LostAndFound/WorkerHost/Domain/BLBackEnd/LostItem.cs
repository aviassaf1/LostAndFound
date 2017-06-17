using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.BLBackEnd
{
    public class LostItem : CompanyItem
    {
        private bool _wasFound;

        public LostItem(int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description,
        int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation, bool wasFound)
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
            _wasFound = wasFound;
        }
        public LostItem(List<Color> colors, ItemType itemType, DateTime date, String location, String description,
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
            _wasFound = false;
        }

        public override void addToDB()
         {
            if (ItemID == -1)
            {
                Cache.getInstance.addLostItem(this);
            }
        }


        public bool WasFound
        {
            get
            {
                return _wasFound;
            }
            set
            {
                _wasFound = value;
                updateItem();
            }
        }

        protected override void updateItem()
        {
            Cache.getInstance.updateLostItem(this);
        }
    }
}
