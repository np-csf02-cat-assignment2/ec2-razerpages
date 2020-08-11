using System;
using System.ComponentModel.DataAnnotations;

namespace MainService.Models
{
    public class ContactUs
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
