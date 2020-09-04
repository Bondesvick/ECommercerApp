using E_CommerceApp;
using E_CommerceApp.Format;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataOperations Operations { get; }
        private IPagination Paginator { get; }

        public MainWindow(IDataOperations operations, IPagination paginator)
        {
            this.Operations = operations;
            this.Paginator = paginator;
            InitializeComponent();
        }

        // Loads the main window
        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.CategorySelect.ItemsSource =
                await Task.Run(() => Operations.LoadCategoryData());
                await Task.Run(() => Operations.LoadCart());
                await Task.Run(() => Operations.LoadProductData());
                await Task.Run(() => Paginator.NextPage(5));
                this.FilterDisplay.ItemsSource = Paginator.PageData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            this.DataContext = Operations;
        }

        // An event that runs when selection is changed on the Product page DataGridView
        private void DataDisplay_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (this.DataDisplay.SelectedItem != null)
                {
                    var selectedRow = (IProduct)this.DataDisplay.SelectedItem;
                    this.ProductName.Text = selectedRow.ProductName;
                    this.ProductPrice.Text = selectedRow.CostPrice.ToString(CultureInfo.InvariantCulture);
                    this.CategorySelect.SelectedIndex = selectedRow.CategoryId - 1;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // An event that calls the Create product method
        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.ProductName.Text) ||
                string.IsNullOrWhiteSpace(this.ProductPrice.Text))
            {
                MessageBox.Show("Fill all the fields!");
                return;
            }

            if (CategorySelect.SelectedIndex == -1)
            {
                MessageBox.Show("Select a category");
                return;
            }

            try
            {
                var categoryId = this.CategorySelect.SelectedIndex + 1;
                Operations.AddProductData(this.ProductName.Text, categoryId, decimal.Parse(this.ProductPrice.Text));
                MessageBox.Show("Product Added!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // An event that calls the Update product method
        private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.ProductName.Text) ||
                string.IsNullOrWhiteSpace(this.ProductPrice.Text))
            {
                MessageBox.Show("Fill all the fields!");
                return;
            }

            if (this.DataDisplay.SelectedItem == null)
            {
                MessageBox.Show("Select an item to update from the table");
                return;
            }

            try
            {
                var categoryId = this.CategorySelect.SelectedIndex + 1;
                var id = ((IProduct)this.DataDisplay.SelectedItem).ProductId;
                Operations.UpdateProductData(id, this.ProductName.Text, categoryId, decimal.Parse(this.ProductPrice.Text));
                MessageBox.Show("item has been updated");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // An event that calls the Delete product method
        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.DataDisplay.SelectedItem == null)
            {
                MessageBox.Show("Select an item to delete from the table");
                return;
            }

            try
            {
                var id = ((IProduct)this.DataDisplay.SelectedItem).ProductId;
                var name = ((IProduct)this.DataDisplay.SelectedItem).ProductName;
                Operations.DeleteProductData(id);
                MessageBox.Show(name + " has been deleted");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        //Refreshes the Product Table on Product register page
        private async void ButtonRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            this.DataDisplay.SelectedItem = null;
            this.ProductName.Text = this.ProductPrice.Text = string.Empty;
            try
            {
                await Task.Run(() => Operations.LoadProductData());

                this.DataContext = Operations;
                this.DataDisplay.ItemsSource = Operations.ProductData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Searches the product Table on Cart page by Name
        private void ProductSrcBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Operations.SearchProductByName(this.ProductSrcBox.Text);
                this.FilterDisplay.ItemsSource = null;
                this.FilterDisplay.ItemsSource = Operations.ProductData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Searches the Product table on Cart page by Price
        private void IdSrcBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Operations.SearchProductByPrice(decimal.Parse(this.IdSrcBox.Text));
                this.FilterDisplay.ItemsSource = null;
                this.FilterDisplay.ItemsSource = Operations.ProductData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Previous page navigation
        private void PrevBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //await Task.Run(() => Operations.LoadCart());
                Paginator.PrevPage(5);
                this.FilterDisplay.ItemsSource = null;
                this.FilterDisplay.ItemsSource = Paginator.PageData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Next page navigation
        private void NextBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //await Task.Run(() => Operations.LoadCart());
                Paginator.NextPage(5);
                this.FilterDisplay.ItemsSource = null;
                this.FilterDisplay.ItemsSource = Paginator.PageData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Search by product name on product page
        private void ProductSrchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Operations.SearchProductByName(this.ProductSrchTxt.Text);
                this.DataDisplay.ItemsSource = null;
                this.DataDisplay.ItemsSource = Operations.ProductData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Search by product price on Product page
        private void IdSrchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Operations.SearchProductByPrice(decimal.Parse(this.IdSrchTxt.Text));
                this.DataDisplay.ItemsSource = null;
                this.DataDisplay.ItemsSource = Operations.ProductData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Reloads data when tab is switched
        private async void Tab_ChangeFocus(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Operations.LoadProductData());
        }

        // Adds product to Cart
        private void ButtonAddToCart_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.QuantityTxt.Text))
            {
                MessageBox.Show("Specify the quantity you want to add!");
                return;
            }

            if (this.FilterDisplay.SelectedItem == null)
            {
                MessageBox.Show("Select a product from the product table to add");
                return;
            }
            try
            {
                var product = (IProduct)this.FilterDisplay.SelectedItem;
                Operations.AddToCart(product.ProductId, product.CategoryId, int.Parse(this.QuantityTxt.Text));
                MessageBox.Show("Product has been added to Cart");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                ;
            }
        }

        // Updates product in cart
        private void ButtonUpdateCart_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.QuantityTxt.Text))
            {
                MessageBox.Show("Fill the quantity field!");
                return;
            }

            if (this.CartDisplay.SelectedItem == null)
            {
                MessageBox.Show("Select an item to update from Cart the table");
                return;
            }

            try
            {
                var id = ((ICart)this.CartDisplay.SelectedItem).Id;
                Operations.UpdateCart(id, int.Parse(this.QuantityTxt.Text));
                MessageBox.Show("Cart has been updated");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Deletes product from Cart
        private void ButtonDelFromCart_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.CartDisplay.SelectedItem == null)
            {
                MessageBox.Show("Select an item to delete from the Cart table");
                return;
            }

            try
            {
                var id = ((ICart)this.CartDisplay.SelectedItem).Id;
                Operations.DeleteFromCart(id);
                MessageBox.Show("Item has been removed from");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Refreshes Cart Table
        private void ButtonRefreshCart_OnClick(object sender, RoutedEventArgs e)
        {
            Operations.LoadCart();
            this.CartDisplay.ItemsSource = Operations.CartData;
        }

        // Selection changed event for Cart Table
        private void CartDisplay_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (this.CartDisplay.SelectedItem != null)
                {
                    var selectedRow = (ICart)this.CartDisplay.SelectedItem;
                    this.QuantityTxt.Text = selectedRow.Quantity.ToString();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}