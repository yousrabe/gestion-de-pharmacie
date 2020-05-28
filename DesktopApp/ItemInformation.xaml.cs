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
using DataObject;
using LogicLayer;
using Microsoft.Win32;
namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for ItemInformation.xaml
    /// </summary>
    public partial class ItemInformation : Window
    {
        private ItemManager _itemManager = new ItemManager();

        private Item _oldItem;
        private Item _newItem;
        private bool _isManager ;
        private BitmapImage loadedPicture;

        public ItemInformation(Item item , bool isManager)
        {
            this._oldItem = item;
            _isManager = isManager;
            InitializeComponent();
            loadedPicture = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isManager==false)
            {
                btnUpdate.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
                
            btnLoad.Visibility = Visibility.Hidden;

            setupItems();

            string path = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/";
            string  pictureName = "logo.png";
             path = path + pictureName;

            Uri iconUri = new Uri(path, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);

        }
        private void setupItems()
        {
            if (cboType.Items.Count == 0)
            {
                var itemTypes = _itemManager.RetrieveAllItemTypes();
                foreach (var item in itemTypes)
                {
                    cboType.Items.Add(item);
                }
            }
            txtItemID.Text = _oldItem.ItemId.ToString();
            txtName.Text = _oldItem.Name;
            txtPrice.Text = _oldItem.Price.ToString();;
            cboType.SelectedItem = _oldItem.Type;
            txtStrength.Text = _oldItem.Strength;
            string pictureName = _oldItem.Picture;
            string path = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/" + pictureName ;
            picture.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute)); 
            txtDescription.Text = _oldItem.Description;
            chkExist.IsChecked = _oldItem.OnStock;
            setupReadOnly();
        }

        private void setupReadOnly()
        {
            txtName.IsReadOnly = true;
            txtItemID.IsReadOnly = true;
            txtPrice.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            txtStrength.IsReadOnly = true;

            cboType.IsEnabled = false;
            chkExist.IsEnabled = false;
            btnUpdateNow.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Visible;
            btnDelete.Content = "Delete";
            btnLoad.Visibility = Visibility.Hidden;

        }

        private void setupEditable()
        {
            txtName.IsReadOnly = false;
            txtPrice.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            txtStrength.IsReadOnly = false;

            cboType.IsEnabled = true;
            chkExist.IsEnabled = true;
            btnPrevious.Visibility = Visibility.Hidden;
            btnUpdateNow.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Content = "Cancel";
            btnLoad.Visibility = Visibility.Visible;

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            setupEditable();
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
                loadedPicture = new BitmapImage(uri);
                picture.Source = loadedPicture;
        
            }

        }
        private void createNewItem()
        {
            string  pictureName = System.IO.Path.GetFileName(_oldItem.Picture);
            // note - include input validation here
            if (txtName.Text == "" ||
                txtPrice.Text == "" ||
               txtStrength.Text == "" ||
                cboType.SelectedItem == null
               )
            {
                MessageBox.Show("You must fill out all the fields.");
                return;
            }
          if (loadedPicture != null)
          {pictureName = System.IO.Path.GetFileName(loadedPicture.UriSource.ToString());
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
        

              _newItem = new Item()
            {
                ItemId = _oldItem.ItemId,
                Name = txtName.Text,
                Price = decimal.Parse(txtPrice.Text),
                Type = cboType.SelectedValue.ToString(),
                Strength = txtStrength.Text,
                Picture = pictureName,
                Description = txtDescription.Text,
                OnStock = (bool)chkExist.IsChecked

            };
        }

        private void btnUpdateNow_Click(object sender, RoutedEventArgs e)
        {
            createNewItem();
            try
            {
                var result = _itemManager.EditItem(_oldItem, _newItem);

                if (result == true)
                {
                    _oldItem = _newItem;
                    MessageBox.Show("Item Updated");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed.");
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           try
            {
              if((string) btnDelete.Content=="Delete")
              {
                  if (MessageBox.Show("Do you really want to delete this Item ?", "Are You Sure", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                  {
                      var result = _itemManager.Delete(_oldItem.ItemId);

                      if (result == true)
                      {
                          _oldItem = _newItem;
                          MessageBox.Show("Item Deleted");
                          this.DialogResult = true;
                      }
                  }

              }
              else
              {
                  if (MessageBox.Show("Do you really want to cancel this operation ?", "Are You Sure", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                  {
                      setupReadOnly();
                  }

              }
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed.");
            }
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
             this.DialogResult = false;
         }
      
        

    }
}
