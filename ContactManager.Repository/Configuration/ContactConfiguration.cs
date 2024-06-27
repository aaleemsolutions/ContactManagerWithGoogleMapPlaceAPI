using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Repository.Entities;

namespace ContactManager.Repository.Configuration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDateTime).HasDefaultValueSql("getdate()");

            builder.Property(x => x.Status).HasDefaultValueSql("1").ValueGeneratedNever();

            builder.Property(x => x.CreatedBy).HasMaxLength(500);
            builder.Property(x => x.CreatedBy).IsRequired(true);


            builder.Property(x => x.FirstName).HasMaxLength(200);
            builder.Property(x => x.LastName).HasMaxLength(200);

            builder.Property(x => x.Email).HasMaxLength(500);
            



        }
    }
}
