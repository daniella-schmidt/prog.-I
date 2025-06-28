using Model;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

InitializeCategories(); 
InitializeProperties();
InitializeClients();

app.Run();

static void InitializeCategories()
{
    if (PropertyData.Categories.Count == 0)
    {
        PropertyData.Categories.AddRange(new List<Category>
        {
            new Category { Id = 1, Name = "Apartamento", Description = "Unidade residencial em edifício com múltiplos andares" },
            new Category { Id = 2, Name = "Casa", Description = "Residência térrea ou sobrado com área privativa" },
            new Category { Id = 3, Name = "Sítio", Description = "Propriedade rural para lazer e descanso" },
            new Category { Id = 4, Name = "Sala Comercial", Description = "Espaço comercial para estabelecimento de negócios" },
            new Category { Id = 5, Name = "Galpão", Description = "Espaço amplo para armazenamento ou indústria" },
            new Category { Id = 6, Name = "Terreno", Description = "Lote urbano ou rural para construção" }
        });
    }
}

static void InitializeProperties()
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
                Name = "Apartamento Residencial Park",
                Description = "Moderno apartamento com 3 quartos, sendo 1 suíte, sala ampla e cozinha americana",
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
                Name = "Apartamento Central Plaza",
                Description = "Apartamento no centro da cidade, 2 quartos, área de serviço e varanda",
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
                Description = "Casa ampla com 4 quartos, sala de estar, sala de jantar, cozinha e área gourmet",
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
                Name = "Casa Térrea Bela Vista",
                Description = "Casa térrea com 2 quartos, quintal amplo e churrasqueira",
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
                Name = "Sítio Recanto Verde",
                Description = "Sítio com 5 hectares, casa sede com 3 quartos, pomar e área para criação",
                BedroomCount = 3,
                GarageSpots = 4,
                BathroomCount = 2,
                Price = 850000.00m,
                ForSale = true,
                Category = categorias.First(c => c.Id == 3),
                Address = new Address
                {
                    Id = 5,
                    Street = "Estrada Rural SC-135, Km 8",
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
                Name = "Sala Comercial Centro Empresarial",
                Description = "Sala comercial de 45m² no centro empresarial, com ar condicionado e internet",
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
                Description = "Galpão de 500m² com pé direito alto, escritório administrativo e pátio de manobras",
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
                Description = "Terreno plano de 450m² em loteamento aprovado, com todas as infraestruturas",
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

static void InitializeClients()
{
    if (ClientData.Clients.Count == 0)
    {
        ClientData.Clients.AddRange(new List<Client>
        {
            new Client
            {
                Id = 1,
                Name = "João Silva",
                Email = "joao.silva@email.com",
                Phone = "47999998888",
                CPF = "123.456.789-00",
                InterestedProperties = new List<Property>()
            },
            new Client
            {
                Id = 2,
                Name = "Maria Oliveira",
                Email = "maria.oliveira@email.com",
                Phone = "47988887777",
                CPF = "987.654.321-00",
                InterestedProperties = new List<Property>()
            },
            new Client
            {
                Id = 3,
                Name = "Carlos Souza",
                Email = "carlos.souza@email.com",
                Phone = "47977776666",
                CPF = "456.789.123-00",
                InterestedProperties = new List<Property>()
            }
        });
    }
}
