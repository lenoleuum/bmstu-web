namespace mbti_web.Mappers
{
    public class TypesDict
    {
        private Dictionary<int, string> types = new Dictionary<int, string>()
        {
            { 0, ""},
            { 1, "ENTP"},
            { 2, "ENTJ"},
            { 3, "INTJ"},
            { 4, "INTP"},
            { 5, "ENFP"},
            { 6, "ENFJ"},
            { 7, "INFP"},
            { 8, "INFJ"},
            { 9, "ESFJ"},
            { 10, "ESTJ"},
            { 11, "ISFJ"},
            { 12, "ISTJ"},
            { 13, "ESFP"},
            { 14, "ESTP"},
            { 15, "ISFP"},
            { 16, "ISTP"}
        };

        public int getTypeByStr(string _type)
        {
            return types.FirstOrDefault(x => x.Value == _type).Key;
        }
        public string getTypeById(int _id)
        {
            return this.types[_id];
        }
    }
}
