using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Facebook.Client;
using Windows.Web.Http.Filters;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Login : Page
    {

        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void BTN_back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void BTN_login_Click(object sender, RoutedEventArgs e)
        {
            request request = new request();
            string cred = JsonConvert.SerializeObject(new { auth = new { email = TB_id.Text, password = TB_password.Text } });
            // TokenAt tmp = request.Login("http://linkat.lazyn.es/api/", "knock/auth_token", cred);
            TokenAt tmp = request.Login("http://api.linkat.fr/api/", "knock/auth_token", cred);
            if (String.IsNullOrEmpty(tmp.jwt))
            {
                MessageDialog msg;
                msg = new MessageDialog("Wrong username or invalid password.");
            //    msg.ShowAsync();
            }
            else
            {
                this.Frame.Navigate(typeof(LeftMenu), tmp.jwt);
            }
        }

        private void BTN_login_facebook_Click(object sender, RoutedEventArgs e)
        {
            //Session.ActiveSession.LoginWithBehavior("email,public_profile,user_friends", FacebookLoginBehavior.LoginBehaviorMobileInternetExplorerOnly);
            MessageDialog msg;
            msg = new MessageDialog("Sorry, the Facebook connection is coming soon");
            msg.ShowAsync();
        }

        private void BTN_login_linkedin_Click_1(object sender, RoutedEventArgs e)
        {
            MessageDialog msg;
            msg = new MessageDialog("Sorry, the Linkedin connection is coming soon");
            msg.ShowAsync();
        }
    }
}
