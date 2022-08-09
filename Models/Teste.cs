using System;

namespace MeuTeste.Models
{
    public class Teste
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;

    }
}
