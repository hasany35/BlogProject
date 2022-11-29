namespace BlogProject_5175.Models.Entities.Concrete
{
    public class UserFollowedCategory
    {
        public int AppUserID { get; set; }

        public AppUser  AppUser { get; set; }


        public int CategoryID { get; set; }

        public Category  Category { get; set; }
    }
}