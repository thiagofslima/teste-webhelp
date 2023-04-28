using AutoMapper;
using CoreApi.Data.Dtos;
using CoreApi.Models;

namespace CoreApi.Profiles;

public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<Vaga, VagaDto>().
            ForMember(vagaDto => vagaDto.Candidatos,
                opt => opt.MapFrom(vaga => vaga.Candidatos));
        CreateMap<VagaDto, Vaga>().
            ForMember(vaga => vaga.Candidatos,
                opt => opt.MapFrom(vagadto => vagadto.Candidatos));

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Usuario>();

        CreateMap<Empresa, EmpresaDto>()
            .ForMember(empresaDto => empresaDto.Tecnologias,
                    opt => opt.MapFrom(empresa => empresa.Tecnologias));
        CreateMap<EmpresaDto, Empresa>().
            ForMember(empresa => empresa.Tecnologias,
                opt => opt.MapFrom(empresaDto => empresaDto.Tecnologias));

        CreateMap<Tecnologia, TecnologiaDto>()
            .ForMember(tecnologiaDto => tecnologiaDto.Empresas,
                    opt => opt.MapFrom(tecnologia => tecnologia.Empresas));
        CreateMap<TecnologiaDto, Tecnologia>()
            .ForMember(tecnologia => tecnologia.Empresas,
                    opt => opt.MapFrom(tecnologiaDto => tecnologiaDto.Empresas));
    }
}
