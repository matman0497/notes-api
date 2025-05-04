namespace _02;

public class NotesService
{
    List<Note> _notes = new List<Note>();

    public List<Note> GetNotes() => _notes;

    public Note? ByGuid(Guid id)
    {
        try
        {
            return _notes.First(n => n.Id == id);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }


    public Note AddNote(Note note)
    {
        _notes.Add(note);

        return note;
    }

    public Note? UpdateNote(Guid guid, Note updateNote)
    {
        var note = ByGuid(guid);

        if (note == null)
        {
            return null;
        }

        note.Title = updateNote.Title;
        note.Content = updateNote.Content;
        note.UpdatedAt = DateTime.UtcNow;

        return note;
    }

    public void DeleteNote(Guid guid)
    {
        var note = ByGuid(guid);

        if (note == null)
        {
            return;
        }

        _notes.Remove(note);
    }

    public List<Note> Filter(List<QueryFilter> filters)
    {
        var filtered = _notes;
        foreach (var filter in filters)
        {
            if (filter.Key == "title")
            {
                filtered = _notes.Where(n => n.Title.StartsWith(filter.Value)).ToList();
            }
            
            if (filter.Key == "content")
            {
                filtered = _notes.Where(n => n.Title.Contains(filter.Value)).ToList();
            }
        }

        return filtered;
    }
    
    public static List<Note> Paginate(List<Note> notes, int page, int pageSize)
    {
        return notes.Skip(page*pageSize).Take(pageSize).ToList();
    }
}