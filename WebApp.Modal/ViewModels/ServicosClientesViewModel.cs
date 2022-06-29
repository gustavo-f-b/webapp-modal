using System.ComponentModel.DataAnnotations;

namespace WebApp.Modal.ViewModels
{
    public class ServicosClientesViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ServicoId { get; set; }

        public virtual ClienteViewModel Cliente { get; set; } = null!;
        public virtual ServicoViewModel Servico { get; set; } = null!;
    }
}
