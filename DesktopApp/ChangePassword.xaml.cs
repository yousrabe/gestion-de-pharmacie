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
using DataObjects;
using LogicLayer;

namespace WpfPresentation
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private User _user;
        private UserManager _userManager;
        private bool _isNewUser;
        public ChangePassword(User user, UserManager userManager, bool isNew = false)
        {

            this._user = user;
            this._userManager = userManager;
            this._isNewUser = isNew;
            
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            string pictureName = "logo.png";
            string path = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/" + pictureName;

            Uri iconUri = new Uri(path, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            if (_isNewUser == true)
            {

                this.lbMessage.Content =  _user.FirstName + ", this is your first login. You must change your password now.";
                this.lbMessage.FontSize = 15;
            }
         
            txtEmail.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Length < 7 || txtEmail.Text.Length > 50)
            {
                MessageBox.Show("Invalid Email.");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }

            if (pwdCurrentPwd.Password.Length < 6)
            {
                MessageBox.Show("Invalid Current Password");
                pwdCurrentPwd.Focus();
                pwdCurrentPwd.SelectAll();
                return;
            }

            if (pwdNewPwd.Password.Length < 6)
            {
                MessageBox.Show("Invalid New Password");
                pwdNewPwd.Focus();
                pwdNewPwd.SelectAll();
                return;
            }

            if (string.Compare(pwdNewPwd.Password,
                pwdRetypedPwd.Password) != 0)
            {
                MessageBox.Show("New Password and Retyped Password must match.");
                pwdRetypedPwd.Password = "";
                pwdNewPwd.Focus();
                pwdNewPwd.SelectAll();
                return;
            }

            // Whew! We made it past the user errors!
            string oldPassword = pwdCurrentPwd.Password;
            string newPassword = pwdNewPwd.Password;
            string username = txtEmail.Text;
            try
            {
                if (_userManager.UpdatePassword(username, oldPassword, newPassword))
                {
                    MessageBox.Show("Password Updated");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
