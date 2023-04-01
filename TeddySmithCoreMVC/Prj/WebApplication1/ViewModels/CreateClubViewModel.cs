using WebApplication1.Data.Enum;

namespace WebApplication1.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; } 
        public IFormFile Image { get; set; }   
        public ClubCategory clubCategory { get; set; }  
    }
}
