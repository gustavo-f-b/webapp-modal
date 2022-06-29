using AutoMapper;
using WebApp.Modal.Models;
using WebApp.Modal.ViewModels;

namespace WebApp.Modal.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Servico, ServicoViewModel>().ReverseMap();
            CreateMap<ServicosCliente, ServicosClientesViewModel>().ReverseMap();
        }
    }
}
