using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto
{
    public class PersonItemDto : PersonsDto
    {
        public int UnitId { get; set; }
        public string CategoryName { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
