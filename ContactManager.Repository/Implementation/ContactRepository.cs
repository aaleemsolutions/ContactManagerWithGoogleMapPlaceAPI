using ContactManager.Repository.Database;
using ContactManager.Repository.Entities;
using ContactManager.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Repository.Implementation
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(ContactDbContext context) : base(context)
        {
        }

     }
}
