using BusinessObject;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

//add to not build project failed
//must delete after finish all pages
builder.Services.AddDbContext<BookStoreDBContext>();

#region repository
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IBookInStoreService, BookInStoreService>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IPublisherService, PublisherService>();
builder.Services.AddSingleton<IRoleService, RoleService>();
builder.Services.AddSingleton<IStoreService, StoreService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
