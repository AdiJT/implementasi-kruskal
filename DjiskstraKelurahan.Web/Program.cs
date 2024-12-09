using DjiskstraKelurahan.Web.Models;
using DjiskstraKelurahan.Web.Services;
using Kruskal.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.HttpOverrides;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IKelurahanService, KelurahanService>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

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
    return Results.Json(daftarKelurahan);
});

app.MapGet("/kelurahans/{nama}", (IKelurahanService kelurahanService, string nama) =>
{
    var kelurahan = kelurahanService.GetByName(nama);
    if(kelurahan is null) return Results.NotFound();

    return Results.Json(kelurahan);
});

app.MapGet("/kelurahans/edges", (IKelurahanService kelurahanService) =>
{
    return Results.Json(kelurahanService.GetGraph().EdgesDistinct.ToList());
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

app.MapPost("/kelurahans/final", (IKelurahanService kelurahanService, FinalRequest request) => 
{
    var start = kelurahanService.GetByName(request.Start);

    if (start is null) return Results.BadRequest();

    var daftarKelurahan = new List<Kelurahan>();
    foreach (var nama in request.DaftarKelurahan)
    {
        var kelurahan = kelurahanService.GetByName(nama);
        if (kelurahan is null) return Results.BadRequest();
        daftarKelurahan.Add(kelurahan);
    }

    var graph = kelurahanService.GetGraph();

    var startVertex = graph.Vertices.FirstOrDefault(v => v.Value == start)!;
    var daftarKelurahanVertex = daftarKelurahan.Select(k => graph.Vertices.FirstOrDefault(v => v.Value == k)!).ToList();

    var path = new List<Vertex<Kelurahan>>() { startVertex };
    var cost = 0d;

    while(daftarKelurahanVertex.Count > 0)
    {
        var result = graph.Djikstra(startVertex);
        var minHeapQueue = new MinHeapQueue<(Vertex<Kelurahan> end, double cost, List<Vertex<Kelurahan>> path), double>(r => r.cost, result);

        var minResult = minHeapQueue.Dequeue();
        while(!daftarKelurahanVertex.Contains(minResult.end) || path.Contains(minResult.end))
            minResult = minHeapQueue.Dequeue();

        path.AddRange(minResult.path.Take(new Range(1, Index.End)));
        daftarKelurahanVertex.Remove(minResult.end);
        cost += minResult.cost;
        startVertex = minResult.end;
    }

    return Results.Json(new { cost, path });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
