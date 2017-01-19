using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    class DataType
    {
        public static Dictionary<string, ItemType> English2EnglishTypes = new Dictionary<string, ItemType>(){{ "ID" , ItemType.ID }, { "WALLET", ItemType.WALLET },
                { "PCMOUSE", ItemType.PCMOUSE }, { "PC", ItemType.PC }, { "PHONE", ItemType.PHONE }, { "KEYS", ItemType.KEYS }, { "BAG", ItemType.BAG }, { "UMBRELLA", ItemType.UMBRELLA },
                { "SWEATSHIRT", ItemType.SWEATSHIRT }, { "GLASSES", ItemType.GLASSES }, { "SHOES", ItemType.SHOES },{ "FLIPFLOPS", ItemType.FLIPFLOPS },
                { "FOLDER", ItemType.FOLDER }, { "CHARGER", ItemType.CHARGER }, { "EARING", ItemType.EARING }, { "RING", ItemType.RING },
                { "NECKLACE", ItemType.NECKLACE }, { "BRACELET", ItemType.BRACELET }, { "HEADPHONES", ItemType.HEADPHONES }};
        public static Dictionary<string, ItemType> Hebrew2EnglishTypes = new Dictionary<string, ItemType>(){{ "תעוד" , ItemType.ID },{ "תז" , ItemType.ID },{ "ת\"ז" , ItemType.ID }, { "ארנק", ItemType.WALLET },
                { "עכבר", ItemType.PCMOUSE }, { "מחשב", ItemType.PC }, { "פון", ItemType.PHONE }, { "מפתח", ItemType.KEYS }, { "תיק", ItemType.BAG }, { "מטרי", ItemType.UMBRELLA },
                { "סווטשרט", ItemType.SWEATSHIRT },{ "סווצרט", ItemType.SWEATSHIRT }, { "משקפ", ItemType.GLASSES }, { "נעל", ItemType.SHOES },{ "כפכ", ItemType.FLIPFLOPS },
                { "תיקיה", ItemType.FOLDER },{ "מחברת", ItemType.FOLDER },{ "קלסר", ItemType.FOLDER }, { "מטען", ItemType.CHARGER }, { "עגיל", ItemType.EARING }, { "טבעת", ItemType.RING },
                { "שרשרת", ItemType.NECKLACE },{ "תליון", ItemType.NECKLACE }, { "צמיד", ItemType.BRACELET }, { "אוזני", ItemType.HEADPHONES }};
        public static Dictionary<string, FBType> FBTypes = new Dictionary<string, FBType>() { { "FOUND", FBType.FOUND }, { "LOST", FBType.LOST } };
        public static Dictionary<string, MatchStatus> status = new Dictionary<string, MatchStatus>() { { "POSSIBLE", MatchStatus.POSSIBLE }, { "CORRECT", MatchStatus.CORRECT }, { "COMPLETE", MatchStatus.COMPLETE }, { "INCORRECT", MatchStatus.INCORRECT } };
        public static Dictionary<string, Color> EnglishColors = new Dictionary<string, Color>(){{ "PINK" , Color.PINK }, { "BLACK", Color.BLACK }, { "BLUE", Color.BLUE }, { "RED", Color.RED },
                { "GREEN", Color.GREEN }, { "YELLOW", Color.YELLOW }, { "WHITE", Color.WHITE },{ "PURPEL", Color.PURPEL }, { "ORANGE", Color.ORANGE },
                { "GRAY", Color.GRAY }, { "BROWN", Color.BROWN }, { "GOLD", Color.GOLD }, { "SILVER", Color.SILVER }};
        public static Dictionary<string, Color> HebColors = new Dictionary<string, Color>(){{ "ורוד" , Color.PINK }, { "שחור", Color.BLACK }, { "כחול", Color.BLUE }, { "אדום", Color.RED }, { "אדומ", Color.RED },
                { "ירוק", Color.GREEN }, { "צהוב", Color.YELLOW }, { "לבן", Color.WHITE }, { "לבנ", Color.WHITE }, { "סגול", Color.PURPEL }, { "כתום", Color.ORANGE }, { "כתומ", Color.ORANGE },
                { "אפור", Color.GRAY }, { "חום", Color.BROWN }, { "חומ", Color.BROWN } , { "זהב", Color.GOLD }, { "זהוב", Color.GOLD }, { "כסף", Color.SILVER }, { "כסוף", Color.SILVER } };
        public static Dictionary<string, FBType> HebTypes = new Dictionary<string, FBType>(){{ "אבד" , FBType.LOST },{ "איבד" , FBType.LOST },{ "איבוד" , FBType.LOST }, { "נעלם", FBType.LOST },
                { "מישהו מצא", FBType.LOST }, { "מישהו במקרה מצא", FBType.LOST }, { "מצאת", FBType.FOUND }, { "מצאנ", FBType.FOUND }, { "נמצא", FBType.FOUND }, { "נימצא", FBType.FOUND }};
    }
}
