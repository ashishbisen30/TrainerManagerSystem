using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;

namespace TrainerManager.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 1. Map the small Helper Objects (Value Objects -> DTOs)
            CreateMap<Address, AddressDto>();
            CreateMap<TrainerCosting, CostingDto>();
            CreateMap<TrainerAccount, AccountDto>();

            // 2. THIS IS THE MISSING PIECE: Map Certification Entity -> DTO
            // Without this, the Trainer map fails when it hits the Certifications list
            CreateMap<TrainerCertification, CertificationDto>();

            // 3. Map the main Trainer Entity -> Details DTO
            CreateMap<Trainer, TrainerDetailsDto>()
                // Map Flat Visa fields
                .ForMember(d => d.VisaType, opt => opt.MapFrom(s => s.Visa.VisaType))
                .ForMember(d => d.VisaCountry, opt => opt.MapFrom(s => s.Visa.Country))
                .ForMember(d => d.VisaExpiry, opt => opt.MapFrom(s => s.Visa.ExpiryDate))

                // Map Grouped Objects
                // AutoMapper will use the maps defined in Step 1 & 2 automatically
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Costing, opt => opt.MapFrom(s => s.Costing))
                .ForMember(d => d.AccountDetails, opt => opt.MapFrom(s => s.AccountDetails))
                .ForMember(d => d.Certifications, opt => opt.MapFrom(s => s.Certifications));

            // 4. Map Trainer -> Summary DTO (For the Index/List Page)
            CreateMap<Trainer, TrainerSummaryDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.HourlyRate,
                    opt => opt.MapFrom(src => src.Costing.HourlyRate))
                .ForMember(dest => dest.Experience,
                    opt => opt.MapFrom(src => src.YearsOfExperience));
        }
         

    }
}
