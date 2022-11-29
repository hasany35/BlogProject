using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.EntityTypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.Models.EntityTypeConfigurations.Concrete
{
    // DİKKAT =>  Like sınıfı BaseEntityden kalıtım almadığı için, BaseMapten de kalıtım almamalı bu yüzden kendiisi doğrudan IEntityTypeCnfigurationdan kalıtım alıp CONFIGURE metotunu implemente ediyor.
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(a=> new {a.ArticleID,a.AppUserID });
            // primary key ve foreign key gibi not null alanlar.
        }
    }
}
