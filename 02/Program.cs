using _02;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var service = new NotesService();

app.MapGet("/api/notes", (HttpQuery filters) =>
    {
        var notes = service.GetNotes();
        if (filters.HasFilters())
        {
            notes = service.Filter(filters.GetFilters());
        }

        notes = NotesService.Paginate(notes, filters.Page, filters.PageSize);

        return notes.ToArray();
    })
    .WithName("GetNotes");

app.MapGet("/api/notes/{guid:guid}", (Guid guid) =>
    {
        var note = service.ByGuid(guid);

        if (note == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(note);
    })
    .WithName("GetNote");

app.MapPost("/api/notes", (Note note) =>
{
    if (string.IsNullOrWhiteSpace(note.Title))
    {
        return Results.BadRequest("Title is required");
    }

    service.AddNote(note);

    return Results.Created($"/api/notes/{note.Id}", note);
}).WithName("CrateNote");

app.MapPut("/api/notes/{guid:guid}", (Guid guid, Note updatedNote) =>
    {
        var note = service.UpdateNote(guid, updatedNote);

        if (note == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(note);
    })
    .WithName("UpdateNote");

app.MapDelete("/api/notes/{guid:guid}", (Guid guid) =>
    {
        service.DeleteNote(guid);

        return Results.NoContent();
    })
    .WithName("DeleteNote");

app.Run();