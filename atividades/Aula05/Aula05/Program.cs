using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

FillProductData();
FillCustomerData();

app.Run();

static void FillCustomerData() 
{
    for (int i = 0; i < 10; i++)
    {
        Customer customer = new()
        {
            Id = i + 1,
            Name = $"Customer {i}",
            HomeAddress = new Adress()
            {
                Id = i,
                Adress_Type = "Casa",
                City = "Fraiburgo",
                Country = "Brasil",
                State_Province = "SC",
                Postal_Code = "89580-000",
                Street1 = "Rua 1",
                Street2 = "Rua 2"
            }
        };

        CustomerData.Customers.Add(customer);
    }
}
static void FillProductData()
{
    for (int i = 0; i < 10; i++)
    {
        Product product = new()
        {
            Id = i + 1,
            ProductName = $"Object {i}",
            Description = "exemplo",
            CurrentPrice = i * 10
        };

        ProductData.Products.Add(product);
    }
}
