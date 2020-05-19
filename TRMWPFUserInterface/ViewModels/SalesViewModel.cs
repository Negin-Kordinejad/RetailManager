using Caliburn.Micro;
using DesktopUILibrary.Api;
using DesktopUILibrary.Helper;
using DesktopUILibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMWPFUserInterface.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndPoint _productEndPoint;
        private IConfigHelper _configHelper;

        public SalesViewModel(IProductEndPoint productEndPoint, IConfigHelper configHelper)
        {
            _productEndPoint = productEndPoint;
            _configHelper = configHelper;
        }
        private async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            Produsts = new BindingList<ProductModel>(productList);

        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<ProductModel> _products;
        public BindingList<ProductModel> Produsts
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Produsts);
            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }
        private int _itemQuantity = 1;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        public string SubTotal
        {
            get
            {
                return calculateSubTotal().ToString("C");
            }
        }
        private decimal calculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += (item.Product.RetailPrice * item.QuantiryInCart);
            }
            return subTotal;
        }
        private decimal CalculateTax()
        {
            decimal taxrate = _configHelper.GetTaxRate()/100;
            decimal taxAmount = 0;
            taxAmount = Cart.Where(x => x.Product.IsTaxable).Sum(x => x.Product.RetailPrice * x.Product.QuantityInStock * taxrate);
            //foreach (var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmount += (item.Product.RetailPrice * item.QuantiryInCart * taxrate);
            //    }
            //}
            return taxAmount;
        }
        public string Tax
        {
            get
            {
                return CalculateTax().ToString("C");
            }

        }
        public string Total
        {
            get
            {
                decimal total = calculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }

        }
        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }
        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (existingItem != null)
            {

                existingItem.QuantiryInCart += ItemQuantity;
                // Cart.ResetBindings();
                Cart.ResetItem(Cart.IndexOf(existingItem));
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantiryInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);

        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                return output;
            }
        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }
        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                return output;
            }
        }
        public void CheckOut()
        {
        }
    }
}
