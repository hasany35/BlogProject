using BlogProject_5175.Models.Entities.Concrete;
using BlogProject_5175.Models.EntityTypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BlogProject_5175.Models.EntityTypeConfigurations.Concrete
{
   public class ArticleMap :BaseMap<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(220).IsRequired(true);
            builder.Property(a => a.Content).IsRequired(true);
            builder.Property(a => a.Image).IsRequired(true);

            builder.HasOne(a => a.AppUser).WithMany(a => a.Articles).HasForeignKey(a => a.AppUserID).OnDelete(DeleteBehavior.Restrict);
            // deleteBehavir.RESTRİCT => ebeveyn- çocuk (parent child) ilişkisi gibi düşünebilirz. Yani makale silindiğinde sıkıntı yok ama makalenin sahibi olan  userı silinmeye kalktığnda hata verir.Makalleri silebiirsiniz ama silmeye kalktığunız userın makaleleri varsa o userı slemezsiniz çünkü onun makaleleri usersız olamaz.

            builder.HasOne(a => a.Category).WithMany(a => a.Articles).HasForeignKey(a => a.CategoryID).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
