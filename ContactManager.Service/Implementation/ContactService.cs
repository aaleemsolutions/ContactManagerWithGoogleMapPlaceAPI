using ContactManager.Common.Helper;
using ContactManager.Common.Model;
using ContactManager.Repository.Entities;
using ContactManager.Repository.Interface;
using ContactManager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Service.Implementation
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        public async Task<ContactDTO> AddOrUpdateService(ContactDTO model)
        {
            Contact contactEntity = model.ProjectTo<Contact>();
            if (contactEntity.Id>0)//Update
                await _contactRepository.UpdateAsync(contactEntity);
            else//Insert
                await _contactRepository.AddAsync(contactEntity);


            return model;
   
        }

        public  async Task<IEnumerable<ContactDTO>> GetAll()
        {
            List<Contact> contact = await _contactRepository.GetAllAsync();
            IEnumerable<ContactDTO> model = contact.ProjectTo<ContactDTO>();
            return model;

        }

        public async Task<ContactDTO> GetById(int Id)
        {
            Contact contact = await _contactRepository.GetByIdAsync(Id);
            ContactDTO model = contact.ProjectTo<ContactDTO>();

            return model;
        }

        public async Task<bool> DeleteContact(int Id)
        {
            var entity = await _contactRepository.GetByIdAsync(Id);

            await _contactRepository.DeleteAsync(entity);
            return true;
        }
    }
}
