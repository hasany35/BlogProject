using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.EntityTypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_5175.Models.EntityTypeConfigurations.Concrete
{
    public class CategoryMap : BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(a => a.Name).IsRequired(true);
            builder.Property(a => a.Description).IsRequired(true);

            base.Configure(builder);
        }

    }
}
