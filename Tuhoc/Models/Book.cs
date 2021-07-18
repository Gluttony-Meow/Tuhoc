using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tuhoc.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required, DisplayName("Tiêu đề")]
        public string BookName { get; set; }
        [Required, DisplayName("Tác giả")]
        public string Author { get; set; }
        [Required, DisplayName("NXB")]
        public string Publisher { get; set; }
        [Required, Range(1990, int.MaxValue), DisplayName("Năm Xuất Bản")]
        public int Year { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
    }
}