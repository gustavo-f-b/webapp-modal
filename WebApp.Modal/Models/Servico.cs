using System;
using System.Collections.Generic;

namespace WebApp.Modal.Models
{
    public partial class Servico
    {
        public Servico()
        {
            ServicosClientes = new HashSet<ServicosCliente>();
        }

        public int Id { get; set; }
        public string? Descricao { get; set; }

        public virtual IEnumerable<ServicosCliente> ServicosClientes { get; set; }
    }
}
