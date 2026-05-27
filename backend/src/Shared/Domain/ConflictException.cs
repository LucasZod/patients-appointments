namespace Backend.Shared.Domain;

public class ConflictException(string message) : Exception(message);
