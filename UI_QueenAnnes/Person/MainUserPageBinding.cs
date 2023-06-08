using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.PersonDto;
using UI_QueenAnnes.Annotations;

namespace UI_QueenAnnes.Person
{
    public class MainUserPageBinding : INotifyPropertyChanged
    {
        private SalesDto _selectedSales;
        public string Name { get; set; }
        public string PersonAccessType { get; set; }

        public ObservableCollection<SalesDto> SalesList { get;} = new ObservableCollection<SalesDto>();

        public SalesDto SelectedSales
        {
            get => _selectedSales;
            set
            {
                _selectedSales = value;
                OnPropertyChanged(nameof(SelectedSales));
            } 
        }

        private int PersonId { get; set; }

        public void FillFields()
        {
            SalesList.Clear();

            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Include(a => a.SaleItemList)
                .ThenInclude(a => a.UnitLink)
                .ThenInclude(a => a.CategoryLink)
                .Where(a => a.PersonId == PersonId)
                .ToList();

            foreach (var sale in personContext[0].SaleItemList)
            {
                var slist = new SalesDto()
                {
                    SaleDate = sale.SaleDate,
                    SaleQuantity = sale.SaleQuantity,
                    Total = sale.Total,
                    CategoryName = sale.UnitLink.CategoryLink.CategoryName,
                    UnitPrice = sale.UnitLink.UnitPrice,
                    UnitInfo = sale.UnitLink.UnitDescription
                };

                SalesList.Add(slist);
            }
        }

        public void SetPerson(int personID)
        {
            PersonId = personID;

            using var context = new CuriosityContext();
            var personContext = context.Persons
                .Include(a => a.SaleItemList)
                .ThenInclude(a => a.UnitLink)
                .ThenInclude(a => a.CategoryLink)
                .Where(a => a.PersonId == PersonId)
                .ToList();

            Name = $"{personContext[0].LastName}, {personContext[0].FirstName}";
            PersonAccessType = personContext[0].AccessType.ToString();

            FillFields();
        }

        public void SearchPerson(string search)
        {
            using var context = new CuriosityContext();
            var personContext = context.SaleUnits
                .Include(a => a.PersonLink)
                .Include(a => a.UnitLink)
                .ThenInclude(a => a.CategoryLink)
                .Where(a => a.PersonId == PersonId && a.UnitLink.CategoryLink.CategoryName.Contains(search))
                .ToList();

            SalesList.Clear();

            foreach (var saleUnit in personContext)
            {
                var slist = new SalesDto()
                {
                    SaleDate = saleUnit.SaleDate,
                    SaleQuantity = saleUnit.SaleQuantity,
                    Total = saleUnit.Total,
                    CategoryName = saleUnit.UnitLink.CategoryLink.CategoryName,
                    UnitPrice = saleUnit.UnitLink.UnitPrice,
                    UnitInfo = saleUnit.UnitLink.UnitDescription
                };
                SalesList.Add(slist);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
