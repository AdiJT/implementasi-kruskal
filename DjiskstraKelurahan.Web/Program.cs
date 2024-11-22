using DjiskstraKelurahan.Web.Models;
using DjiskstraKelurahan.Web.Services;
using Kruskal.Core;
using Microsoft.AspNetCore.Http.HttpResults;

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

app.MapGet("/kelurahans/edges", (IKelurahanService kelurahanService) =>
{
    return Results.Json<List<Edge<Kelurahan>>>(kelurahanService.GetGraph().EdgesDistinct.ToList());
});

app.MapPost("/kelurahans/path", (IKelurahanService kelurahanService, PathRequest pathRequest) =>
{
    var graph = kelurahanService.GetGraph();
    var startKelurahan = kelurahanService.GetByName(pathRequest.Start);
    var endKelurahan = kelurahanService.GetByName(pathRequest.End);

    if (startKelurahan is null || endKelurahan is null)
        return Results.BadRequest();

    var result = graph.Djikstra(new Vertex<Kelurahan>(startKelurahan), new Vertex<Kelurahan>(endKelurahan));

    return Results.Json(new { result.cost, result.path });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
