namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }        
        public Evento Evento { get; } //IMPORTANTE TER SÓ GET PRA NN FICAR DANDO LOOP INFINITO NA HR DO POST NO BANCO DE DADOS
        public int? PalestranteId { get; set; }
        public Palestrante Palestrante { get; } //IMPORTANTE TER SÓ GET PRA NN FICAR DANDO LOOP INFINITO NA HR DO POST NO BANCO DE DADOS
    }
}