# Processing Xamarin ListView with LoadMore in MVVM

ListView supports binding the [LoadMoreOption](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~LoadMoreOption.html), [LoadMoreCommand](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~LoadMoreCommand.html), and [IsBusy](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~IsBusy.html) properties from view model to load more number of items at runtime. `LoadMoreOption` enables load more manually or automatically when loading the items at runtime. `LoadMoreCommand` executes to load the items form view model. The `IsBusy` property notifies that the items are populating from view model to show or hide the load more view.

The IsBusy property in view model shows the busy indicator when populating the [ItemsSource](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemsSource.html).

```
<syncfusion:SfListView x:Name="listView"                            
                        LoadMoreOption="Auto"
                        LoadMoreCommand="{Binding LoadMoreItemsCommand}"
                        LoadMoreCommandParameter="{Binding Source={x:Reference Name=listView}}"
                        IsBusy="{Binding IsBusy}"
                        ItemsSource="{Binding BookInfoCollection}">
```

```
public class ViewModel:INotifyPropertyChanged
{
    private bool isBusy;
    public bool IsBusy
    {
        get { return isBusy; }
        set
        {
            this.isBusy = value;
            RaisePropertyChanged("IsBusy");
        }
    }
    private int totalItems = 22;
    public Command<object> LoadMoreItemsCommand { get; set; }

    public ViewModel()
    {
        BookInfoCollection = new ObservableCollection<BookInfo>();
        AddBookInfos(0, 3);
        LoadMoreItemsCommand = new Command<object>(LoadMoreItems);
    }

    private bool CanLoadMoreItems(object obj)
    {
        if (BookInfoCollection.Count >= totalItems)
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

            var index = BookInfoCollection.Count;
            var count = index + 3 >= totalItems ? totalItems - index : 3;
            AddBookInfos(index, 3);

        }
        catch
        {

        }
        finally
        {
            IsBusy = false;
        }
    }

    private void AddBookInfos(int index, int count)
    {
        int bookNameCount = 0;
        int bookDescriptionCount = 0;
        for (int i = index; i < index + count; i++)
        {
            if (bookNameCount == 11)
                bookNameCount = 0;
            if (bookDescriptionCount == 11)
                bookDescriptionCount = 0;
            BookInfoCollection.Add(new BookInfo() { BookName = bookNames[bookNameCount] , BookDescription =  bookDescription[bookDescriptionCount] });
            bookNameCount++;
            bookDescriptionCount++;
        }
    }
}
```
You can also refer our UG documentation to know more about [MVVM](https://help.syncfusion.com/xamarin/sflistview/mvvm).
