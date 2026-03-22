namespace Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        private Email(string value)
        {
            Value = value;
        }
        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email es obligatorio.");

            if (!email.Contains("@"))
                throw new ArgumentException("Formato de email invalido.");

            return new Email(email);
        }
    }
}
