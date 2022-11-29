using BlogProject_5175.Models.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.Models.EntityTypeConfigurations.Abstract
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        // virtual olmalı ki diğer sınıflar istediği gibi şekilledirebilisn.
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(a=>a.ID);
            builder.Property(a => a.CreateDate).IsRequired(true);
            builder.Property(a => a.ModifiedDate).IsRequired(false);
            builder.Property(a => a.RemovedDate).IsRequired(false);    // nullable
            builder.Property(a => a.Statu).IsRequired(true);           // not null
        }
    }
}
