﻿using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data.EntityConfiguration
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {

        public CreditCardConfig()
        {

        }

        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(c => c.CreditCardId);

            builder
                .Property(c => c.Limit)
                .IsRequired(true);

           
        }
    }
}
