using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using AutoMapper;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {          
            //Transferencia
            CreateMap<TransferenciaModel, TransferenciaInclui>()
                .ForMember(x => x.CdTipoCliente, opt => opt.MapFrom(y => y.TipoContaFavorecido))
                .ForMember(x => x.CdUsuario, opt => opt.MapFrom(y => y.CodCliente))
                .ForMember(x => x.IdTransacao, opt => opt.MapFrom(y => ""))
                .ForMember(x => x.CdAgenciaDebito, opt => opt.MapFrom(y => y.AgenciaCliente))
                .ForMember(x => x.DcContaDebito, opt => opt.MapFrom(y => y.ContaCliente))
                .ForMember(x => x.VlValor, opt => opt.MapFrom(y => y.Valor))
                .ForMember(x => x.FlAprovado, opt => opt.MapFrom(y => ""))
                .ForMember(x => x.CdBancoCred, opt => opt.MapFrom(y => y.BancoFavorecido))
                .ForMember(x => x.CdAgenciaCred, opt => opt.MapFrom(y => y.AgenciaFavorecido))
                .ForMember(x => x.DcContaCred, opt => opt.MapFrom(y => y.ContaFavorecido))
                .ForMember(x => x.CdTipoContaCred, opt => opt.MapFrom(y => y.TipoContaFavorecido))
                .ForMember(x => x.CdCnpjCpfCliCred, opt => opt.MapFrom(y => y.NumDocumentoFavorecido))
                .ForMember(x => x.DcNomeCliCred, opt => opt.MapFrom(y => y.NomeFavorecido))
                .ForMember(x => x.CdCnpjCpfCliCred2, opt => opt.MapFrom(y => y.NumDocumentoFavorecido2))
                .ForMember(x => x.DcNomeCliCred2, opt => opt.MapFrom(y => y.NomeFavorecido2))
                .ForMember(x => x.DtTransferencia, opt => opt.MapFrom(y => y.DataTransacao))
                .ForMember(x => x.CdProtocoloApi, opt => opt.MapFrom(y => ""))
                ;
        }
    }
}