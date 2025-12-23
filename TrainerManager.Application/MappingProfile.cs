using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities;

namespace TrainerManager.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add this line to disable the internal method scanning that causes the error
            //InternalApi.Internal(this).ForAllMaps((typeMap, mappingExpression) => { });
            // 1. THIS IS THE FIX: Disable the scanning of extension methods 
            // that causes the 'MaxInteger' verification exception.
            //ShouldMapMethod = (m => false);

            CreateMap<Trainer, TrainerSummaryDto>()
                // Combine FirstName and LastName into FullName
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                // Map HourlyRate from the nested Costing object
                .ForMember(dest => dest.HourlyRate,
                    opt => opt.MapFrom(src => src.Costing.HourlyRate))
                // Map YearsOfExperience to Experience
                .ForMember(dest => dest.Experience,
                    opt => opt.MapFrom(src => src.YearsOfExperience));

            // Inside your MappingProfile constructor
            CreateMap<UpdateTrainerCommand, Trainer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Don't overwrite the ID
                .ForPath(dest => dest.Costing.HourlyRate, opt => opt.MapFrom(src => src.Rate));
        }
    }
}
