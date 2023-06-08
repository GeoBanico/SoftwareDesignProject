using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatusDto
{
    public class StoreDto
    {
        //StoreBought Class
        public int StoreBoughtId { get; set; }  //
        public float Total { get; set; }        //
        public int Quantity { get; set; }
        public DateTime DateBought { get; set; }

        //StoreCity Class
        public int StoreCityId { get; set; }
        public string CityName { get; set; }

        //Store Class
        public int StoreId { get; set; }
        public string StoreName { get; set; }   //
    }
}
