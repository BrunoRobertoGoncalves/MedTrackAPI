using AutoMapper;
using MedTrackAPI.DTOs;
using MedTrackAPI.Models;

namespace MedTrackAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CreatePacienteDTO, Paciente>();

            CreateMap<Paciente, PacienteDTO>();
        }
    }
}
