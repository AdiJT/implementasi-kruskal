using DjiskstraKelurahan.Web.Models;
using DjiskstraKelurahan.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IKelurahanService, KelurahanService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/kelurahans", (IKelurahanService kelurahanService) => 
{
    var daftarKelurahan = kelurahanService.GetAll();
    return Results.Json<List<Kelurahan>>(daftarKelurahan);
});

app.MapGet("/kelurahans/{nama}", (IKelurahanService kelurahanService, string nama) =>
{
    var kelurahan = kelurahanService.GetByName(nama);
    if(kelurahan is null) return Results.NotFound();

    return Results.Json<Kelurahan>(kelurahan);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
