using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class elem //: INotifyPropertyChanged
    {
        /*public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string elemValue;*/
        public string value { get; set; }

        public override string ToString()
        {
            return this.value;
        }
        /*{
            get
            {
                return this.elemValue;
            }
            set
            {
                if (value != this.elemValue)
                {
                    this.elemValue = value;
                   NotifyPropertyChanged();
                }
            }
        }*/
    }

    public class medicalBookAt 
    {
        public medicalBookAt()
        {
            fieldlist = new ObservableCollection<elem>();
        }
        public string id { get; set; }
        public string user_id { get; set; }
        public string fields { get; set; }
        public string size { get; set; }
        public string weight { get; set; }
        public string agreement { get; set; }
        public string address_id { get; set; }
        public ObservableCollection<elem> fieldlist { get; set; }

       
        
    }
}
