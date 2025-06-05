using CSharpFunctionalExtensions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.QrMaster
{
    public class DeleteQrCommand : IRequest<Result>
    {
        [Required] public required Guid Id { get; set; }
    }
}
