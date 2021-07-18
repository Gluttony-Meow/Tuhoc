using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tuhoc.Models
{
    public class ModelContext : DbContext
    {
        public ModelContext():base("BookManagement")
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}