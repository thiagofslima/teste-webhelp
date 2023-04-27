using AutoMapper;
using CoreApi.Data.Dtos;
using CoreApi.Models;

namespace CoreApi.Profiles;

public class CoreProfile : Profile
{
    public CoreProfile()
    {
        CreateMap<CreateVagaDto, Vaga>();
        CreateMap<UpdateVagaDto, Vaga>();
        CreateMap<Vaga, UpdateVagaDto>();
        CreateMap<Vaga, ReadVagaDto>();

        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Usuario>();

        CreateMap<Empresa, EmpresaDto>()
            .ForMember(empresaDto => empresaDto.Tecnologias,
                    opt => opt.MapFrom(empresa => empresa.Tecnologias)); ;
        CreateMap<EmpresaDto, Empresa>();

        CreateMap<Tecnologia, TecnologiaDto>()
            .ForMember(tecnologiaDto => tecnologiaDto.Empresas,
                    opt => opt.MapFrom(tecnologia => tecnologia.Empresas)); ;
        CreateMap<TecnologiaDto, Tecnologia>();
    }
}
