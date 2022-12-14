namespace mbti_web.Repository.Exceptions
{
    public class TypeException : Exception
    {
        public TypeException(string _message)
            : base("[TypeException] " + _message) { }
    }

    public class TypeUpdateException : TypeException
    {
        public TypeUpdateException(int _id)
            : base("Type with Id " + _id.ToString() + " does not exist!") { }
    }
    public class TypeNotFoundException : TypeException
    {
        public TypeNotFoundException(int _id)
            : base("Type with Id " + _id.ToString() + " is not found!") { }
        public TypeNotFoundException(string _name)
            : base("Type with Name " + _name + " is not found!") { }
    }
}
