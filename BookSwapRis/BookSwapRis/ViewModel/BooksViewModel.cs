using BookSwapRis.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.SharedTransitions;

namespace BookSwapRis.ViewModel
{



    public class BooksViewModel : BaseViewModel
    {
        private Book _selectedBook;



        string _textSearchBar;

        public string TextSearchBar
        {
            get { return _textSearchBar; }
            set 
            {
                this.BuscarTitulo(value);
                SetProperty(ref _textSearchBar, value); 
            }
        }
    
        private ObservableCollection<Book> _books;
        private List<Book> _allBooks;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set => SetProperty<ObservableCollection<Book>>(ref _books, value);
        }
        public Book SwapFromBook { get; set; }

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { SetProperty<Book>(ref _selectedBook, value); }
        }

       



        public BooksViewModel()
        {
            IsBusy = false;
            LoadData();
            
        }

        private void LoadData()
        {
            IsBusy = true;

            SwapFromBook = new Book()
            {
                Title = "Extremely Loud and Incredibly Close",
                Author = "Jonathan Safran Foer",
                Colors = new ColorValues
                {
                    Accent = Color.FromHex("#FFF571"),
                    DarkAccent = Color.FromHex("#F1EE55"),
                    ExtraDarkAccent = Color.FromHex("#F3DD3F"),
                    TitleColor = Color.FromHex("#F00D39"),
                    AccentTextColor = Color.FromHex("#B7A701"),
                },
                UserImage = "https://randomuser.me/api/portraits/women/12.jpg",
                Description = "Oskar Schell is a super-smart nine-year old grieving the loss of his father, Thomas, who was killed in the World Trade Center attacks on September 11, 2001. He's feeling depressed and anxious, and feels angry and distant towards his mother.",
                CoverImage = "book_extremelyloud",
            };

            _allBooks = new List<Book>()
            {
                new Book()
                {
                    Title = "Everything is Illuminated",
                    Author = "Jonathan Safran Foer",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/12.jpg",
                    CoverImage = "book_illuminated",
                    Description = "Jonathan Safran Foer, a young American Jew and avid collector of his family's heritage, journeys to Ukraine in search of Augustine, the woman who saved his grandfather's life during the Nazi liquidation of Trachimbrod, his family shtetl (a small town) in occupied eastern Poland. ",
                    UserName = "ERNEST ASANOV"
                },
                new Book()
                {
                    Title = "Ulysses",
                    Author = "James Joyce",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/12.jpg",
                    CoverImage = "book_ulysses",
                    Description = "The three central characters—Stephen Dedalus (the hero of Joyce's earlier Portrait of the Artist as a Young Man); Leopold Bloom, a Jewish advertising canvasser; and his wife, Molly—are intended to be modern counterparts of Telemachus, Ulysses (Odysseus), and Penelope, respectively, and the events of the novel loosely",
                    UserName = "KATE BAIKOVA"
                },
                new Book()
                {
                    Title = "Flowers for Algernon",
                    Author = "Daniel Keyes",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/24.jpg",
                    CoverImage = "book_flowers",
                    Description = "Algernon is a laboratory mouse who has undergone surgery to increase his intelligence. The story is told by a series of progress reports written by Charlie Gordon, the first human subject for the surgery, and it touches on ethical and moral themes such as the treatment of the mentally disabled.",
                    UserName = "MARINA YALANSKA"
                },
                new Book()
                {
                    Title = "Casino Royale",
                    Author = "Ian Fleming",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/24.jpg",
                    CoverImage = "book_casino",
                    Description = "By Ian Fleming is a spy thriller set during the Cold War. It tells the story of one man, James Bond, and his evolution into a committed spy and secret agent. Bond accepts a mission to defeat a Russian agent, Le Chiffre, in a card game. ... There are several story lines in the book.",
                    UserName = "VLAD TARAN"
                },
                new Book()
                {
                    Title = "City on the Edge",
                    Author = "Mark Goldman",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/32.jpg",
                    Description = "In a sweeping narrative that speaks to the serious student of urban studies as well as the general reader, Mark Goldman tells the story of twentieth-century Buffalo, New York.",
                    CoverImage = "book_city",
                    UserName = "KONST"

                },
                new Book()
                {
                    Title = "The Hobbit",
                    Author = "J. R. R. Tolkien",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/32.jpg",
                    Description = "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort.",
                    CoverImage = "book_hobbit",
                    UserName = "DENYS BOLDYRIEV"
                }
            };
            IList<Book> filtro = new List<Book>();
            _allBooks.ForEach(item => filtro.Add(item));
            _allBooks.ForEach(item => filtro.Add(item));
            _allBooks.ForEach(item => filtro.Add(item));
            Books = new ObservableCollection<Book>(_allBooks);

            IsBusy = false;
        }

        #region Commands

        public ICommand SearchBarCommand { get { return new Command<string>(BuscarTitulo); } }

        private void BuscarTitulo(string titulo)
        {
            if (titulo == null)
                return;
            IList<Book> filtro = new List<Book>();
            double result;
            string BookTitle;
            titulo = titulo.ToLower();
            var stringcomparerDouble = new F23.StringSimilarity.JaroWinkler();
            _allBooks.ForEach(
                    (item) =>
                    {
                        BookTitle = item.Title.ToLower();
                        result = stringcomparerDouble.Similarity(titulo,BookTitle);
                        if (result>0.85 || BookTitle.Contains(titulo))
                        {
                            filtro.Add(item);
                        }
                    }
                );
            Books = new ObservableCollection<Book>(filtro);
        }

        public ICommand RefreshCommand { get => new Command(LoadData); }

        public ICommand ItemListViewTappedeCommand { get => new Command<Book>(ItemListViewTapped); }

        private void ItemListViewTapped(Book obj)
        {
            var pantalla = new BookSwapRis.Views.BookSwapDetail();
            SharedTransitionNavigationPage.SetBackgroundAnimation(pantalla, BackgroundAnimation.Flip);
            App.Current.MainPage.Navigation.PushAsync(pantalla);
        }



        #endregion

    }
}
