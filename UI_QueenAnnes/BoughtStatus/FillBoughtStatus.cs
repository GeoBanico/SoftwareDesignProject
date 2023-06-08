using System;
using System.Collections.Generic;
using System.Text;
using SoftwareDesign_Project_QueenAnneCuriosityShop;

namespace UI_QueenAnnes.BoughtStatus
{
    public class FillBoughtStatus
    {
        public void FillValues()
        {
            using var context = new CuriosityContext();

            var dSBoughtStatus = new SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatus
            {
                Status = BuyStatus.DirectSupplier
            };
            context.BoughtStatuses.Add(dSBoughtStatus);

            var onlineBoughtStatus = new SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatus
            {
                Status = BuyStatus.Online
            };
            context.BoughtStatuses.Add(onlineBoughtStatus);

            var storeBoughtStatus = new SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatus
            {
                Status = BuyStatus.StoreBought
            };
            context.BoughtStatuses.Add(storeBoughtStatus);

            context.SaveChanges();
        }
    }
}
