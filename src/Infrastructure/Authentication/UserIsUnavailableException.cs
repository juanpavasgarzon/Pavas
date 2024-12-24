namespace Infrastructure.Authentication;

internal class UserIsUnavailableException : Exception
{
    public UserIsUnavailableException() : base("User ID is unavailable")
    {
    }

    public UserIsUnavailableException(string message) : base(message)
    {
    }

    public UserIsUnavailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
