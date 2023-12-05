using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Loan
    {
        public ObjectId Id { get; set; }
        public ObjectId BorrowerId { get; set; }
        public ObjectId BookId { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
