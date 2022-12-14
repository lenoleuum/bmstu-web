namespace mbti_web.Models
{
#nullable disable
    public class CharacterModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public CharacterModel() { }
        public CharacterModel(int _id, string _name, string _type, string _category)
        {
            ID = _id;
            Name = _name;
            Type = _type;
            Category = _category;
        }
    }
}
