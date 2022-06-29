using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Modal.ViewModels
{
    public class ServicoViewModel
    {
        public ServicoViewModel()
        {
            ServicosClientes = new HashSet<ServicosClientesViewModel>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        public virtual IEnumerable<ServicosClientesViewModel> ServicosClientes { get; set; }
    }
}
