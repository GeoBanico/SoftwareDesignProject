using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto
{
    public class SalesDto
    {
        //From Category
        public string CategoryName { get; set; }

        //From Unit
        public int UnitId { get; set; }
        public float UnitPrice { get; set; }
        public string UnitInfo { get; set; }

        //Sales
        public int SaleQuantity { get; set; }
        public DateTime SaleDate { get; set; }
        public float Total { get; set; }

    }
}
