namespace Domain.Exceptions;

public class ValidationException(string message) : AppException(message, 400);
