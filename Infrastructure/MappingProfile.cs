using MySqlBasicCore.Models;
using AutoMapper;

namespace MySqlBasicCore.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order_MasterViewModel, Order>()
                .ReverseMap();
            CreateMap<InvoiceViewModel, Invoice>()
              .ReverseMap();
            CreateMap<IndsellCompoViewModel_Datatable, IndsellCompoViewModel>()
            .ReverseMap();

        }
    }
}
