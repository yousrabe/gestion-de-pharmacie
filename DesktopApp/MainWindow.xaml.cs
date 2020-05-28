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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace WpfPresentation
{

    public partial class MainWindow : Window
    {

        private UserManager _userManager = new UserManager();
        private User _user = null;

        public MainWindow()
        {
            InitializeComponent();
            
        }

         private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string src = Environment.CurrentDirectory + "/../../../MVCPresentation/Content/Photos/";

            string pictureName = "logo.png";
            string path = src + pictureName;

            LogoPicture.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            pictureName = "login_button.jpg";
            path = src + pictureName;


            LoginPicture.ImageSource = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

            pictureName = "logo.png";
            path = src + pictureName;


            Uri iconUri = new Uri(path, UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);


        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            // test our login function
            try
            {
                string email = txtEmail.Text;
                string password = pwdPassword.Password;

                if (email == "")
                {
                    if (password == "")
                        throw new ApplicationException("Please enter your Email and your Password");
                    else
                        throw new ApplicationException("Please enter your Email");
                }
                else
                {
                    if (email.Length < 7 || email.Length > 50)
                        throw new ApplicationException("Invalid Email");
                   
                        if (password == "")
                            throw new ApplicationException("Please enter your Password");
                   
                            else
                            {
                                _user = _userManager.AuthenticateUser(email, password);

                                if (_user != null)
                                {

                                    // check for new user
                                    //The default password of the user is the concatenation of firstName and lastName 

                                    if (password == (_user.FirstName).ToLower() + (_user.LastName).ToLower())
                                    {

                                        this.Hide();
                                        HomePage homePage = new HomePage(_user, _userManager, true);
                                        homePage.ShowDialog();
                                        this.Close();
                                        ;
                                    }
                                    else
                                    {

                                        this.Hide();
                                        HomePage homePage = new HomePage(_user, _userManager, false);
                                        homePage.ShowDialog();
                                        this.Close();
                                    }

                                    
                                }
                                else
                                {
                                    throw new ApplicationException("Enter a valid Email and Password");

                                }
                            

                            }
                        
                    
           }
  }

 catch (Exception ex)
     {
       MessageBox.Show(ex.Message);
     }
 }



     
    }
}