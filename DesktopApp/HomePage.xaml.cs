using DataObject;
using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private UserManager _userManager = new UserManager();
        private ItemManager _itemManager = new ItemManager();
        private List<Item> _items = new List<Item>();
        private List<Item> _currentItems = new List<Item>();

        private User _user;
        private bool _isNew;
        public HomePage(User user, UserManager userManager, bool isNew = false)
        {
            this._userManager = userManager;
            this._user = user;
            this._isNew = isNew;

            InitializeComponent();
           
          }
 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string src = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/";

            string pictureName = "logo.png";
            string path =  src + pictureName;

            Logo.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            pictureName = "setup_icon.png";
            path = src + pictureName;
            Settings.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute)); 
       
            pictureName = "shutdown.png";
            path = src + pictureName;
            LogOut.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            pictureName = "logo.png";
            path = src + pictureName;

            Uri iconUri = new Uri(path, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);


            try
            { 
              if (_isNew == true)
            {

                ChangePassword changePassword = new ChangePassword(_user, _userManager, true);

                changePassword.ShowDialog();


            }
              if (_user.Role != "Manager")
                  btnAdd.Visibility = Visibility.Hidden;


            lbWelcome.Content = "Welcome " + _user.FirstName;
            _items = _itemManager.RetrieveItems();
              
                if (_currentItems == null)
                {
                    _currentItems = _items;
                }
                refreshData();
             
                if (cboType.Items.Count == 0)
                {
                    cboType.Items.Add("Show All");

                    var itemTypes = _itemManager.RetrieveAllItemTypes();
                    foreach (var item in itemTypes)
                    {
                        cboType.Items.Add(item);
                    }
                    cboType.SelectedItem = "Show All";

                }


         
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception while loading data "+ ex.Message+ ex.Source);
            }


        }

      
        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(_user, _userManager, false);

            changePassword.ShowDialog();
        }

        private void LogOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }


        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            filterItems();
        }
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterItems();
        }

        private void filterItems()
        {
            try
            {
                _currentItems = _itemManager.RetrieveItems();


                if (cboType.SelectedItem.ToString() != "Show All")
                {
                    _currentItems = _currentItems.FindAll(b => b.Type == cboType.SelectedItem.ToString());
                }
                if (txtSearch.Text.ToString() != "")
                {
                    _currentItems = _currentItems.FindAll(b => b.Description.ToLower().Contains(txtSearch.Text.ToString().ToLower()));
                }
                 refreshData();

             
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception while loading items "+ex.Message);
              
            }
        }

  

        private void SearchById_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _currentItems = _itemManager.RetrieveItems();
            refreshData();
      
        }


        private void SearchByName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _currentItems = _itemManager.RetrieveItems();
            refreshData();
    
        }

        private void TabItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _currentItems = _itemManager.RetrieveItems();
            refreshData();

        }

        private void btnSearchById_Click(object sender, RoutedEventArgs e)
        {
            try { 
            int id = int.Parse(txtItemId.Text);
            if (txtItemId.Text != null)
            {
                _currentItems = _itemManager.RetrieveItemsById(id);
                refreshData();

            }
          
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchByNme_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            if (name != null)
            { _currentItems = _itemManager.RetrieveItemsByName(name);
              refreshData();

            }
        }

        private void dgItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ;
            ItemInformation itemInformation;

            var item = (Item)dgItems.SelectedItem;
            if (_user.Role == "Manager")
            {
                itemInformation = new ItemInformation(item, true);
                itemInformation.ShowDialog();
                if (itemInformation.DialogResult == true)
                {
                    _currentItems = _itemManager.RetrieveItems();
                    refreshData();
                }
            }
            else
            {
                itemInformation = new ItemInformation(item, false);
                itemInformation.ShowDialog();
                if (itemInformation.DialogResult == true)
                {
                    _currentItems = _itemManager.RetrieveItems();
                    refreshData();
                }
               
            }

        }
        public void refreshData()
        {

            dgItems.ItemsSource = _currentItems;
            dgItems.Columns[4].Visibility = Visibility.Hidden;
            dgItems.Columns[5].Visibility = Visibility.Hidden;
            dgItems.Columns[6].Visibility = Visibility.Hidden;
            dgItems.Columns[7].Visibility = Visibility.Hidden;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.ShowDialog();
            if (addItem.DialogResult == true)
            {
                _currentItems = _itemManager.RetrieveItems();
               refreshData();
            }
                
        }
  
    }
}
