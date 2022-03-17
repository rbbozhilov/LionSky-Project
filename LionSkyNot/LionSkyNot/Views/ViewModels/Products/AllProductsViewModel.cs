namespace LionSkyNot.Views.ViewModels.Products
{
    public class AllProductsViewModel
    {

        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public string Type { get; set; }

        public IEnumerable<string> Types { get; set; }

        public IEnumerable<ProductListViewModel> Products { get; set; }

        public SortedProductViewModel SortedBy { get; set; }


    }
}
