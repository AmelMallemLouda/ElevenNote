using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;//application user Id(we'll get whatever the user who's loged in has created).

        public NoteService(Guid userId)// Take the value of the field
        {
            _userId = userId;
        }

        //This will create an instance of Note
        public bool CreateNote(NoteCreate model)//We don't have to create an Id The service and Data layer will work on that.
        {
            var entity =
                new Note()// table of data that holds notes, and I am crearting a new one
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId=model.CategoryId,// Added after having Category
                    CreatedUtc = DateTimeOffset.Now,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);// Add our entity to the note table
                return ctx.SaveChanges() == 1;//the number of changes that are gonna be made to the database(we only chaged the note table=1)
            }
        }
        //This method will allow us to see all the notes that belong to a specific user.

        public IEnumerable<NoteListItem> GetNotes()//Ienumerable is a collection of Items
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx//database
                        .Notes
                        .Where(e => e.OwnerId == _userId)// filter  the database and get only the notes that I have created as a user because I am the one who's loged in . e is for entity
                        .Select(// Select a Note database
                            e =>// declaring a variable of type note and goes to NoteListItem
                                new NoteListItem
                                {
                                    NoteId = e.NoteId,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc,
                                    CategoryName= e.Category.CategoryName, //Access Category Table then select Category name
                                }
                        );

                return query.ToArray();
            }
        }
        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        //.Single to make sure there is one owner that is == to user id and one noteId that has the Id 
                        .Single(e => e.NoteId == id && e.OwnerId == _userId); // The ownerId should match the userId and noteId shd match Id
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CategoryName=entity.Category.CategoryName,//Added after having category
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.CategoryId = model.CategoryId;// Added after having category
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteNote(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
