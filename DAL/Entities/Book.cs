using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool AllowBorrow { get; set; }
    }
}
