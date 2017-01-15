using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    class LostItem : CompanyItem
    {
        private bool _wasFound;

        public LostItem(int itemID, List<Color> colors, ItemType itemType, DateTime date, String location, String description,
        int serialNumber, String companyName, String contactName, String contactPhone, String photoLocation)
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
            _contactPhone = ContactPhone;
            _photoLocation = photoLocation;
            _wasFound = false;
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
            _contactPhone = ContactPhone;
            _photoLocation = photoLocation;
            _wasFound = false;
        }

        /* public void addToDB()
         {
             cache.addLostItem(this);
         }*/


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
            cache.updateLostItem(this);
        }
    }
}
