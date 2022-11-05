using Microsoft.EntityFrameworkCore;
using TrabalhoPOO.Data;
using TrabalhoPOO.Services;
using TrabalhoPOO.Repositores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AtendenteServico>();
builder.Services.AddScoped<AtendenteRepositorio>();

builder.Services.AddScoped<ClienteServico>();
builder.Services.AddScoped<ClienteRepositorio>();

builder.Services.AddScoped<IngredienteServico>();
builder.Services.AddScoped<IngredienteRepositorio>();

builder.Services.AddScoped<CustoServico>();
builder.Services.AddScoped<CustoRepositorio>();

builder.Services.AddScoped<PedidoServico>();
builder.Services.AddScoped<PedidoRepositorio>();

builder.Services.AddScoped<ProdutoServico>();
builder.Services.AddScoped<ProdutoRepositorio>();

builder.Services.AddDbContext<ContextoBD>(
    options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("ConexaoBanco"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConexaoBanco"))
    )
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
