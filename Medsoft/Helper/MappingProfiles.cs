using AutoMapper;
using Medsoft.Dto;
using Medsoft.Models;

namespace Medsoft.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Sex, SexDto>();
            CreateMap<SexDto, Sex>();
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<PatientType, PatientTypeDto>();
            CreateMap<PatientTypeDto, PatientType>();
            CreateMap<Admission, AdmissionDto>();
            CreateMap<AdmissionDto, Admission>();
            CreateMap<PatientState, PatientStateDto>();
            CreateMap<PatientStateDto, PatientState>();
            CreateMap<Position, PositionDto>();
            CreateMap<PositionDto, Position>();
        }
    }
}
