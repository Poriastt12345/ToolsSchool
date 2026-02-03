namespace ToolsSchool.Entities
{
    public class QualityLevel
    {
        public int id { get; set; }
        public int level { get; set; }
        public string Name {  get; set; }=string.Empty;

        public List<Tools> Tools { get; set; }=new List<Tools>();
    }
}
