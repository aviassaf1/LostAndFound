using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Managers;
using Domain.BLBackEnd;

namespace Domain
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup("EAACEdEose0cBAJyealbA8wUeiDXvIuJdgIzND9730WTkpm2f2FQLkJDXRlMN941Rm5BMgfxUZBeunKU5XEOGL2T5BdmW7ZBDE0o4kTVpmlkT6h3YfH0EXhSFJZBdJzquQmpbsZCxy9K0Xe1T5FqihE2NZAog4CRcfMdT0IEttXwZDZD",
                "1538105046204967");
            List<string> colors = new List<string>() { "PINK" };
            ItemManager.getInstance.addLostItem(colors,"KEYS", DateTime.Today, "here", "desc",56658, "GuyCompany", "noam",
                "0555555555", "location");
            int i = 0;
        }
    }
}