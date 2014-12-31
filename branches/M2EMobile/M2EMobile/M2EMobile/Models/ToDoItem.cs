using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace M2EMobile.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TokenId { get; set; }
        public string UTMZK { get; set; }
        public string UTMZV { get; set; }
        public string UserType { get; set; }

    }
}
