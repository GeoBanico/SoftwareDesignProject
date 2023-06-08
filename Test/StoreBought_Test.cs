using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SoftwareDesign_Project_QueenAnneCuriosityShop;

namespace Test
{
    public class StoreBought_Test
    {
        [Test]
        public void TryFillDirectSupplier_BoughtStatus()
        {
            //arrange
            using var context = new CuriosityContext();

            //act

            var dSBoughtStatus = new SoftwareDesign_Project_QueenAnneCuriosityShop.BoughtStatus
            {
                Status = BuyStatus.StoreBought
            };
            context.BoughtStatuses.Add(dSBoughtStatus);
            context.SaveChanges();


            Action act = () => context.SaveChanges();

            // assert
            act.Should().NotThrow<DbUpdateException>();
        }
    }
}
