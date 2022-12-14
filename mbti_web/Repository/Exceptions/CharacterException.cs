namespace mbti_web.Repository.Exceptions
{
    public class CharacterException : Exception
    {
        public CharacterException(string _message)
            : base("[CharacterException] " + _message) { }
    }
    public class CharacterNotFoundException : CharacterException
    {
        public CharacterNotFoundException(int _id)
            : base("Character with Id " + _id.ToString() + " is not found!") { }
    }

    // todo
    public class CharacterAddException : CharacterException
    {
        public CharacterAddException(int _id)
            : base("Character with Id " + _id.ToString() + " already exists!") { }
    }
    public class CharacterUpdateException : CharacterException
    {
        public CharacterUpdateException(int _id)
            : base("Character with Id " + _id.ToString() + " does not exists!") { }
    }
}
