using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public record DeleteTrainerCommand(int Id) : IRequest<bool>;

    public class DeleteTrainerHandler(TrainerDbContext context, IFileStorageService files)
        : IRequestHandler<DeleteTrainerCommand, bool>
    {
        public async Task<bool> Handle(DeleteTrainerCommand request, CancellationToken ct)
        {
            var trainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == request.Id, ct);

            if (trainer == null) return false;

            // Clean up files from local storage before deleting record
            files.DeleteFile(trainer.ProfileImagePath);
            files.DeleteFile(trainer.ResumePath);

            context.Trainers.Remove(trainer);
            await context.SaveChangesAsync(ct);

            return true;
        }
    }
}
