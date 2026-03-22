namespace FnTecnologia.Heiner.PruebaTecnica.API.Models
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? TraceId { get; set; }
    }

}
