using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using AutoMapper;
using AutoMapper.Configuration;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings
{
    public class MappingProfile : Profile
    {
        #region Props
        private static IConfiguration _config;
        public static IConfiguration Config
        {
            get
            {
                return _config;
            }
            private set
            {
                _config = value;
            }
        }
        public static void SetConfig(IConfiguration config)
        {
            Config = config;
            if (MappingProfile.Mapper == null)
                _ = new MappingProfile();
        }

        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                return _mapper;
            }
            private set
            {
                _mapper = value;
            }
        }
        #endregion
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

            CreateMap<TransferenciaInclui, TransferenciasArquivo>()
                .ForMember(x => x.transferencia.TipoContaFavorecido, opt => opt.MapFrom(y => y.CdTipoCliente))
                .ForMember(x => x.transferencia.CodCliente, opt => opt.MapFrom(y => y.CdUsuario))
                .ForMember(x => x.transferencia.AgenciaCliente, opt => opt.MapFrom(y => y.CdAgenciaDebito))
                .ForMember(x => x.transferencia.ContaCliente, opt => opt.MapFrom(y => y.DcContaDebito))
                .ForMember(x => x.transferencia.Valor, opt => opt.MapFrom(y => y.VlValor))
                .ForMember(x => x.transferencia.BancoFavorecido, opt => opt.MapFrom(y => y.CdBancoCred))
                .ForMember(x => x.transferencia.AgenciaFavorecido, opt => opt.MapFrom(y => y.CdAgenciaCred))
                .ForMember(x => x.transferencia.NumDocumentoFavorecido, opt => opt.MapFrom(y => y.DcContaCred))
                .ForMember(x => x.transferencia.TipoContaFavorecido, opt => opt.MapFrom(y => y.CdTipoContaCred))
                .ForMember(x => x.transferencia.NumDocumentoFavorecido, opt => opt.MapFrom(y => y.CdCnpjCpfCliCred))
                .ForMember(x => x.transferencia.NomeFavorecido, opt => opt.MapFrom(y => y.DcNomeCliCred))
                .ForMember(x => x.transferencia.NumDocumentoFavorecido, opt => opt.MapFrom(y => y.CdCnpjCpfCliCred2))
                .ForMember(x => x.transferencia.NomeFavorecido, opt => opt.MapFrom(y => y.DcNomeCliCred2))
                .ForMember(x => x.transferencia.DataTransacao, opt => opt.MapFrom(y => y.DtTransferencia))
                .ForMember(x => x.root.Protocolo, opt => opt.MapFrom(y => y.CdProtocoloApi))
                ;

        }
    }
}