using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
    public class LoadMoreViewModel :INotifyPropertyChanged
    {
        #region Fields
        internal string[] Names = new string[]
        {"Apple", "Banana", "Papaya", "Lime", "Pomegranate", "Orange",  "Watermelon", "Apricot", "Grapes", "Cherry", "Custard Apple", "Dragon", "Pear", "Mango", "Lemon", "Guava", "Jackfruit", "Kiwi", "Peaches", "Pineapple", "Strawberry", "Raspberry"};

        internal string[] Weights = new string[]
        {"500 gm","850 gm","500 gm","500 gm","400 gm","500 gm","950 gm","900 gm","500 gm","500 gm","500 gm","950 gm","500 gm","500 gm","500 gm","500 gm","500 gm","500 gm","500 gm","750 gm","500 gm","500 gm"};

        internal double[] Prices = new double[]
        {2.47,1.40,1.48,2.28,10.47,1.00,3.98,14.99,1.50,7.48,26.20,22.66,1.47,7.10,7.40,6.00,7.27,7.33,9.99,2.00,13.99,16.99};

        private int totalItems = 22;

        private bool isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public ObservableCollection<Product> Products { get; set; }
        public Command<object> LoadMoreItemsCommand { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        #endregion

        #region Constructor
        public LoadMoreViewModel()
        {
            Products = new ObservableCollection<Product>();
            AddProducts(0, 3);
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
        }
        #endregion

        #region Private Methods
        private bool CanLoadMoreItems(object obj)
        {
            if (Products.Count >= totalItems)
                return false;
            return true;
        }

        private async void LoadMoreItems(object obj)
        {
            var listview = obj as Syncfusion.ListView.XForms.SfListView;
            try
            {
                IsBusy = true;
                await Task.Delay(1000);
                var index = Products.Count;
                var count = index + 3 >= totalItems ? totalItems - index : 3;
                AddProducts(index, count);
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddProducts(int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                var name = Names[i];
                var p = new Product()
                {
                    Name = name,
                    Weight = Weights[i],
                    Price = Prices[i],
                    Image = ImageSource.FromResource("SfListViewSample.LoadMore." + name.Replace(" ", string.Empty) + ".jpg")
                };

                Products.Add(p);
            }
        }
        #endregion

        #region Interface
        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
