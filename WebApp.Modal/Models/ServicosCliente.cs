using System;
using System.Collections.Generic;

namespace WebApp.Modal.Models
{
    public partial class ServicosCliente
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Servico Servico { get; set; } = null!;
    }
}
