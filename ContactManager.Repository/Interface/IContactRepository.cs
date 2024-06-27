using ContactManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Repository.Interface
{
    public interface IContactRepository : IRepository<Contact>
    {
    }
}
