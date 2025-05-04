namespace _02;

public class NotesService
{
    List<Note> _notes = new List<Note>();

    public List<Note> GetNotes() => _notes;

    public List<Note> ByGuid(Guid id) => _notes.Where(n => n.Id == id).ToList();

    public Note AddNote(Note note)
    {
        _notes.Add(note);

        return note;
    }
}