using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tuhoc.Models;

namespace Tuhoc.Controllers
{
    public class BooksController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: Books
        public ActionResult Index(string Sorting_Order,string Search_Data,string Filter_Values,int? PageNo)
        {
            ViewBag.CurrentSort_Order = Sorting_Order;
            ViewBag.Sorting_Name = String.IsNullOrEmpty(Search_Data) ? "Name_Description" : "";
            ViewBag.Sorting_Author = String.IsNullOrEmpty(Search_Data) ? "Author_Description" : "";
            ViewBag.Sorting_Publisher = String.IsNullOrEmpty(Search_Data) ? "Publisher_Description" : "";

            if (Search_Data != null)
            {
                PageNo = 1;
            }
            else {
                Search_Data = Filter_Values;
            }

            ViewBag.Filter_Data = Search_Data;

            var books = from b in db.Books select b;

            if (!String.IsNullOrEmpty(Search_Data)) {
                books = books.Where(b => b.BookName.Contains(Search_Data));
            }

            switch(Sorting_Order)
            {
                case "Name_Description":
                    books = books.OrderByDescending(b => b.BookName); 
                    break;
                case "Author_Description":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "Publisher_Description":
                    books = books.OrderBy(b => b.Publisher);
                    break;
                default:
                    books = books.OrderBy(b => b.BookName);
                    break;
            }

            int Size_Of_Page = 4;
            int No_Of_Page = (PageNo ?? 1);
            return View(books.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookName,Author,Publisher,Year,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookName,Author,Publisher,Year,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
