using Inventory.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Inventory.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    RefreshProducts(value);
                }
            }
        }

        private void RefreshProducts(CategoryViewModel value)
        {
            SelectedProduct = null;
            SelectedCategoryProducts.Clear();
            foreach (var p in value.Products)
            {
                SelectedCategoryProducts.Add(p);
            }
        }

        private ProductViewModel selectedProduct;
        public ProductViewModel SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    CreateInvoiceCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<CategoryViewModel> Categories { get; private set; } = new ObservableCollection<CategoryViewModel>();
        public ObservableCollection<ProductViewModel> SelectedCategoryProducts { get; private set; } = new ObservableCollection<ProductViewModel>();

        public RelayCommand CreateInvoiceCommand { get; set; }

        private int requiredCount;

        public int RequiredCount
        {
            get { return requiredCount; }
            set
            {
                if (value != requiredCount)
                {
                    requiredCount = value;
                    OnPropertyChanged(nameof(RequiredCount));
                    CreateInvoiceCommand?.RaiseCanExecuteChanged();
                }
            }
        }



        private readonly ProductService productService;
        public MainViewModel()
        {
            productService = new ProductService();
            this.CreateInvoiceCommand = new RelayCommand(_ => CreateInvoice(), _ => RequiredCount != 0 && SelectedProduct != null);
            Init();
        }

        public void CreateInvoice()
        {
            var sfd = new SaveFileDialog();
            var value = sfd.ShowDialog();
            if (value ?? false)
                productService.CreateInvoce(SelectedProduct.ProductName, RequiredCount, sfd.FileName);
            RequiredCount = 0;
        }

        private void Init()
        {
            var categories = productService.GetCategoriesMaster();
            foreach (var cat in categories)
            {
                var catVm = new CategoryViewModel
                {
                    CategoryName = cat.Name,
                    CategoryId = cat.CategoryID,
                    Products = new ObservableCollection<ProductViewModel>(
                                    cat.Products.Select(p => new ProductViewModel
                                    {
                                        ProductName = p.Name,
                                        QuantityPerUnit = p.QuantityPerUnit,
                                        UnitPrice = p.UnitPrice,
                                    }))
                };
                this.Categories.Add(catVm);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
