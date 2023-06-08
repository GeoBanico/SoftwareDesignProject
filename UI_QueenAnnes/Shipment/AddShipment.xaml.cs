using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SoftwareDesign_Project_QueenAnneCuriosityShop;
using SoftwareDesign_Project_QueenAnneCuriosityShop.ShippingDto;
using UI_QueenAnnes.Annotations;
using UI_QueenAnnes.BoughtStatus;
using UI_QueenAnnes.Item;
using UI_QueenAnnes.Log_In;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace UI_QueenAnnes.Shipment
{
    /// <summary>
    /// Interaction logic for AddShipment.xaml
    /// </summary>
    public partial class AddShipment : Window
    {
        private string ShipmentId { get; set; } = null;
        private BuyStatus BuyStatusLabel { get; set; }
        private int UnitId { get; set; }

        public AddShipment(BuyStatus bS, int uId) //From Add
        {
            InitializeComponent();
            _binding.FillFields("", "", "", "", "");
            DataContext = _binding;
            BuyStatusLabel = bS;
            UnitId = uId;
        }

        private readonly AddShipmentBinding _binding = new AddShipmentBinding();

        public AddShipment(int sId, BuyStatus bS, int uId) //From Edit
        {
            InitializeComponent();
            _binding.EditFillFields(sId);
            DataContext = _binding;
            ShipmentId = sId.ToString();
            BuyStatusLabel = bS;
            UnitId = uId;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields()) return;
            if (string.IsNullOrEmpty(ShipmentId))
                _binding.AddToSave(float.Parse(TxtShipCost.Text),
                    int.Parse(TxtItemQuan.Text), DateTime.Parse(DpDepartDate.SelectedDate.ToString()),
                    DateTime.Parse(DpArrivalDate.SelectedDate.ToString()), CmBCity.SelectedItem.ToString(),
                    CmBCompany.SelectedItem.ToString(), CmBShipper.SelectedItem.ToString(),
                    CmbInsuranceType.SelectedItem.ToString(), CmbInsurerName.SelectedItem.ToString(), float.Parse(TxtInsuranceValue.Text), DpDateInsured.SelectedDate.Value,BuyStatusLabel, UnitId);

            else
            {
                _binding.EditToSave(float.Parse(TxtShipCost.Text),
                    int.Parse(TxtItemQuan.Text), DateTime.Parse(DpDepartDate.SelectedDate.ToString()),
                    DateTime.Parse(DpArrivalDate.SelectedDate.ToString()), CmBCity.SelectedItem.ToString(),
                    CmBCompany.SelectedItem.ToString(), CmBShipper.SelectedItem.ToString(),
                    CmbInsuranceType.SelectedItem.ToString(), CmbInsurerName.SelectedItem.ToString(), float.Parse(TxtInsuranceValue.Text), DpDateInsured.SelectedDate.Value, BuyStatusLabel, int.Parse(ShipmentId), UnitId);
            }

            Close();
        }

        private bool CheckFields()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtShipCost.Text)) throw new Exception();
                if (string.IsNullOrEmpty(TxtItemQuan.Text)) throw new Exception();
                if (string.IsNullOrEmpty(DpDepartDate.SelectedDate.ToString())) throw new Exception();
                if (string.IsNullOrEmpty(DpArrivalDate.SelectedDate.ToString())) throw new Exception();
                if(string.IsNullOrEmpty(TxtInsuranceValue.Text)) throw new Exception();
                if(string.IsNullOrEmpty(DpDateInsured.SelectedDate.ToString())) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill up the empty fields");
                return false;
            }

            try
            {
                var f = float.Parse(TxtShipCost.Text);
                var x = int.Parse(TxtItemQuan.Text);
                var y = float.Parse(TxtInsuranceValue.Text);

            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter numerical inputs on their corresponding fields");
                return false;
            }

            try
            {
                if (DpDepartDate.SelectedDate > DpArrivalDate.SelectedDate) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Departure Date should be less than the Arrival Date" +
                                "\nPlease Change...");
                return false;
            }

            try
            {
                if(DpDepartDate.SelectedDate > DpDateInsured.SelectedDate || DpArrivalDate.SelectedDate < DpDateInsured.SelectedDate) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Date Insured should be between the departure date and the arrival date");
            }

            return true;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelMainFunctionality()
        {
            BtnAddCity.IsEnabled = false;
            BtnAddShipper.IsEnabled = false;
            BtnAddCompany.IsEnabled = false;
            BtnViewInsuranceType.IsEnabled = false;
            BtnViewInsurer.IsEnabled = false;

            TxtShipCost.IsEnabled = false;
            TxtItemQuan.IsEnabled = false;
            TxtInsuranceValue.IsEnabled = false;

            DpArrivalDate.IsEnabled = false;
            DpDepartDate.IsEnabled = false;
            DpDateInsured.IsEnabled = false;

            CmBCompany.IsEnabled = false;
            CmbInsurerName.IsEnabled = false;
            CmBShipper.IsEnabled = false;
            CmBCity.IsEnabled = false;
            CmbInsuranceType.IsEnabled = false;
        }

        private void ReturnMainFunctionality()
        {
            BtnAddCity.IsEnabled = true;
            BtnAddShipper.IsEnabled = true;
            BtnAddCompany.IsEnabled = true;
            BtnViewInsuranceType.IsEnabled = true;
            BtnViewInsurer.IsEnabled = true;

            TxtShipCost.IsEnabled = true;
            TxtItemQuan.IsEnabled = true;
            TxtInsuranceValue.IsEnabled = true;

            DpArrivalDate.IsEnabled = true;
            DpDepartDate.IsEnabled = true;
            DpDateInsured.IsEnabled = true;

            CmBCompany.IsEnabled = true;
            CmbInsurerName.IsEnabled = true;
            CmBShipper.IsEnabled = true;
            CmBCity.IsEnabled = true;
            CmbInsuranceType.IsEnabled = true;
        }

        private void ReturnChanges(IReadOnlyList<string> holdChanges)
        {
            TxtShipCost.Text = holdChanges[0];
            TxtItemQuan.Text = holdChanges[1];

            if (!string.IsNullOrEmpty(holdChanges[2]))
            {
                DpDepartDate.SelectedDate = DateTime.Parse(holdChanges[2]);
            }

            if (!string.IsNullOrEmpty(holdChanges[3]))
            {
                DpArrivalDate.SelectedDate = DateTime.Parse(holdChanges[3]);
            }

            if (!string.IsNullOrEmpty(holdChanges[4])) CmBCity.SelectedItem = holdChanges[4];
            if (!string.IsNullOrEmpty(holdChanges[5])) CmBCompany.SelectedItem = holdChanges[5];

            if (!string.IsNullOrEmpty(holdChanges[6])) CmBShipper.SelectedItem = holdChanges[6];
            if (!string.IsNullOrEmpty(holdChanges[7])) CmbInsuranceType.SelectedItem = holdChanges[7];

            if (!string.IsNullOrEmpty(holdChanges[8])) CmbInsurerName.SelectedItem = holdChanges[8];

            TxtInsuranceValue.Text = holdChanges[9];

            if(!string.IsNullOrEmpty(holdChanges[10])) DpDateInsured.SelectedDate = DateTime.Parse(holdChanges[10]);
        }

        //CITY FUNCTIONALITY
        private void BtnAddCity_Click(object sender, RoutedEventArgs e)
        {
            ShipmentTabControl.Visibility = Visibility.Visible;
            TabCityDetails.Visibility = Visibility.Visible;
            CityTabItem.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            _binding.LoadCity(CmBCity.SelectedItem.ToString());
        }

        private void BtnShipmentCityAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtShipmentCityName.IsEnabled = true;
            TxtExchangeRate.IsEnabled = true;

            TxtShipmentCityName.Text = "";
            TxtExchangeRate.Text = "";

            HideCityButtons();
        }

        private string oldShipmentCityName { get; set; } = null;
        private string oldShipmentCityER { get; set; } = null;
        private void BtnShipmentCityEdit_Click(object sender, RoutedEventArgs e)
        {
            oldShipmentCityName = TxtShipmentCityName.Text;
            oldShipmentCityER = TxtExchangeRate.Text;

            TxtShipmentCityName.IsEnabled = true;
            TxtExchangeRate.IsEnabled = true;

            HideCityButtons();
        }

        private void BtnShipmentCityDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this store: {TxtShipmentCityName.Text}?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.CityDelete(TxtShipmentCityName.Text, TxtExchangeRate.Text);
                MessageBox.Show("Delete Successful");
            }

            CityReturnToNormal();
        }

        private void BtnShipmentCitySave_Click(object sender, RoutedEventArgs e)
        {
            if (!CityCheckFields()) return;

            if (string.IsNullOrEmpty(oldShipmentCityER) && string.IsNullOrEmpty(oldShipmentCityName)) //Add
            {
                _binding.CityAddToSave(TxtShipmentCityName.Text, float.Parse(TxtExchangeRate.Text));
                CityReturnToNormal();
            }
            else //Edit
            {
                _binding.CityEditToSave(oldShipmentCityName, oldShipmentCityER, TxtShipmentCityName.Text, float.Parse(TxtExchangeRate.Text));
                CityReturnToNormal();
            }
        }

        private void BtnShipmentCityBack_Click(object sender, RoutedEventArgs e)
        {
            CityReturnToNormal();
        }

        private bool CityCheckFields()
        {
            try
            {
                if(string.IsNullOrEmpty(TxtShipmentCityName.Text)) throw new Exception();
                if(string.IsNullOrEmpty(TxtExchangeRate.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            try
            {
                var x = float.Parse(TxtExchangeRate.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter a correct numerical input in th Exchange Rate field");
                return false;
            }

            return true;
        }

        private void CityReturnToNormal()
        {
            _binding.FillFields("", _binding.SelectedVendor, _binding.SelectedShipper, _binding.SelectedInsuranceType, _binding.SelectedInsurer);
            var holdChanges = new string[]
            {
                TxtShipCost.Text, TxtItemQuan.Text,
                DpDepartDate.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString(),
                "", _binding.SelectedVendor, 
                _binding.SelectedShipper, _binding.SelectedInsuranceType, 
                _binding.SelectedInsurer, TxtInsuranceValue.Text,
                DpDateInsured.SelectedDate.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);

            TxtShipmentCityName.IsEnabled = false;
            TxtExchangeRate.IsEnabled = false;

            ReturnCityButtons();
            ReturnMainFunctionality();

            oldShipmentCityER = null;
            oldShipmentCityName = null;
        }

        private void HideCityButtons()
        {
            BtnShipmentCityAdd.Visibility = Visibility.Collapsed;
            BtnShipmentCityEdit.Visibility = Visibility.Collapsed;
            BtnShipmentCityDelete.Visibility = Visibility.Collapsed;

            BtnShipmentCitySave.Visibility = Visibility.Visible;
        }

        private void ReturnCityButtons()
        {
            ShipmentTabControl.Visibility = Visibility.Collapsed;
            TabCityDetails.Visibility = Visibility.Collapsed;
            CityTabItem.Visibility = Visibility.Collapsed;

            BtnShipmentCityAdd.Visibility = Visibility.Visible;
            BtnShipmentCityEdit.Visibility = Visibility.Visible;
            BtnShipmentCityDelete.Visibility = Visibility.Visible;

            BtnShipmentCitySave.Visibility = Visibility.Collapsed;
        }

        //VENDOR FUNCTIONALITY

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SoloSupplier.Visibility = Visibility.Collapsed;
            CompanySupplier.Visibility = Visibility.Visible;
        }

        private void RbIsCompany_Unchecked(object sender, RoutedEventArgs e)
        {
            SoloSupplier.Visibility = Visibility.Visible;
            CompanySupplier.Visibility = Visibility.Collapsed;
        }

        private void BtnAddCompany_Click(object sender, RoutedEventArgs e)
        {
            ShipmentTabControl.Visibility = Visibility.Visible;
            ShipmentTabControl.SelectedIndex = 1;

            TabSupplierDetails.Visibility = Visibility.Visible;
            SupplierTabItem.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            _binding.LoadSupplier(CmBCompany.SelectedItem.ToString());

            RbIsCompany.IsChecked = _binding.SelectedMainVendor.IsCompany;
        }

        private void BtnShipmentVendorAdd_Click(object sender, RoutedEventArgs e)
        {
            RbIsCompany.IsEnabled = true;

            TxtCompanyName.IsEnabled = true;
            TxtCompanyFirstName.IsEnabled = true;
            TxtCompanyLastName.IsEnabled = true;

            TxtCompanyName.Text = "";
            TxtCompanyFirstName.Text = "";
            TxtCompanyLastName.Text = "";

            HideVendorButton();

        }

        private Vendor oldVendor { get; set; } = new Vendor();
        private void BtnShipmentVendorEdit_Click(object sender, RoutedEventArgs e)
        {
            RbIsCompany.IsEnabled = true;
            if (_binding.SelectedMainVendor.IsCompany)
            {
                oldVendor = new Vendor()
                {
                    IsCompany = true,
                    CompanyName = TxtCompanyName.Text
                };

                TxtCompanyName.IsEnabled = true;
                TxtCompanyFirstName.IsEnabled = true;
                TxtCompanyLastName.IsEnabled = true;

                TxtCompanyFirstName.Text = "";
                TxtCompanyLastName.Text = "";
            }
            else
            {
                oldVendor = new Vendor()
                {
                    IsCompany = false,
                    CompanyFName = TxtCompanyFirstName.Text,
                    CompanyLName =  TxtCompanyLastName.Text
                };

                TxtCompanyFirstName.IsEnabled = true;
                TxtCompanyLastName.IsEnabled = true;

                TxtCompanyName.IsEnabled = true;
                TxtCompanyName.Text = "";
            }
            
            HideVendorButton();
        }

        private void BtnShipmentVendorDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete this supplier?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.SupplierDelete((bool)RbIsCompany.IsChecked, TxtCompanyName.Text, TxtCompanyFirstName.Text, TxtCompanyLastName.Text);
                MessageBox.Show("Delete Successful");
            }

            VendorReturnToNormal();
        }

        private void BtnShipmentVendorSave_Click(object sender, RoutedEventArgs e)
        {
            if (!VendorCheckFields()) return;

            if (oldVendor.CompanyName == null && oldVendor.CompanyFName == null) //Add
            {
                _binding.SupplierAddToSave((bool)RbIsCompany.IsChecked, TxtCompanyName.Text, TxtCompanyFirstName.Text, TxtCompanyLastName.Text);
                VendorReturnToNormal();
            }
            else
            {
                _binding.SupplierEditToSave((bool)RbIsCompany.IsChecked, TxtCompanyName.Text, TxtCompanyFirstName.Text, TxtCompanyLastName.Text, oldVendor);
                VendorReturnToNormal();
            }
        }

        private void BtnShipmentVendorBack_Click(object sender, RoutedEventArgs e)
        {
            VendorReturnToNormal();
        }

        private bool VendorCheckFields()
        {
            try
            {
                if (RbIsCompany.IsChecked == true)
                {
                    if(string.IsNullOrEmpty(TxtCompanyName.Text)) throw new Exception();
                }
                else
                {
                    if (string.IsNullOrEmpty(TxtCompanyFirstName.Text)) throw new Exception();
                    if (string.IsNullOrEmpty(TxtCompanyLastName.Text)) throw new Exception();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            return true;
        }

        private void VendorReturnToNormal()
        {
            _binding.FillFields(_binding.SelectedCity, "", _binding.SelectedShipper, _binding.SelectedInsuranceType, _binding.SelectedInsurer);
            var holdChanges = new string[]
            {
                TxtShipCost.Text, TxtItemQuan.Text,
                DpDepartDate.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString(),
                _binding.SelectedCity, "",
                _binding.SelectedShipper, _binding.SelectedInsuranceType,
                _binding.SelectedInsurer, TxtInsuranceValue.Text,
                DpDateInsured.SelectedDate.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);

            RbIsCompany.IsEnabled = false;
            TxtCompanyName.IsEnabled = false;
            TxtCompanyFirstName.IsEnabled = false;
            TxtCompanyLastName.IsEnabled = false;
            
            ReturnVendorButtons();
            ReturnMainFunctionality();

            oldVendor = new Vendor();
        }

        private void HideVendorButton()
        {
            BtnShipmentVendorAdd.Visibility = Visibility.Collapsed;
            BtnShipmentVendorEdit.Visibility = Visibility.Collapsed;
            BtnShipmentVendorDelete.Visibility = Visibility.Collapsed;

            BtnShipmentVendorSave.Visibility = Visibility.Visible;
        }

        private void ReturnVendorButtons()
        {
            ShipmentTabControl.Visibility = Visibility.Collapsed;
            TabSupplierDetails.Visibility = Visibility.Collapsed;
            SupplierTabItem.Visibility = Visibility.Collapsed;

            BtnShipmentVendorAdd.Visibility = Visibility.Visible;
            BtnShipmentVendorEdit.Visibility = Visibility.Visible;
            BtnShipmentVendorDelete.Visibility = Visibility.Visible;

            BtnShipmentVendorSave.Visibility = Visibility.Collapsed;
        }

        //SHIPPER FUNCTIONALITY
        private void BtnAddShipper_Click(object sender, RoutedEventArgs e)
        {
            ShipmentTabControl.Visibility = Visibility.Visible;
            ShipmentTabControl.SelectedIndex = 2;

            TabShipperDetails.Visibility = Visibility.Visible;
            ShipperTabItem.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            _binding.LoadShipper(CmBShipper.SelectedItem.ToString());
        }

        private void BtnShipmentShipperAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtShipperContact.IsEnabled = true;
            TxtShipperName.IsEnabled = true;
            TxtShipperEmail.IsEnabled = true;

            TxtShipperContact.Text = "";
            TxtShipperName.Text = "";
            TxtShipperEmail.Text = "";

            HideShipperButtons();
        }

        private Shipper oldShipper { get; set; } = new Shipper();
        private void BtnShipmentShipperEdit_Click(object sender, RoutedEventArgs e)
        {
            oldShipper.ShipperName = TxtShipperName.Text;
            oldShipper.ShipperContact = TxtShipperContact.Text;
            oldShipper.ShipperEmail = TxtShipperEmail.Text;

            TxtShipperContact.IsEnabled = true;
            TxtShipperName.IsEnabled = true;
            TxtShipperEmail.IsEnabled = true;

            HideShipperButtons();
        }

        private void BtnShipmentShipperDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete shipper {TxtShipperName.Text} ?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.ShipperDelete(CmBShipper.SelectedItem.ToString());
                MessageBox.Show("Delete Successful");
            }

            ShipperReturnToNormal();
        }

        private void BtnShipmentShipperSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ShipperCheckFields()) return;

            if (oldShipper.ShipperName == null)
            {
                _binding.ShipperAddToSave(TxtShipperName.Text, TxtShipperEmail.Text, TxtShipperContact.Text);
                ShipperReturnToNormal();
            }
            else
            {
                _binding.ShipperEditToSave(TxtShipperName.Text, TxtShipperEmail.Text, TxtShipperContact.Text, oldShipper);
                ShipperReturnToNormal();
            }
        }

        private void BtnShipmentShipperBack_Click(object sender, RoutedEventArgs e)
        {
            ShipperReturnToNormal();
        }

        private bool ShipperCheckFields()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtShipperName.Text)) throw new Exception();
                if (string.IsNullOrEmpty(TxtShipperEmail.Text)) throw new Exception();
                if (string.IsNullOrEmpty(TxtShipperContact.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            var checkFields = new Sign_InCheckUp();

            try
            {
                if(!checkFields.CheckEmail(TxtShipperEmail.Text)) throw new Exception();
                if (!checkFields.CheckPhone(TxtShipperContact.Text)) throw new Exception();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void ShipperReturnToNormal()
        {
            _binding.FillFields(_binding.SelectedCity, _binding.SelectedVendor, "", _binding.SelectedInsuranceType, _binding.SelectedInsurer);
            var holdChanges = new string[]
            {
                TxtShipCost.Text, TxtItemQuan.Text,
                DpDepartDate.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString(),
                _binding.SelectedCity, _binding.SelectedVendor,
                "", _binding.SelectedInsuranceType,
                _binding.SelectedInsurer, TxtInsuranceValue.Text,
                DpDateInsured.SelectedDate.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);

            TxtShipperEmail.IsEnabled = false;
            TxtShipperContact.IsEnabled = false;
            TxtShipperName.IsEnabled = false;


            ReturnShipperButtons();
            ReturnMainFunctionality();

            oldShipper = new Shipper();
        }

        private void HideShipperButtons()
        {
            BtnShipmentShipperAdd.Visibility = Visibility.Collapsed;
            BtnShipmentShipperEdit.Visibility = Visibility.Collapsed;
            BtnShipmentShipperDelete.Visibility = Visibility.Collapsed;

            BtnShipmentShipperSave.Visibility = Visibility.Visible;
        }

        private void ReturnShipperButtons()
        {
            ShipmentTabControl.Visibility = Visibility.Collapsed;
            TabShipperDetails.Visibility = Visibility.Collapsed;
            ShipperTabItem.Visibility = Visibility.Collapsed;

            BtnShipmentShipperAdd.Visibility = Visibility.Visible;
            BtnShipmentShipperEdit.Visibility = Visibility.Visible;
            BtnShipmentShipperDelete.Visibility = Visibility.Visible;

            BtnShipmentShipperSave.Visibility = Visibility.Collapsed;
        }

        //INSURANCE TYPE FUNCTIONALITY
        private void BtnViewInsuranceType_Click(object sender, RoutedEventArgs e)
        {
            ShipmentTabControl.Visibility = Visibility.Visible;
            ShipmentTabControl.SelectedIndex = 3;

            TabTypeDetails.Visibility = Visibility.Visible;
            TypeTabItem.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            TxtInsuranceTypeName.Text = _binding.SelectedInsuranceType;
        }

        private void BtnShipmentTypeAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtInsuranceTypeName.IsEnabled = true;
            TxtInsuranceTypeName.Text = "";

            HideTypeButtons();
        }

        private string oldType { get; set; } = null;
        private void BtnShipmentTypeEdit_Click(object sender, RoutedEventArgs e)
        {
            TxtInsuranceTypeName.Text = _binding.SelectedInsuranceType;
            oldType = TxtInsuranceTypeName.Text;

            TxtInsuranceTypeName.IsEnabled = true;
            HideTypeButtons();
        }

        private void BtnShipmentTypeDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete insurance type: {TxtInsuranceTypeName.Text} ?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.TypeDelete(CmbInsuranceType.SelectedItem.ToString());
                MessageBox.Show("Delete Successful");
            }

            TypeReturnToNormal();
        }

        private void BtnShipmentTypeSave_Click(object sender, RoutedEventArgs e)
        {
            if (!TypeCheckFields()) return;

            if (string.IsNullOrEmpty(oldType))//Add
            {
                _binding.TypeAddToSave(TxtInsuranceTypeName.Text);
                TypeReturnToNormal();
            }
            else
            {
                _binding.TypeEditToSave(TxtInsuranceTypeName.Text, oldType);
                TypeReturnToNormal();
            }
        }

        private void BtnShipmentTypeBack_Click(object sender, RoutedEventArgs e)
        {
            TypeReturnToNormal();
        }

        private bool TypeCheckFields()
        {
            try
            {
                if(string.IsNullOrEmpty(TxtInsuranceTypeName.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            return true;
        }

        private void TypeReturnToNormal()
        {
            _binding.FillFields(_binding.SelectedCity, _binding.SelectedVendor, _binding.SelectedShipper, "", _binding.SelectedInsurer);
            var holdChanges = new string[]
            {
                TxtShipCost.Text, TxtItemQuan.Text,
                DpDepartDate.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString(),
                _binding.SelectedCity, _binding.SelectedVendor,
                _binding.SelectedShipper, "",
                _binding.SelectedInsurer, TxtInsuranceValue.Text,
                DpDateInsured.SelectedDate.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);

            TxtInsuranceTypeName.IsEnabled = false;

            ReturnTypeButtons();
            ReturnMainFunctionality();

            oldType = null;
        }

        private void HideTypeButtons()
        {
            BtnShipmentTypeAdd.Visibility = Visibility.Collapsed;
            BtnShipmentTypeEdit.Visibility = Visibility.Collapsed;
            BtnShipmentTypeDelete.Visibility = Visibility.Collapsed;

            BtnShipmentTypeSave.Visibility = Visibility.Visible;
        }

        private void ReturnTypeButtons()
        {
            ShipmentTabControl.Visibility = Visibility.Collapsed;
            TabTypeDetails.Visibility = Visibility.Collapsed;
            TypeTabItem.Visibility = Visibility.Collapsed;

            BtnShipmentTypeAdd.Visibility = Visibility.Visible;
            BtnShipmentTypeEdit.Visibility = Visibility.Visible;
            BtnShipmentTypeDelete.Visibility = Visibility.Visible;

            BtnShipmentTypeSave.Visibility = Visibility.Collapsed;
        }

        //INSURER FUNCTIONALITY
        private void BtnViewInsurer_Click(object sender, RoutedEventArgs e)
        {
            ShipmentTabControl.Visibility = Visibility.Visible;
            ShipmentTabControl.SelectedIndex = 4;

            TabInsurerDetails.Visibility = Visibility.Visible;
            InsurerTabItem.Visibility = Visibility.Visible;

            CancelMainFunctionality();
            TxtInsuranceInsurerName.Text = _binding.SelectedInsurer;
        }

        private void BtnShipmentInsurerAdd_Click(object sender, RoutedEventArgs e)
        {
            TxtInsuranceInsurerName.IsEnabled = true;
            TxtInsuranceInsurerName.Text = "";

            HideInsurerButtons();
        }

        private string oldInsurer { get; set; } = null;
        private void BtnShipmentInsurerEdit_Click(object sender, RoutedEventArgs e)
        {
            TxtInsuranceInsurerName.Text = _binding.SelectedInsurer;
            oldInsurer = TxtInsuranceInsurerName.Text;

            TxtInsuranceInsurerName.IsEnabled = true;
            HideInsurerButtons();
        }

        private void BtnShipmentInsurerDelete_Click(object sender, RoutedEventArgs e)
        {
            var mbresult = MessageBox.Show($"Are you sure you want to delete Insurer: {TxtInsuranceInsurerName.Text} ?", "Alert",
                MessageBoxButton.YesNo);

            if (mbresult == MessageBoxResult.Yes)
            {
                //delete
                _binding.InsurerDelete(CmbInsurerName.SelectedItem.ToString());
                MessageBox.Show("Delete Successful");
            }

            InsurerReturnToNormal();
        }

        private void BtnShipmentInsurerSave_Click(object sender, RoutedEventArgs e)
        {
            if (!InsurerCheckFields()) return;

            if (string.IsNullOrEmpty(oldInsurer))//Add
            {
                _binding.InsurerAddToSave(TxtInsuranceInsurerName.Text);
                InsurerReturnToNormal();
            }
            else
            {
                _binding.InsurerEditToSave(TxtInsuranceInsurerName.Text, oldInsurer);
                InsurerReturnToNormal();
            }
        }

        private void BtnShipmentInsurerBack_Click(object sender, RoutedEventArgs e)
        {
            InsurerReturnToNormal();
        }

        private bool InsurerCheckFields()
        {
            try
            {
                if (string.IsNullOrEmpty(TxtInsuranceInsurerName.Text)) throw new Exception();
            }
            catch (Exception e)
            {
                MessageBox.Show("Please fill-up the empty fields");
                return false;
            }

            return true;
        }

        private void InsurerReturnToNormal()
        {
            _binding.FillFields(_binding.SelectedCity, _binding.SelectedVendor, _binding.SelectedShipper, _binding.SelectedInsuranceType, "");
            var holdChanges = new string[]
            {
                TxtShipCost.Text, TxtItemQuan.Text,
                DpDepartDate.SelectedDate.ToString(), DpArrivalDate.SelectedDate.ToString(),
                _binding.SelectedCity, _binding.SelectedVendor,
                _binding.SelectedShipper, _binding.SelectedInsuranceType,
                "", TxtInsuranceValue.Text,
                DpDateInsured.SelectedDate.ToString()
            };

            DataContext = null;
            DataContext = _binding;

            ReturnChanges(holdChanges);

            TxtInsuranceInsurerName.IsEnabled = false;

            ReturnInsurerButtons();
            ReturnMainFunctionality();

            oldInsurer = null;
        }

        private void HideInsurerButtons()
        {
            BtnShipmentInsurerAdd.Visibility = Visibility.Collapsed;
            BtnShipmentInsurerEdit.Visibility = Visibility.Collapsed;
            BtnShipmentInsurerDelete.Visibility = Visibility.Collapsed;

            BtnShipmentInsurerSave.Visibility = Visibility.Visible;
        }

        private void ReturnInsurerButtons()
        {
            ShipmentTabControl.Visibility = Visibility.Collapsed;
            TabInsurerDetails.Visibility = Visibility.Collapsed;
            InsurerTabItem.Visibility = Visibility.Collapsed;

            BtnShipmentInsurerAdd.Visibility = Visibility.Visible;
            BtnShipmentInsurerEdit.Visibility = Visibility.Visible;
            BtnShipmentInsurerDelete.Visibility = Visibility.Visible;

            BtnShipmentInsurerSave.Visibility = Visibility.Collapsed;
        }
    }

    public class AddShipmentBinding : INotifyPropertyChanged
    {
        private ShipmentDto _selectedShipment;

        private string _selectedCity;
        private string _selectedVendor;
        private string _selectedShipper;
        private string _selectedInsuranceType;
        private string _selectedInsurer;
        private float _insuranceValue;

        private City _selectedMainCity;
        private Vendor _selectedMainVendor;
        private Shipper _selectedMainShipper;

        public ShipmentDto SelectedShipment
        {
            get => _selectedShipment;
            set
            {
                _selectedShipment = value;
                OnPropertyChanged(nameof(SelectedShipment));
            }
        }

        public List<string> CityList { get; set; } = new List<string>();
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public List<string> VendorList { get; set; } = new List<string>();
        public string SelectedVendor
        {
            get => _selectedVendor;
            set
            {
                _selectedVendor = value;
                OnPropertyChanged(nameof(SelectedVendor));
            }
        }

        public List<string> ShipperList { get; set; } = new List<string>();
        public string SelectedShipper
        {
            get => _selectedShipper;
            set
            {
                _selectedShipper = value;
                OnPropertyChanged(nameof(SelectedShipper));
            }
        }

        public List<string> InsuranceTypeList { get; set; } = new List<string>();
        public string SelectedInsuranceType
        {
            get => _selectedInsuranceType;
            set
            {
                _selectedInsuranceType = value;
                OnPropertyChanged(nameof(SelectedInsuranceType));
            }
        }

        public List<string> InsurerList { get; set; } = new List<string>();
        public string SelectedInsurer
        {
            get => _selectedInsurer;
            set
            {
                _selectedInsurer = value;
                OnPropertyChanged(nameof(SelectedInsurer));
            }
        }

        //If opened window
        public void CityCmbRefresh(string city)
        {
            using var context = new CuriosityContext();
            var cityContext = context.Cities
                .Where(a => a.IsDeleted == false)
                .ToList();

            CityList.Clear();
            foreach (var c in cityContext)
            {
                CityList.Add(c.CityName);
            }

            if (CityList.Count > 0) SelectedCity = string.IsNullOrEmpty(city) ? CityList[0] : city;
        }

        public void VendorCmbRefresh(string vendor)
        {
            using var context = new CuriosityContext();
            var vendorContext = context.Vendors
                .Where(a => a.IsDeleted == false)
                .ToList();

            VendorList.Clear();
            foreach (var v in vendorContext)
            {
                VendorList.Add(v.IsCompany ? v.CompanyName : $"{v.CompanyFName} {v.CompanyLName}");
            }

            if (VendorList.Count > 0) SelectedVendor = string.IsNullOrEmpty(vendor) ? VendorList[0] : vendor;
        }

        public void ShipperCmbRefresh(string shipper)
        {
            using var context = new CuriosityContext();
            var shipperContext = context.Shippers
                .Where(a => a.IsDeleted == false)
                .ToList();

            ShipperList.Clear();
            foreach (var s in shipperContext)
            {
                ShipperList.Add(s.ShipperName);
            }

            if (ShipperList.Count > 0) SelectedShipper = string.IsNullOrEmpty(shipper) ? ShipperList[0] : shipper;

        }

        public void TypeCmbRefresh(string type)
        {
            using var context = new CuriosityContext();
            var typeContext = context.InsuranceTypes
                .Where(a => a.IsDeleted == false)
                .ToList();

            InsuranceTypeList.Clear();
            foreach (var insuranceType in typeContext)
            {
                InsuranceTypeList.Add(insuranceType.InsuranceName);
            }

            if (InsuranceTypeList.Count > 0)
                SelectedInsuranceType = string.IsNullOrEmpty(type) ? InsuranceTypeList[0] : type;
        }

        public void InsurerCmbRefresh(string insurer)
        {
            using var context = new CuriosityContext();
            var insurerContext = context.Insurers
                .Where(a => a.IsDeleted == false)
                .ToList();

            InsurerList.Clear();
            foreach (var insurers in insurerContext)
            {
                InsurerList.Add(insurers.InsurerName);
            }

            if (InsurerList.Count > 0) SelectedInsurer = string.IsNullOrEmpty(insurer) ? InsurerList[0] : insurer;
        }

        //For Add
        public void FillFields(string c, string v, string s, string iT, string iser)
        {
            CityCmbRefresh(c);
            VendorCmbRefresh(v);
            ShipperCmbRefresh(s);
            TypeCmbRefresh(iT);
            InsurerCmbRefresh(iser);
        }

        //ForEdit
        public void EditFillFields(int sId)
        {
            using var context = new CuriosityContext();
            var shipmentToEdit = context.Shipments
                .Include(a => a.CityLink)
                .Include(a => a.VendorLink)
                .Include(a => a.ShipperLink)
                .Include(a => a.ShipmentInsuranceLink)
                .ThenInclude(a => a.InsuranceTypeLink)
                .Include(a => a.ShipmentInsuranceLink)
                .ThenInclude(a => a.InsurerLink)
                .First(a => a.ShipmentId == sId);

            //For Shipment
            SelectedShipment = new ShipmentDto()
            {
                ShipmentCost = shipmentToEdit.ShipmentCost,
                DepartureDate = shipmentToEdit.DepartureDate,
                ExpectedArrivalDate = shipmentToEdit.ExpectedArrivalDate,
                Quantity = shipmentToEdit.Quantity,

                CityName = shipmentToEdit.CityLink.CityName, //City
                ShipperName = shipmentToEdit.ShipperLink.ShipperName, //Shipper

                InsuranceName =
                    shipmentToEdit.ShipmentInsuranceLink.InsuranceTypeLink.InsuranceName, //For ShipmentInsurance
                InsurerName = shipmentToEdit.ShipmentInsuranceLink.InsurerLink.InsurerName,

                InsuranceValue = shipmentToEdit.ShipmentInsuranceLink.InsuranceValue,

                DateInsured = shipmentToEdit.ShipmentInsuranceLink.DateInsured
            };

            SelectedShipment.CompanyName = shipmentToEdit.VendorLink.IsCompany //Vendor
                ? shipmentToEdit.VendorLink.CompanyName
                : $"{shipmentToEdit.VendorLink.CompanyFName} {shipmentToEdit.VendorLink.CompanyLName}";


            //Fill Lists
            FillFields(SelectedShipment.CityName, SelectedShipment.CompanyName, SelectedShipment.ShipperName,
                SelectedShipment.InsuranceName, SelectedShipment.InsurerName);
        }

        public void AddToSave(float shipCost, int quantity, DateTime depDate, DateTime arrDate,
            string city, string vendor, string shipper, string type, string insurer, float inValue, DateTime dateInsured, BuyStatus bSLabel, int uId)
        {
            using var context = new CuriosityContext();

            var cityContext = context.Cities
                .First(a => a.CityName == city);

            var vendorContext = context.Vendors
                .First(a => a.CompanyName == vendor || vendor.Contains(a.CompanyFName));

            var shipperContext = context.Shippers
                .First(a => a.ShipperName == shipper);


            var insuranceTypeContext = context.InsuranceTypes
                .First(a => a.InsuranceName == type);

            var insurerContext = context.Insurers
                .First(a => a.InsurerName == insurer);

            try
            {
                var Context = context.BoughtStatuses
                    .First(a => a.Status == bSLabel);
            }
            catch (Exception e)
            {
                var fill = new FillBoughtStatus();
                fill.FillValues();

            }

            var buyStatusContext = context.BoughtStatuses
                .First(a => a.Status == bSLabel);

            var unitContext = context.Units
                .Find(uId);

            var newShipInsurance = new ShipmentInsurance()
            {
                InsuranceValue = inValue,

                DateInsured = dateInsured,

                InsurerId = insurerContext.InsurerId,
                InsurerLink = insurerContext,

                InsuranceTypeId = insuranceTypeContext.InsuranceTypeId,
                InsuranceTypeLink = insuranceTypeContext
            };

            var newShipment = new SoftwareDesign_Project_QueenAnneCuriosityShop.Shipment
            {
                ShipmentCost = shipCost,
                Quantity = quantity,
                DepartureDate = depDate,
                ExpectedArrivalDate = arrDate,

                IsDeleted = false,

                CityId = cityContext.CityId,
                CityLink = cityContext,

                VendorId = vendorContext.VendorId,
                VendorLink = vendorContext,

                ShipperId = shipperContext.ShipperId,
                ShipperLink = shipperContext,

                ShipmentInsuranceLink = newShipInsurance,

                BoughtStatusId = buyStatusContext.BoughtStatusId,
                BoughtStatusLink = buyStatusContext
            };

            unitContext.BoughtStatusId = buyStatusContext.BoughtStatusId;
            unitContext.BoughtStatusLink = buyStatusContext;

            context.Shipments.Add(newShipment);
            context.SaveChanges();

            MessageBox.Show("Added successfully... now returning");
        }

        public void EditToSave(float shipCost, int quantity, DateTime depDate, DateTime arrDate,
            string city, string vendor, string shipper, string type, string insurer, float inValue, DateTime dateInsured, BuyStatus bSLabel, int shipId, int unitId)
        {
            using var context = new CuriosityContext();

            var shipmentToEdit = context.Shipments
                .Include(a => a.ShipmentInsuranceLink)
                .First(a => a.ShipmentId == shipId);

            var cityContext = context.Cities
                .First(a => a.CityName == city);

            var vendorContext = context.Vendors
                .First(a => a.CompanyName == vendor || (vendor.Contains(a.CompanyFName) && vendor.Contains(a.CompanyLName)));

            var shipperContext = context.Shippers
                .First(a => a.ShipperName == shipper);

            var insuranceTypeContext = context.InsuranceTypes
                .First(a => a.InsuranceName == type);

            var insurerContext = context.Insurers
                .First(a => a.InsurerName == insurer);

            var buyStatusContext = context.BoughtStatuses
                .First(a => a.Status == bSLabel);

            var unitContext = context.Units
                .Find(unitId);

            if (shipmentToEdit.Quantity - quantity < 0)
                unitContext.UnitQuantity += Math.Abs(shipmentToEdit.Quantity - quantity);
            else if (shipmentToEdit.Quantity - quantity > 0)
                unitContext.UnitQuantity -= Math.Abs(shipmentToEdit.Quantity - quantity);

            shipmentToEdit.ShipmentCost = shipCost;
            shipmentToEdit.Quantity = quantity;
            shipmentToEdit.DepartureDate = depDate;
            shipmentToEdit.ExpectedArrivalDate = arrDate;

            shipmentToEdit.CityId = cityContext.CityId;
            shipmentToEdit.CityLink = cityContext;

            shipmentToEdit.VendorId = vendorContext.VendorId;
            shipmentToEdit.VendorLink = vendorContext;

            shipmentToEdit.ShipperId = shipperContext.ShipperId;
            shipmentToEdit.ShipperLink = shipperContext;

            shipmentToEdit.ShipmentInsuranceLink.InsuranceTypeId = insuranceTypeContext.InsuranceTypeId;
            shipmentToEdit.ShipmentInsuranceLink.InsuranceTypeLink = insuranceTypeContext;

            shipmentToEdit.ShipmentInsuranceLink.InsurerId = insurerContext.InsurerId;
            shipmentToEdit.ShipmentInsuranceLink.InsurerLink = insurerContext;

            shipmentToEdit.ShipmentInsuranceLink.InsuranceValue = inValue;
            shipmentToEdit.ShipmentInsuranceLink.DateInsured = dateInsured;

            context.SaveChanges();
            MessageBox.Show("Edit successful... now returning");
        }


        //CITY FUNCTIONALITY
        public City SelectedMainCity
        {
            get => _selectedMainCity;
            set
            {
                _selectedMainCity = value;
                OnPropertyChanged(nameof(SelectedMainCity));
            }
        }

        public void LoadCity(string city)
        {
            using var context = new CuriosityContext();

            var cityContext = context.Cities
                .First(a => a.CityName == city);

            SelectedMainCity = new City()
            {
                CityName = cityContext.CityName,
                ExchangeRate = cityContext.ExchangeRate
            };
        }

        public void CityAddToSave(string name, float er)
        {
            using var context = new CuriosityContext();

            var checkCityContext = context.Cities
                .Where(a => a.CityName == name)
                .ToList();

            if (checkCityContext.Count > 0)
            {
                MessageBox.Show("City is already added");
                return;
            }

            var newCity = new City()
            {
                CityName = name,
                ExchangeRate = er,
                IsDeleted = false
            };

            context.Cities.Add(newCity);
            context.SaveChanges();

            MessageBox.Show("Add Successful");
        }

        public void CityEditToSave(string oldCityName, string oldEr, string name, float er)
        {
            using var context = new CuriosityContext();

            var checkCity = context.Cities
                .Where(a => a.CityName == name)
                .ToList();

            if (checkCity.Count > 0)
            {
                MessageBox.Show("City is already added");
                return;
            }

            var newCity = context.Cities
                .First(a => a.CityName == oldCityName);

            newCity.ExchangeRate = er;
            newCity.CityName = name;

            context.SaveChanges();
            MessageBox.Show("Edit successful");
        }

        public void CityDelete(string oldCityName, string oldEr)
        {
            using var context = new CuriosityContext();
            var newCity = context.Cities
                .First(a => a.CityName == oldCityName && Math.Round(a.ExchangeRate, 3) == Math.Round(float.Parse(oldEr), 3));

            newCity.IsDeleted = true;
            context.SaveChanges();
        }

        //SUPPLIER FUNCTIONALITY
        public Vendor SelectedMainVendor
        {
            get => _selectedMainVendor;
            set
            {
                _selectedMainVendor = value;
                OnPropertyChanged(nameof(SelectedMainVendor));
            }
        }

        public void LoadSupplier(string vendor)
        {
            using var context = new CuriosityContext();

            var vendorContext = context.Vendors
                .First(a => a.CompanyName == vendor ||
                            (vendor.Contains(a.CompanyFName) && vendor.Contains(a.CompanyLName)));

            if (vendorContext.IsCompany)
            {
                SelectedMainVendor = new Vendor()
                {
                    IsCompany = true,
                    CompanyName = vendorContext.CompanyName
                };
            }
            else
            {
                SelectedMainVendor = new Vendor()
                {
                    IsCompany = false,
                    CompanyFName = vendorContext.CompanyFName,
                    CompanyLName = vendorContext.CompanyLName
                };
            }
        }

        public void SupplierAddToSave(bool isCompany, string cName, string cFName, string cLName)
        {
            using var context = new CuriosityContext();

            var checkSupplier = context.Vendors
                .Where(a => a.CompanyName == cName || (a.CompanyFName == cFName && a.CompanyLName == cLName))
                .ToList();

            if (checkSupplier.Count > 0)
            {
                MessageBox.Show("Supplier is already added");
                return;
            }

            var newVendor = new Vendor();

            if (isCompany)
            {
                newVendor.IsCompany = true;
                newVendor.CompanyName = cName;
            }
            else
            {
                newVendor.IsCompany = false;
                newVendor.CompanyFName = cFName;
                newVendor.CompanyLName = cLName;
            }

            newVendor.IsDeleted = false;

            context.Vendors.Add(newVendor);
            context.SaveChanges();
            MessageBox.Show("Add Successful");
        }

        public void SupplierEditToSave(bool isCompany, string cName, string cFName, string cLName, Vendor oldVendor)
        {
            using var context = new CuriosityContext();

            var checkSupplier = context.Vendors
                .Where(a => a.CompanyName == cName || (a.CompanyFName == cFName && a.CompanyLName == cLName))
                .ToList();

            if (checkSupplier.Count > 0)
            {
                MessageBox.Show("Supplier is already added");
                return;
            }

            var vendorContext = context.Vendors
                .First(a => a.CompanyName == oldVendor.CompanyName 
                            || (a.CompanyFName == oldVendor.CompanyFName && a.CompanyLName == oldVendor.CompanyLName));

            if (isCompany)
            {
                vendorContext.IsCompany = true;
                vendorContext.CompanyName = cName;
                vendorContext.CompanyFName = null;
                vendorContext.CompanyLName = null;
            }
            else
            {
                vendorContext.IsCompany = false;
                vendorContext.CompanyFName = cFName;
                vendorContext.CompanyLName = cLName;
                vendorContext.CompanyName = null;
            }

            context.SaveChanges();
            MessageBox.Show("Edit Successful");
        }

        public void SupplierDelete(bool isCompany, string cName, string cFName, string cLName)
        {
            using var context = new CuriosityContext();


            if (isCompany)
            {
                var deleteVendor = context.Vendors
                    .First(a => a.IsCompany == true && a.CompanyName == cName);
                deleteVendor.IsDeleted = true;
            }
            else
            {
                var deleteVendor2 = context.Vendors
                    .First(a => a.IsCompany == false && a.CompanyFName == cFName && a.CompanyLName == cLName);
                deleteVendor2.IsDeleted = true;
            }

            context.SaveChanges();
        }

        //SHIPPER FUNCTIONALITY
        public Shipper SelectedMainShipper
        {
            get => _selectedMainShipper;
            set
            {
                _selectedMainShipper = value;
                OnPropertyChanged(nameof(SelectedMainShipper));
            }
        }

        public void LoadShipper(string shipper)
        {
            using var context = new CuriosityContext();

            var shipperContext = context.Shippers
                .First(a => a.ShipperName == shipper);

            SelectedMainShipper = new Shipper()
            {
                ShipperName = shipperContext.ShipperName,
                ShipperEmail = shipperContext.ShipperEmail,
                ShipperContact = shipperContext.ShipperContact
            };
        }

        public void ShipperAddToSave(string name, string email, string contact)
        {
            using var context = new CuriosityContext();

            var checkShipper = context.Shippers
                .Where(a => a.ShipperName == name)
                .ToList();

            if (checkShipper.Count > 0)
            {
                MessageBox.Show("This Shipper is already added");
                return;
            }

            var newShipper = new Shipper()
            {
                ShipperName = name,
                ShipperContact = contact,
                ShipperEmail = email
            };

            context.Shippers.Add(newShipper);   
            context.SaveChanges();
            MessageBox.Show("Add successful");
        }

        public void ShipperEditToSave(string name, string email, string contact, Shipper oldShipper)
        {
            using var context = new CuriosityContext();

            var checkShipper = context.Shippers
                .Where(a => a.ShipperName == name)
                .ToList();

            if (checkShipper.Count > 0)
            {
                MessageBox.Show("Shipper is already added");
                return;
            }

            var shipperContext = context.Shippers
                .First(a => a.ShipperName == oldShipper.ShipperName);

            shipperContext.ShipperName = name;
            shipperContext.ShipperContact = contact;
            shipperContext.ShipperEmail = email;

            context.SaveChanges();

            MessageBox.Show("Edit successful");
        }

        public void ShipperDelete(string name)
        {
            using var context = new CuriosityContext();
            var shipperContext = context.Shippers
                .First(a => a.ShipperName == name);

            shipperContext.IsDeleted = true;
            context.SaveChanges();
        }

        //INSURANCE TYPE FUNCIONALITY
        public void TypeEditToSave(string name, string oldName)
        {
            using var context = new CuriosityContext();

            var checkType = context.InsuranceTypes
                .Where(a => a.InsuranceName == name)
                .ToList();

            if (checkType.Count > 0)
            {
                MessageBox.Show("Insurance Type is already added");
                return;
            }

            var editType = context.InsuranceTypes
                .First(a => a.InsuranceName == oldName);

            editType.InsuranceName = name;
            context.SaveChanges();
            MessageBox.Show("Edit successful");
        }

        public void TypeAddToSave(string name)
        {
            using var context = new CuriosityContext();

            var checkType = context.InsuranceTypes
                .Where(a => a.InsuranceName == name)
                .ToList();

            if (checkType.Count > 0)
            {
                MessageBox.Show("Insurance Type is already added");
                return;
            }

            var newType = new InsuranceType()
            {
                InsuranceName = name,
                IsDeleted = false
            };

            context.InsuranceTypes.Add(newType);
            context.SaveChanges();

            MessageBox.Show("Add successful");
        }

        public void TypeDelete(string name)
        {
            using var context = new CuriosityContext();
            var typeContext = context.InsuranceTypes
                .First(a => a.InsuranceName == name);

            typeContext.IsDeleted = true;
            context.SaveChanges();
        }

        //INSURER FUNCTIONALITY
        public void InsurerEditToSave(string name, string oldname)
        {
            using var context = new CuriosityContext();

            var checkInsurer = context.Insurers
                .Where(a => a.InsurerName == name)
                .ToList();

            if (checkInsurer.Count > 0)
            {
                MessageBox.Show("Insurer is already added");
                return;
            }


            var insurerContext = context.Insurers
                .First(a => a.InsurerName == oldname);

            insurerContext.InsurerName = name;

            context.SaveChanges();
            MessageBox.Show("Edit successful");
        }

        public void InsurerAddToSave(string name)
        {
            using var context = new CuriosityContext();

            var checkInsurer = context.Insurers
                .Where(a => a.InsurerName == name)
                .ToList();

            if (checkInsurer.Count > 0)
            {
                MessageBox.Show("Insurer is already added");
                return;
            }

            var newInsurer = new Insurer()
            {
                InsurerName = name,
                IsDeleted = false
            };
            context.Insurers.Add(newInsurer);

            context.SaveChanges();
            MessageBox.Show("Add successful");
        }

        public void InsurerDelete(string name)
        {
            using var context = new CuriosityContext();
            var insurerContext = context.Insurers.First(a => a.InsurerName == name);

            insurerContext.IsDeleted = true;
            context.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
