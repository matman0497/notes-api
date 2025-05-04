namespace Tests;

using _02;

public class NoteServiceTest
{
    [Fact]
    public void TestAddNote()
    {
        var noteService = new NotesService();
        var note = new Note() { Title = "Title", Content = "Content" };

        noteService.AddNote(note);

        Assert.NotEmpty(noteService.GetNotes());
    }

    [Fact]
    public void TestUpdateNote()
    {
        var noteService = new NotesService();
        var note = new Note { Title = "Title", Content = "Content" };
        noteService.AddNote(note);

        var updatedNote = noteService.UpdateNote(note.Id, new Note { Title = "NewTitle" });

        Assert.Same("NewTitle", updatedNote?.Title);
    }

    [Fact]
    public void TestGetByGuid()
    {
        var noteService = new NotesService();
        var note = new Note { Title = "Title", Content = "Content" };
        noteService.AddNote(note);

        Assert.NotNull(noteService.ByGuid(note.Id));
    }
    
    
    [Fact]
    public void TestGetByGuidReturnsNullIfNotFound()
    {
        var noteService = new NotesService();
        var note = new Note { Title = "Title", Content = "Content" };
        noteService.AddNote(note);

        Assert.Null(noteService.ByGuid(Guid.NewGuid()));
    }
    
    [Fact]
    public void TestDeleteNote()
    {
        var noteService = new NotesService();
        var note = new Note { Title = "Title", Content = "Content" };
        noteService.DeleteNote(note.Id);

        Assert.Null(noteService.ByGuid(note.Id));
    }
}