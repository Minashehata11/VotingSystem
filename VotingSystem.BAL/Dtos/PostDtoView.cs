using Microsoft.CodeAnalysis.Options;

namespace BLL.VotingSystem.Dtos
{
    public class PostDtoView
    {
        public string Qualfication { get; set; }
        public string FullName { get; set; }
        public int PostId { get; set; }
        public string Decription { get; set; }
        public string Image { get; set; }
    }
}