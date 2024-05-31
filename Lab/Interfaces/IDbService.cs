namespace Lab.Interfaces
{
    public interface IDbService
    {
        IList<ProductModel> ProductsByPhrase(string phrase);
        IList<TagModel> AllTags();
        IList<CatalogModel> AllCatalogs();

        IList<ProductModel> AllProducts();

        void AddProducts(params ProductModel[] products);
        void AddCatalogs(params CatalogModel[] catalogs);
        void AddTags(params TagModel[] tags);

        void Save();

        bool AnyCatalogs();

        void CreateTag(String name);
    }
}