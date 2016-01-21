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
using Newtonsoft.Json;
using Windows.UI.Popups;


// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Subscribe : Page
    {
        public Subscribe()
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

        private void BTN_subscribe_Click(object sender, RoutedEventArgs e)
        {
            request request = new request();
            UserAt usr = new UserAt();
            usr.first_name = TB_fn.Text;
            usr.last_name = TB_ln.Text;
            usr.email = TB_mail.Text;
            if (TB_Pass.Text == TB_VPass.Text){
                usr.password = TB_Pass.Text;
                usr.password_confirmation = TB_VPass.Text;
                string Sjson = JsonConvert.SerializeObject(new { user = usr });
                string user = request.Post("http://api.linkat.fr/api/users", "", Sjson);
                if (user == "")
                {
                    MessageDialog msg;
                    msg = new MessageDialog("An error as come, please try later.");
                    msg.ShowAsync();
                    //this.Frame.Navigate(typeof(Subscribe));
                }
                else
                {
                    this.Frame.Navigate(typeof(Login));
                }
            } else {
                MessageDialog msg;
                msg = new MessageDialog("Sorry, your password doesn't match");
                msg.ShowAsync();
            }
        }

    }
}
