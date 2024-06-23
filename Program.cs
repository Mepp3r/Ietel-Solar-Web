var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IVagasData, VagasSql>();
builder.Services.AddTransient<ICandidatoData, CandidatoSql>();
builder.Services.AddTransient<IOrcamentoData, OrcamentoSql>();
builder.Services.AddTransient<IAgendamentoData, AgendamentoSql>();
builder.Services.AddTransient<ILoginData, LoginSql>();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute("default", "/{controller=Informacao}/{action=Index}/{id?}/{admin?}");

app.Run();
