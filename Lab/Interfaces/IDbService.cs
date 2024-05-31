namespace Lab.Interfaces
{
    public interface IDbService
    {
        IList<ProductModel> ProductsByPhrase(string phrase);
        IList<TagModel> AllTags();
        IList<CatalogModel> AllCatalogs();

        void CreateTag(String name);
    }
}