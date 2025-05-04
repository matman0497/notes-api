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
}