using System.ComponentModel.DataAnnotations.Schema;

namespace ToolsSchool.Entities
{
    public class Tools
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public float Price {  get; set; }

        [ForeignKey(nameof(Quality))]
        public int QualityID {  get; set; }
        public QualityLevel Quality { get; set; } = new();

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public User User { get; set; }=new User();
    }
}
