using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao container
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

InicializarCategorias();
InicializarPropriedades();
InicializarClientes();

app.Run();

static void InicializarCategorias()
{
    if (PropertyData.Categories.Count == 0)
    {
        PropertyData.Categories.AddRange(new List<Category>
        {
            new Category { Id = 1, Name = "Apartamento", Description = "Unidade residencial em pr�dio" },
            new Category { Id = 2, Name = "Casa", Description = "Resid�ncia com quintal privativo" },
            new Category { Id = 3, Name = "S�tio/Fazenda", Description = "Propriedade rural para lazer ou agricultura" },
            new Category { Id = 4, Name = "Escrit�rio Comercial", Description = "Espa�o para uso empresarial" },
            new Category { Id = 5, Name = "Galp�o", Description = "�rea ampla para armazenamento ou uso industrial" },
            new Category { Id = 6, Name = "Terreno", Description = "Lote urbano ou rural para constru��o" }
        });
    }
}

static void InicializarPropriedades()
{
    if (PropertyData.Properties.Count == 0)
    {
        var categorias = PropertyData.Categories;

        PropertyData.Properties.AddRange(new List<Property>
        {
            new Property
            {
                Id = 1,
                Number = 345,
                Name = "Apartamento Parque Residencial",
                Description = "Apartamento moderno com 3 quartos (1 su�te), sala ampla e cozinha americana",
                BedroomCount = 3,
                GarageSpots = 2,
                BathroomCount = 2,
                Price = 450000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 1),
                Address = new Address
                {
                    Id = 1,
                    Street = "Rua das Flores, 123",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-000",
                    Address_Type = "Residencial"
                }
            },
            new Property
            {
                Id = 2,
                Number = 895,
                Name = "Apartamento Plaza Central",
                Description = "Apartamento no centro com 2 quartos, �rea de servi�o e varanda",
                BedroomCount = 2,
                GarageSpots = 1,
                BathroomCount = 1,
                Price = 1800.00m,
                ForSale = false,
                Category = categorias.First(c => c.Id == 1),
                Address = new Address
                {
                    Id = 2,
                    Street = "Avenida Brasil, 456",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-001",
                    Address_Type = "Residencial"
                }
            },
            new Property
            {
                Id = 3,
                Number = 745,
                Name = "Casa Familiar Jardim Am�rica",
                Description = "Casa espa�osa com 4 quartos, sala, copa, cozinha e �rea gourmet",
                BedroomCount = 4,
                GarageSpots = 3,
                BathroomCount = 3,
                Price = 680000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 2),
                Address = new Address
                {
                    Id = 3,
                    Street = "Rua dos Ip�s, 789",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-002",
                    Address_Type = "Residencial"
                }
            },
            new Property
            {
                Id = 4,
                Number = 741,
                Name = "Casa Bela Vista",
                Description = "Casa t�rrea com 2 quartos, quintal amplo e �rea de churrasco",
                BedroomCount = 2,
                GarageSpots = 2,
                BathroomCount = 1,
                Price = 2500.00m,
                ForSale = false,
                Category = categorias.First(c => c.Id == 2),
                Address = new Address
                {
                    Id = 4,
                    Street = "Rua Bela Vista, 321",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-003",
                    Address_Type = "Residencial"
                }
            },
            new Property
            {
                Id = 5,
                Number = 12,
                Name = "S�tio Retiro Verde",
                Description = "S�tio com 5 hectares, casa principal com 3 quartos, pomar e �rea para animais",
                BedroomCount = 3,
                GarageSpots = 4,
                BathroomCount = 2,
                Price = 850000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 3),
                Address = new Address
                {
                    Id = 5,
                    Street = "Estrada SC-135, Km 8",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-900",
                    Address_Type = "Rural"
                }
            },
            new Property
            {
                Id = 6,
                Number = 1856,
                Name = "Escrit�rio Comercial Centro",
                Description = "Escrit�rio comercial de 45m� com ar condicionado e internet",
                BedroomCount = 0,
                GarageSpots = 1,
                BathroomCount = 1,
                Price = 1200.00m,
                ForSale = false,
                Category = categorias.First(c => c.Id == 4),
                Address = new Address
                {
                    Id = 6,
                    Street = "Rua Comercial, 555",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-004",
                    Address_Type = "Comercial"
                }
            },
            new Property
            {
                Id = 8,
                Number = 451,
                Name = "Galp�o Industrial Zona Norte",
                Description = "Galp�o de 500m� com p�-direito alto, escrit�rio e p�tio para manobras",
                BedroomCount = 0,
                GarageSpots = 10,
                BathroomCount = 2,
                Price = 4500.00m,
                ForSale = false,
                Category = categorias.First(c => c.Id == 5),
                Address = new Address
                {
                    Id = 8,
                    Street = "Rua Industrial, 200",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-006",
                    Address_Type = "Industrial"
                }
            },
            new Property
            {
                Id = 9,
                Number = 253,
                Name = "Terreno Residencial Plano",
                Description = "Terreno plano de 450m� em loteamento aprovado, com toda infraestrutura",
                BedroomCount = 0,
                GarageSpots = 0,
                BathroomCount = 0,
                Price = 120000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 6),
                Address = new Address
                {
                    Id = 9,
                    Street = "Rua Projetada A, Lote 15",
                    City = "Videira",
                    State_Province = "SC",
                    Country = "Brasil",
                    Postal_Code = "89560-007",
                    Address_Type = "Residencial"
                }
            }
        });
    }
}

static void InicializarClientes()
{
    if (ClientData.Clients.Count == 0)
    {
        ClientData.Clients.AddRange(new List<Client>
        {
            new Client
            {
                Id = 1,
                Name = "George Russhel",
                Email = "george.russhel@email.com",
                Phone = "47999998888",
                CPF = "123.456.789-00",
                InterestedProperties = new List<Property>()
            },
            new Client
            {
                Id = 2,
                Name = "Jean Grey",
                Email = "jean.grey@email.com",
                Phone = "47988887777",
                CPF = "987.654.321-00",
                InterestedProperties = new List<Property>()
            },
            new Client
            {
                Id = 3,
                Name = "Kurt Wagner",
                Email = "kurt.wagner@email.com",
                Phone = "47977776666",
                CPF = "456.789.123-00",
                InterestedProperties = new List<Property>()
            }
        });
    }
}