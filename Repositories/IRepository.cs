    namespace DevSpot.Repositories
    {
        public interface IRepository<T> where T: class
        {
            Task<IEnumerable<T>> GetAllAsync(); //T is Job Posting in our scenerio can use any other variable as well 
            Task<T> GetByIdAsync(int id);
            Task AddAsync(T entity); //enity is everything we can add to database eg is here i want to add job posting 
            Task UpdateAsync(T entity); 
            Task DeleteAsync(int id);
        }
    }
