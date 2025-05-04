using _02;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var service = new NotesService();

app.MapGet("/api/notes", () => { return service.GetNotes().ToArray(); })
    .WithName("GetNotes");

app.MapGet("/api/notes/{guid:guid}", (Guid guid) => { return service.ByGuid(guid); })
    .WithName("GetNote");

app.MapPost("/api/notes", (Note note) => { service.AddNote(note); }).WithName("CrateNotes");
app.Run();