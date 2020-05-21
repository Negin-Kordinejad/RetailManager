using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMWPFUserInterface.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        public ProductDisplayModel Product { get; set; }
        private int _quantiryInCart;
        public int QuantiryInCart
        {
            get { return _quantiryInCart; }
            set
            {
                _quantiryInCart = value;
                CallPropertiChanged(nameof(QuantiryInCart));
                CallPropertiChanged(nameof(DisplayText));
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName} ({QuantiryInCart})";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void CallPropertiChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         }
    }
}
