using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatusDto
{
    public class OnlineDto
    {
        //Online Class
        public int OnlineId { get; set; } //List
        public DateTime DateBought { get; set; }
        public DateTime ArrivalDate { get; set; }
        public float Total { get; set; } //List
        public int Quantity { get; set; }

        //Online Store
        public int OnlineStoreId { get; set; }
        public string StoreName { get; set; } //List
    }
}
