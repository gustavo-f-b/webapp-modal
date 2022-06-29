using System;
using System.Collections.Generic;

namespace WebApp.Modal.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ServicosClientes = new HashSet<ServicosCliente>();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }

        public virtual IEnumerable<ServicosCliente> ServicosClientes { get; set; }
    }
}
