using ContactManager.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Service.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetAll();
        Task<ContactDTO> GetById(int Id);
        Task<bool> DeleteContact(int Id);
        Task<ContactDTO> AddOrUpdateService(ContactDTO model);
 
    }
}
