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
            List<FBItem> list =  MatchManager.getInstance.getPostsFromGroup("EAACEdEose0cBAF4oCerYDZA1f7hPgkAkCZApnlwG6NGmUMSAt3WZCw3hkNUGxyp2PwKUDQO1K46GNUyP7REFx1sbJgPyw5DV9kyV2VOBZBIQ2c1EsRe4EZB1w5ZCqIMhkbPLqjVFkYlW4qiqcXvssZBR6FvDV9dhiso69CpGsfRAgZDZD",
                "1538105046204967");
//            int i = 0;
        }
    }
}