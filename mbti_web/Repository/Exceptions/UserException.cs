namespace mbti_web.Repository.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string _message)
            : base("[UserException] " + _message) { }
    }
    public class UserAddException : UserException
    {
        public UserAddException(int _id)
            : base("User with Id " + _id.ToString() + " already exists!") { }
        public UserAddException(string _login)
            : base("User with Login " + _login + " already exists!") { }
    }
    public class UserUpdateException : UserException
    {
        public UserUpdateException(int _id)
            : base("User with Id " + _id.ToString() + " does not exists!") { }
    }
    public class UserDeleteException : UserException
    {
        public UserDeleteException(int _id)
            : base("User with Id " + _id.ToString() + " does not exists!") { }
    }
    public class UserNotFoundException : UserException
    {
        public UserNotFoundException(int _id)
            : base("User with Id " + _id.ToString() + " is not found!") { }
        public UserNotFoundException(string _login)
            : base("User with Login " + _login + " is not found!") { }
    }
}
