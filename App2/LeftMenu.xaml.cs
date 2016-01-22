using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.Devices.Geolocation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Coding4Fun.Toolkit.Controls;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace App2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class LeftMenu : Page
    {
        public int val { get; set; }
        public string token { get; set; }
        public UserAt usr = new UserAt();
        medicalBookAt Book = new medicalBookAt();
        ObservableCollection<Contact> ContactList = new ObservableCollection<Contact>();

        public LeftMenu()
        {
            this.InitializeComponent();
            val = 0;
            Menu.DataContext = new {pos = val};
            lmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
             token = e.Parameter as string;
            request request = new request();
            // usr = JsonConvert.DeserializeObject<UserAt>(request.Get("http://linkat.lazyn.es/api/", "users/" + usr.id.ToString(), token));
            usr = JsonConvert.DeserializeObject<UserAt>(request.Get("http://api.linkat.fr/api/", "users/" + usr.id.ToString(), token));
            usr.auth_token = token;
            UserName.DataContext = usr;
            MessageDialog msg;
            Debug.WriteLine(token);
            //msg.ShowAsync();
        }

        private void BTN_menu_Click(object sender, RoutedEventArgs e)
        {
            Home.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Search.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Contacts.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Forum.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            User.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            MedicalBook.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            lmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //this.Frame.Navigate(typeof(LeftMenu));
            // if (val == 0)
            // {
            // val = -365;
            // Menu.DataContext = new { pos = val };
            // }
            //   else
            // {
            val = 0;
            Menu.DataContext = new { pos = val };
            // }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            usr.auth_token = null;
            this.Frame.Navigate(typeof(MainPage));
        }

        private void GR_home_Tapped(object sender, RoutedEventArgs e)
        {
            if (val == 0)
            {
                val = -365;
                Menu.DataContext = new { pos = val };
            }
            else
            {
                val = 0;
                Menu.DataContext = new { pos = val };
            }
            Home.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void Geocode()
        {
            var geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 100;
            Geoposition position = await geolocator.GetGeopositionAsync();

            // reverse geocoding
            BasicGeoposition myLocation = new BasicGeoposition
            {
                Longitude = position.Coordinate.Longitude,
                Latitude = position.Coordinate.Latitude
            };
            Geopoint pointToReverseGeocode = new Geopoint(myLocation);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

            // here also it should be checked if there result isn't null and what to do in such a case
            var resultLoc = result.Locations[0];
            TB_pos.DataContext = resultLoc.Address;
        }


        private void GR_search_Tapped(object sender, RoutedEventArgs e)
        {
            Geocode();
            val = -365;
            Menu.DataContext = new { pos = val };
            Search.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void GR_user_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(usr.first_name))
            {
                Fname.DataContext = usr;
                TB_fn.DataContext = usr;
            }
            if (!String.IsNullOrEmpty(usr.last_name))
            {
                TB_ln.DataContext = usr;
            }
            if (!String.IsNullOrEmpty(usr.email))
            {
                TB_email.DataContext = usr;
            }
            if (!String.IsNullOrEmpty(usr.address))
            {
                TB_addr.DataContext = usr;
            }
            if (!String.IsNullOrEmpty(usr.age))
            {
                TB_age.DataContext = usr;
            }

            val = -365;
            Menu.DataContext = new { pos = val };
            User.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void GR_contacts_Tapped(object sender, TappedRoutedEventArgs e)
        {
            val = -365;
            Menu.DataContext = new { pos = val };
            Contacts.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (L_Contacts.Items.Count == 0)
            {
                Contact contact = JsonConvert.DeserializeObject<Contact>("{'Image': 'Assets/Contact.png', 'Name': 'Jean', 'Work': 'Généralist', 'Phone': '01.42.42.42.42', 'LName': 'Dupont', 'Adress': '42 rue de la serviette, 42000 VaugonCity'}");
                ContactList.Add(contact);

                L_Contacts.ItemsSource = ContactList;
            }
            else
            {
                return;
            }
        }

        private void BTN_addContact_Click(object sender, RoutedEventArgs e)
        {
            Contacts.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AddContact.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void GR_forum_Tapped(object sender, TappedRoutedEventArgs e)
        {
            val = -365;
            Menu.DataContext = new { pos = val };
            Forum.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        
        private void BTN_backbook_Click(object sender, RoutedEventArgs e)
        {
            User.Visibility = Windows.UI.Xaml.Visibility.Visible;
            MedicalBook.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void BTN_arround_me_click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<UserAt> docList = new ObservableCollection<UserAt>();
            string pos = TB_pos.Text;
            request req = new request();
            string res = req.Get("http://api.linkat.fr/api", "api/search?name=&speciality=&city=" + pos, usr.auth_token);
            char[] delimiterChars = {'[', ']' };
            string[] docs = res.Split(delimiterChars);
            foreach (string doc in docs)
            {
                docList.Add(JsonConvert.DeserializeObject<UserAt>(doc));
            }
            L_Search.ItemsSource = docList;
            Search.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SearchList.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void BTN_addbook_Click(object sender, RoutedEventArgs e)
        {
            //ItemBookAt item = JsonConvert.DeserializeObject<ItemBookAt>("{'Image': 'Assets/heart.png', 'Categorie': 'Test', 'Title': 'test', 'Description': 'Prescription antibiotique, 05/05/13'}");

            //Book.Items.Add(item);
            //L_medicalBook.ItemsSource = Book.Items;
        }

        List<object> selectedItems;

        private void ItemView_MedicalBook_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ItemBookAt elem = (ItemBookAt)e.ClickedItem;
            MessageDialog msg;
            //msg = new MessageDialog(elem.Title.ToString());
            //msg.ShowAsync();
        }

        private void ItemView_Contacts_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageDialog msg;
            msg = new MessageDialog("contact");
            msg.ShowAsync();
        }

        private void list_medicalbook_Click(object sender, RoutedEventArgs e)
        {
         //   if (L_medicalBook.Items.Count == 0)
         //   {
                // Get the medical book on the api
                request request = new request();
                string book = request.Get("http://api.linkat.fr/api/medical_book_by_user_id", "?user_id=" + usr.id.ToString(), usr.auth_token);
                if (book == "")
                {
                    MessageDialog msg;
                    msg = new MessageDialog("Veuillez d'abord créer un carnet médical en cliquant sur \"Créer\".");
                    msg.ShowAsync();
                } else
                {
                    usr.book = JsonConvert.DeserializeObject<medicalBookAt>(book);
                    MessageDialog msge = new MessageDialog(book);
                    msge.ShowAsync();
                    // Need aprsing pour plusieur fields.
                    usr.book.fieldlist.Add(new elem { value = usr.book.fields });
                    L_medicalBook.ItemsSource = usr.book.fieldlist;
                    Tsize.Text = usr.book.size;
                    Tweight.Text = usr.book.weight;
                    // Lister les feilds
                    // Pouvoir les modifier
                    User.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    MedicalBook.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
           // }
        }

        private void BTN_SaveCOntact_Click(object sender, RoutedEventArgs e)
        {
            Contact ct = new Contact();
            ct = JsonConvert.DeserializeObject<Contact>("{'Image': 'Assets/Contact.png', 'Name': '" + TB_ContactName.Text + "', 'Work': '" + TB_ContactWork.Text + "', 'Phone': '" + TB_ContactPhone.Text + "', 'LName': '" + TB_ContactLName.Text + "', 'Adress': '" + TB_ContactAdress.Text + "'}");
            
            ContactList.Add(ct);
            L_Contacts.ItemsSource = ContactList;
            AddContact.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Contacts.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void BTN_backContact_Click(object sender, RoutedEventArgs e)
        {
            AddContact.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Contacts.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        
        private void create_medicalbook_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg;
            request request1 = new request();
            string book = request1.Get("http://api.linkat.fr/api/medical_book_by_user_id", "?user_id=" + usr.id.ToString(), usr.auth_token);
            if (book == "")
            {
                string json = JsonConvert.SerializeObject(new { medical_book = new { user_id = usr.id.ToString(), size = "", weight = "", agreement = true } });         
                //msg = new MessageDialog(json);
                //msg.ShowAsync();
                request request = new request();
                string ret = request.Post("http://api.linkat.fr/api/medical_book/", "", json, usr.auth_token);
                msg = new MessageDialog("Carnet médical créer. Cliquer sur \"Voir\" pour consulter et modifier le carnet médical");
                msg.ShowAsync();
            }
            else
            {
                msg = new MessageDialog("Votre carnet médical a déjà été créé. Cliquer sur \"Voir\" pour le consulter et le modifier");
                msg.ShowAsync();
            }
        }

        private string getNewUserInfo()
        {
            if (usr.first_name != TB_fn.Text)
            {
                usr.first_name = TB_fn.Text;
            }
            if (usr.last_name != TB_ln.Text)
            {
                usr.last_name = TB_ln.Text;
            }
            string cred = JsonConvert.SerializeObject(new { user = new { first_name = TB_fn.Text, last_name = TB_ln.Text } });
            return cred;
        }

        private void BTN_save_user_Click(object sender, RoutedEventArgs e)
        {
            request request = new request();
            string json = getNewUserInfo();
            MessageDialog msg;
            msg = new MessageDialog(json);
            msg.ShowAsync();
            string ret = request.Put("http://api.linkat.fr/api/users/", usr.id.ToString(), json, usr.auth_token);
            //MessageDialog msg;
            msg = new MessageDialog("Save done.");
            //msg.ShowAsync();
            User.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            lmenu.Visibility = Windows.UI.Xaml.Visibility.Visible;
            val = 0;
            Menu.DataContext = new { pos = val };
        }

        private void BTN_delete_account_Click(object sender, RoutedEventArgs e)
        {
            request request = new request();
            bool tmp = request.deleteAccount(usr.id.ToString(), usr.auth_token);
            if (tmp == true)
            {
                MessageDialog msg;
                msg = new MessageDialog("Account succefully deleted.");
                msg.ShowAsync();
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog msg;
                msg = new MessageDialog("Impossible to delete your account. Try again later");
                msg.ShowAsync();
            }
        }

        private void L_Search_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;


        }

       /* private void input_completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
                string result = e.Result;
                if (item != null)
                {
                    int i = 0;
                    foreach (elem el in usr.book.fieldlist)
                    {
                        if (el == item)
                        {
                            Debug.WriteLine("Founded");
                            usr.book.fieldlist[i].value = result;
                        }
                        i = i + 1;
                    }
                //L_medicalBook.InitializeViewChange();
                L_medicalBook.ItemsSource = usr.book.fieldlist;
                
                }
        }

        public object item;

        private void modif_elem_med_book(object sender, ItemClickEventArgs e)
        {
            InputPrompt input = new InputPrompt();
            input.Message = "Modifier le champ sélectionné";
            item = e.ClickedItem;
            input.Completed += input_completed;
            input.Show();
            
               

       } */

        private void textBlock7_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // Fonction de sauvegarde du medical book.
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(new { medical_book = new { size = Tsize.Text, weight = Tweight.Text , fields = Tfield.Text} });
            //string json = JsonConvert.SerializeObject(new { medical_book = usr.book });
            request request = new request();
            string ret = request.Put("http://api.linkat.fr/api/medical_book/", usr.id.ToString(), json, usr.auth_token);
            MessageDialog msg;
            msg = new MessageDialog(ret);
            msg.ShowAsync();
        }
    }
}
