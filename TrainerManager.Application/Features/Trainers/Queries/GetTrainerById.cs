using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities;
using TrainerManager.Infrastructure.Data;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    // We return the DTO, not the Entity
    //public record GetTrainerByIdQuery(int Id) : IRequest<TrainerSummaryDto>;
    // Change this in your Queries folder
    //public record GetTrainerByIdQuery(int Id) : IRequest<Trainer?>;
    // This MUST return TrainerDetailsDto? to match your Handler
    public record GetTrainerByIdQuery(int Id) : IRequest<TrainerDetailsDto?>;

    public class GetTrainerByIdHandler(TrainerDbContext context)
    : IRequestHandler<GetTrainerByIdQuery, TrainerDetailsDto?>
    {
        public async Task<TrainerDetailsDto?> Handle(GetTrainerByIdQuery request, CancellationToken ct)
        {
            var trainer = await context.Trainers
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (trainer == null) return null;

            return new TrainerDetailsDto
            {
                Id = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                Field = trainer.Field,
                Specialization = trainer.Specialization,
                YearsOfExperience = trainer.YearsOfExperience,
                LastCompanyName = trainer.LastCompanyName,
                ProfileImagePath = trainer.ProfileImagePath,
                ResumePath = trainer.ResumePath,

                // Mapping the Value Objects to DTOs
                Address = new AddressDto
                {
                    Street = trainer.Address.Street,
                    City = trainer.Address.City,
                    State = trainer.Address.State,
                    Zip = trainer.Address.Zip,
                    Country = trainer.Address.Country
                },
                Costing = new CostingDto
                {
                    HourlyRate = trainer.Costing.HourlyRate,
                    Currency = trainer.Costing.Currency
                },
                AccountDetails = new AccountDto
                {
                    BankName = trainer.AccountDetails?.BankName,
                    AccountNumber = trainer.AccountDetails?.AccountNumber
                }
            };
        }

        //public async Task<Trainer?> Handle(GetTrainerByIdQuery request, CancellationToken ct)
        //{
        //    return await context.Trainers
        //        .FirstOrDefaultAsync(x => x.Id == request.Id, ct);
        //}
    }
    //public class GetTrainerByIdHandler(TrainerDbContext context, IMapper mapper)
    //    : IRequestHandler<GetTrainerByIdQuery, TrainerSummaryDto>
    //{
    //    public async Task<TrainerSummaryDto> Handle(GetTrainerByIdQuery request, CancellationToken ct)
    //    {
    //        // Use ProjectTo to fetch only the columns required by TrainerSummaryDto
    //        var trainer = await context.Trainers
    //            .AsNoTracking()
    //            .Where(t => t.Id == request.Id)
    //            .ProjectTo<TrainerSummaryDto>(mapper.ConfigurationProvider)
    //            .FirstOrDefaultAsync(ct);

    //        if (trainer == null)
    //        {
    //            throw new KeyNotFoundException($"Trainer with ID {request.Id} was not found.");
    //        }

    //        return trainer;
    //    }
    //}
}
