namespace mbti_web.Models
{
#nullable disable
    public class TypeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeModel() { }
        public TypeModel(int _id, string _name, string _description)
        {
            ID = _id;
            Name = _name;
            Description = _description;
        }
    }
}
