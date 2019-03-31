using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public UserConfig()
        {
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder
                .Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(u => u.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(u => u.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired(true);

            builder
                .Property(u => u.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsRequired(true);

        }
    }
}


        //◦ FirstName(up to 50 characters, unicode)
        //◦ LastName(up to 50 characters, unicode)
        //◦ Email(up to 80 characters, non-unicode)
        //◦ Password(up to 25 characters, non-unicode)