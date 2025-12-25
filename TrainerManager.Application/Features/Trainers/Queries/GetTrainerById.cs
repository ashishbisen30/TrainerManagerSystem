using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities;
using TrainerManager.Infrastructure.Data;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    
    public record GetTrainerByIdQuery(int Id) : IRequest<TrainerDetailsDto?>;

    public class GetTrainerByIdHandler(TrainerDbContext context, IMapper mapper)
    : IRequestHandler<GetTrainerByIdQuery, TrainerDetailsDto?>
    {
        public async Task<TrainerDetailsDto?> Handle(GetTrainerByIdQuery request, CancellationToken ct)
        {
            var trainer = await context.Trainers
                .Include(t => t.Certifications)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (trainer == null) return null;

            // Use AutoMapper to convert Entity to DTO
            return mapper.Map<TrainerDetailsDto>(trainer);
        }

        /*
        public async Task<TrainerDetailsDto?> Handle(GetTrainerByIdQuery request, CancellationToken ct)
        {
            var trainer = await context.Trainers
                .Include(t => t.Certifications)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (trainer == null) return null;

            return new TrainerDetailsDto
            {
                Id = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                MobileNumber = trainer.MobileNumber,
                LinkedInUrl = trainer.LinkedInUrl,
                IdentityNumber = trainer.IdentityNumber,
                Field = trainer.Field,
                Specialization = trainer.Specialization,
                YearsOfExperience = trainer.YearsOfExperience,
                LastCompanyName = trainer.LastCompanyName,
                ProfileImagePath = trainer.ProfileImagePath,
                ResumePath = trainer.ResumePath,

                // Grouped Mapping (Solves the "Cannot implicitly convert" error)
                Address = new AddressDto
                {
                    Street = trainer.Address?.Street ?? "",
                    City = trainer.Address?.City ?? "",
                    State = trainer.Address?.State ?? "",
                    Zip = trainer.Address?.Zip ?? "",
                    Country = trainer.Address?.Country ?? ""
                },
                Costing = new CostingDto
                {
                    HourlyRate = trainer.Costing?.HourlyRate ?? 0,
                    Currency = trainer.Costing?.Currency ?? "INR"
                },
                AccountDetails = new AccountDto
                {
                    BankName = trainer.AccountDetails?.BankName,
                    AccountNumber = trainer.AccountDetails?.AccountNumber
                },

                // Flat Visa mapping
                VisaType = trainer.Visa?.VisaType,
                VisaCountry = trainer.Visa?.Country,
                VisaExpiry = trainer.Visa?.ExpiryDate,

                Certifications = trainer.Certifications.Select(c => new CertificationDto
                {
                    Name = c.Name,
                    IssuingOrganization = c.IssuingOrganization,
                    DateObtained = c.DateObtained
                }).ToList()
            };
        }

        */
    }
     
}
