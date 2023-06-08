using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto
{
    public class ReceiveDto
    {
        public int ReceivedStatusId { get; set; }
        public DateTime ArivalDate { get; set; }
        public Condition Condition { get; set; }

        public string PersonName { get; set; }
    }
}
