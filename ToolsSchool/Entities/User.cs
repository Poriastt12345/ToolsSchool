namespace ToolsSchool.Entities
{
    public class User
    { 
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName {  get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public List<Tools> Tools { get; set; }=new  List<Tools>();
    }
}
