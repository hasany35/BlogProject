namespace BlogProject_5175.Models.Entities.Concrete
{
    public class Like
    {
        // BaseEntityden kalıtım almamaktadır çünkü beğeni için crud operasyonlarının tümü geçerli değildir.

        // LİKE KİME AİT?
        public int AppUserID { get; set; }

        public AppUser  AppUser { get; set; }

        // LİKE HANGİ MAKALEYE AİT?
        public int ArticleID { get; set; }

        public Article  Article { get; set; }
    }
}