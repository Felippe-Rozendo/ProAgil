namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; } 
    }
}