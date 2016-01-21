using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class ItemBookAt
    {
        public string Image { get; set; }
        public string Categorie { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class medicalBookAt
    {
        public ObservableCollection<ItemBookAt> Items = new ObservableCollection<ItemBookAt>();
    }
}
