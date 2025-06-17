namespace CRUD_API_Demo.Services
{
    public static class ServicesExtensions
    {
        public static void AddServicesToDI(this IServiceCollection services)
        {
           services.AddScoped<IBookService, BookService>();
        }
    }
}
