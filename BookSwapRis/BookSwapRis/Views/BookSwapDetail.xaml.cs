using BookSwapRis.Models;
using BookSwapRis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookSwapRis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookSwapDetail : ContentPage
    {
        public BookSwapDetail()
        {
            InitializeComponent();
            BindingContext = App.mainBooksViewModel;
        }
    }
}