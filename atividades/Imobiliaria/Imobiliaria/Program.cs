using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
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
            new Category { Id = 1, Name = "Apartamento", Description = "Unidade residencial em prédio" },
            new Category { Id = 2, Name = "Casa", Description = "Residência com quintal privativo" },
            new Category { Id = 3, Name = "Sítio/Fazenda", Description = "Propriedade rural para lazer ou agricultura" },
            new Category { Id = 4, Name = "Escritório Comercial", Description = "Espaço para uso empresarial" },
            new Category { Id = 5, Name = "Galpão", Description = "Área ampla para armazenamento ou uso industrial" },
            new Category { Id = 6, Name = "Terreno", Description = "Lote urbano ou rural para construção" }
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
                Description = "Apartamento moderno com 3 quartos (1 suíte), sala ampla e cozinha americana",
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
                Description = "Apartamento no centro com 2 quartos, área de serviço e varanda",
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
                Name = "Casa Familiar Jardim América",
                Description = "Casa espaçosa com 4 quartos, sala, copa, cozinha e área gourmet",
                BedroomCount = 4,
                GarageSpots = 3,
                BathroomCount = 3,
                Price = 680000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 2),
                Address = new Address
                {
                    Id = 3,
                    Street = "Rua dos Ipês, 789",
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
                Description = "Casa térrea com 2 quartos, quintal amplo e área de churrasco",
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
                Name = "Sítio Retiro Verde",
                Description = "Sítio com 5 hectares, casa principal com 3 quartos, pomar e área para animais",
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
                Name = "Escritório Comercial Centro",
                Description = "Escritório comercial de 45m² com ar condicionado e internet",
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
                Name = "Galpão Industrial Zona Norte",
                Description = "Galpão de 500m² com pé-direito alto, escritório e pátio para manobras",
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
                Description = "Terreno plano de 450m² em loteamento aprovado, com toda infraestrutura",
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