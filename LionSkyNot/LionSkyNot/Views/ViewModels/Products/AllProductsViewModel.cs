namespace LionSkyNot.Views.ViewModels.Products
{
    public class AllProductsViewModel
    {

        public const int ProductsPerPage = 3; 

        public string Brand { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<string> Brands { get; set; }

        public string Type { get; set; }

        public IEnumerable<string> Types { get; set; }

        public IEnumerable<ProductListViewModel> Products { get; set; }

        public SortedProductViewModel SortedBy { get; set; }


    }
}
