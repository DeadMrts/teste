using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Banco SQLite
builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite("Data Source=estacionamento.db"));

var app = builder.Build();


//entrada do veículo
app.MapPost("/api/entrada", (Veiculo v, AppDataContext ctx) =>
{
    v.HoraEntrada = DateTime.Now;
    v.Dentro = true;

    ctx.Veiculos.Add(v);
    ctx.SaveChanges();

    return Results.Created("", v);
});


//r saída
app.MapPost("/api/saida/{placa}", (string placa, AppDataContext ctx) =>
{
    var veiculo = ctx.Veiculos.FirstOrDefault(x => x.Placa == placa && x.Dentro);

    if (veiculo == null)
        return Results.NotFound("Veículo não encontrado no pátio.");

    veiculo.HoraSaida = DateTime.Now;
    veiculo.Dentro = false;

    ctx.SaveChanges();

    return Results.Ok(veiculo);
});


// todos os veículos
app.MapGet("/api/veiculos", (AppDataContext ctx) =>
    ctx.Veiculos.ToList()
);


// apenas os veículos estacionados
app.MapGet("/api/veiculos/dentro", (AppDataContext ctx) =>
    ctx.Veiculos.Where(v => v.Dentro).ToList()
);

app.Run();
