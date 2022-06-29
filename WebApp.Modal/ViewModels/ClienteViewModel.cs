using System.ComponentModel.DataAnnotations;

namespace WebApp.Modal.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            ServicosClientes = new HashSet<ServicosClientesViewModel>();
        }

        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }

        public virtual IEnumerable<ServicosClientesViewModel> ServicosClientes { get; set; }
    }
}
