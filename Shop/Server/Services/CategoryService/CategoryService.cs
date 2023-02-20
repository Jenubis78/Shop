namespace Shop.Server.Services.CategoryService
{
    public class CategoryService: ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var response = new ServiceResponse<List<Category>>
            {
                Data = await _context.Categories.ToListAsync()

            };
            return response;
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int categoryId)
        {
            var response = new ServiceResponse<Category>();

            var result = await _context.Categories.FindAsync(categoryId);

            if (result == null)
            {
                response.Success = false;
                response.Message = "Cannot find this category";

            }
            else
            {
                response.Data = result;
            }

            return response;
        }
    }
}
