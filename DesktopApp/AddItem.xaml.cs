using DataObject;
using LogicLayer;
using Microsoft.Win32;
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
using System.Text.RegularExpressions;
using System.IO;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        private ItemManager _itemManager = new ItemManager();
        private Item _newItem;
        private BitmapImage loadedPicture= null;
        public AddItem()
        {
            InitializeComponent();
            string path = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/";
            string pictureName = "logo.png";
            path = path + pictureName;

            Uri iconUri = new Uri(path, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Uri uri = new Uri(op.FileName);
                 loadedPicture=new BitmapImage(uri);
                 picture.Source = loadedPicture;
                
            }  

        }
       

        private void createNewItem()
        {
            try {
            // note - include input validation here
            if (txtName.Text == "" ||
                txtItemId.Text == "" ||
                txtPrice.Text == "" ||
               txtStrength.Text == "" ||
                cboType.SelectedItem == null
               
               )
            {
                MessageBox.Show("You must fill out all the fields.");
                return;
            }
                if(loadedPicture== null)
                {
                    MessageBox.Show("You must upload a picture");
                    return;
                }

            string pictureName = System.IO.Path.GetFileName(picture.Source.ToString());
           
           
           _newItem = new Item()
            {   ItemId= int.Parse(txtItemId.Text),
                Name = txtName.Text,
                Price = decimal.Parse(txtPrice.Text),
                Type = cboType.SelectedValue.ToString(),
                Strength = txtStrength.Text,
                Picture =pictureName,
                Description = txtDescription.Text,
                OnStock = (bool)chkExist.IsChecked

            };
           string currentDirectory = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/" + pictureName;
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(loadedPicture));
                try
                {
                  using (var fileStream = new System.IO.FileStream(currentDirectory, System.IO.FileMode.Create))
                   {
                    encoder.Save(fileStream);
                    fileStream.Close();
                   }

                }
             
                catch (System.IO.IOException) { }

        }
            catch (Exception ex){

                MessageBox.Show(ex.Message);
            }

        }

  
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            createNewItem();
            try
            {
                var result = _itemManager.AddItem(_newItem);
                if (result == true)
                {
                    MessageBox.Show("Item Added successfully");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Failed.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cboType.Items.Count == 0)
            {
                var itemTypes = _itemManager.RetrieveAllItemTypes();
                foreach (var item in itemTypes)
                {
                    cboType.Items.Add(item);
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
                MessageBox.Show("Please Enter only numbers");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           if (MessageBox.Show("Do you really want to cancel this operation ?", "Are You Sure", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
           {
               this.DialogResult = false;
           }
        }
    }
}
