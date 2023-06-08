using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto;
using UI_QueenAnnes.Annotations;
using Condition = SoftwareDesign_Project_QueenAnneCuriosityShop.Condition;
using Exception = System.Exception;

namespace UI_QueenAnnes.Shipment
{
    /// <summary>
    /// Interaction logic for ShipmentReceived.xaml
    /// </summary>
    public partial class ShipmentReceived : Window
    {
        private ShipmentReceiveBinding _binding = new ShipmentReceiveBinding();

        private int ShipmentId { get; set; }
        private int UnitId { get; set; }
        public ShipmentReceived(int unitId, int shipId)
        {
            InitializeComponent();

            _binding.FillFields(unitId, shipId);
            DataContext = _binding;
            ShipmentId = shipId;
            UnitId = unitId;
        }
        
        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields()) return;

            var condition = Condition.Damaged;
            if (CmbNewCondition.SelectedItem.ToString() == "Good") condition = Condition.Good;
            _binding.AddNewReceive(DpNewArrive.SelectedDate.Value, condition, CmbNewEmployee.SelectedItem.ToString()
                , ShipmentId, UnitId);

            _binding.FillFields(UnitId, ShipmentId);
            DataContext = null;
            DataContext = _binding;

            GrdAddReceive.Visibility = Visibility.Collapsed;
            BtnAdd.Visibility = Visibility.Collapsed;
        }

        private bool CheckFields()
        {
            try
            {
                if(string.IsNullOrEmpty(DpNewArrive.SelectedDate.ToString())) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            return true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            GrdAddReceive.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var receiveStatus = "Item has not received yet:" +
                                "\n Please click add if the item is received" +
                                "\n or the shipment has arrived";
            if (_binding.ReceiveStatus == receiveStatus)
            {
                BtnAdd.Visibility = Visibility.Visible;
                LblNoReceiveStatus.Height = 80;
            }
        }
    }

    public class ShipmentReceiveBinding :INotifyPropertyChanged
    {
        public List<string> EmployeeList { get; set; } = new List<string>();
        public string SelectedEmployee { get; set; }

        public string[] ConditionList { get; set; } = new []{Condition.Damaged.ToString(), Condition.Good.ToString()};
        public string SelectedCondition { get; set; } = Condition.Good.ToString();

        public string ShipmentId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string ReceiveStatus { get; set; }

        public ReceiveDto SelectedReceive { get; set; }

        public void FillFields(int unitId, int shipId)
        {
            using var context = new CuriosityContext();

            //Add unit details
            var selectedUnit = context.Units
                .Include(a => a.CategoryLink)
                .First(a => a.UnitId == unitId);

            UnitId = selectedUnit.UnitId.ToString();
            UnitName = selectedUnit.CategoryLink.CategoryName;

            //Fill cmb Employee
            var employeeContext = context.Persons
                .Include(a => a.EmployeeLink)
                .Where(a => a.IsDeleted == false && a.AccessType != AccessType.Customer);

            foreach (var employee in employeeContext)
            {
                EmployeeList.Add($"{employee.FirstName} {employee.LastName}");
            }

            SelectedEmployee = EmployeeList[0];

            //Add Shipment details and Receive details
            var selectedShip = context.ReceivedStatuses
                .FirstOrDefault(a => a.ShipmentId == shipId);

            ShipmentId = shipId.ToString();

            if (selectedShip == null)
                ReceiveStatus = "Item has not received yet:" +
                                "\n Please click add if the item is received" +
                                "\n or the shipment has arrived";
            else
            {
                ReceiveStatus = "Item received";

                var receiveContext = context.ReceivedStatuses
                    .Include(a => a.EmployeeLink)
                    .ThenInclude(a => a.PersonLink)
                    .First(a => a.ShipmentId == shipId);

                SelectedReceive = new ReceiveDto()
                {
                    ArivalDate = receiveContext.ArivalDate,
                    Condition = receiveContext.Condition,
                    PersonName = $"{receiveContext.EmployeeLink.PersonLink.FirstName} {receiveContext.EmployeeLink.PersonLink.LastName}",
                };
            }
        }

        public void AddNewReceive(DateTime arrDate, Condition condition, string pId, int sId, int uId)
        {
            using var context = new CuriosityContext();

            var personContext = context.Employees
                .Include(a => a.PersonLink)
                .First(a => pId.Contains(a.PersonLink.FirstName) && pId.Contains(a.PersonLink.LastName));

            var shipmentContext = context.Shipments
                .Find(sId);

            var unitContext = context.Units
                .Include(a => a.CategoryLink)
                .First(a => a.UnitId == uId);

            unitContext.UnitQuantity += shipmentContext.Quantity;

            var newReceive = new ReceivedStatus()
            {
                ArivalDate = arrDate,
                Condition = condition,

                EmployeeLink = personContext,
                EmployeeId = personContext.EmployeeId,

                ShipmentLink = shipmentContext,
                ShipmentId = shipmentContext.ShipmentId,

                IsDeleted = false
            };

            context.ReceivedStatuses.Add(newReceive);
            context.SaveChanges();

            MessageBox.Show($"Received: \n*Item: {unitContext.CategoryLink.CategoryName}" +
                            $"\n*Quantity: {shipmentContext.Quantity}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
