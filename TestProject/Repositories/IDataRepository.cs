namespace TestProject.Repositories
{
    public interface IDataRepository
    {
        Task CreateAsync(int item1, string item2);
        Task<IList<Data>> GetAsync(int start, int end);
        Task DeleteAsync(int id);

    }
}
