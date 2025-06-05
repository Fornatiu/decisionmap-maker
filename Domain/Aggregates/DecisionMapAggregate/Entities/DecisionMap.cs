using CSharpFunctionalExtensions;
using Domain.Aggregates.Value_Objects;

namespace Domain.Aggregates.DecisionMapAggregate.Entities
{
    public class DecisionMap : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<ProjectQr> SelectedQrs { get; set; } = new List<ProjectQr>();
        public List<DependencyEdge> DMatrix { get; set; } = new List<DependencyEdge>();
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        
        public DecisionMap(){ }

        public DecisionMap(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public Result<ProjectQr> AddQr(Guid qrMasterId, DefaultImpact impact, SusteinabilityDimension dim)
        {
            if (SelectedQrs.Any(q => q.QrMasterId == qrMasterId))
                return Result.Failure<ProjectQr>("QR already selected.");

            var qr = new ProjectQr(qrMasterId, impact, dim);
            SelectedQrs.Add(qr);

            // create empty edges from/to this new node
            foreach (var other in SelectedQrs.Where(q => q != qr))
            {
                DMatrix.Add(new DependencyEdge(qr.Id, other.Id, EdgeEffect.Undefined));
                DMatrix.Add(new DependencyEdge(other.Id, qr.Id, EdgeEffect.Undefined));
            }

            //AddDomainEvent(new QrAddedDomainEvent(Id, qrMasterId));
            return Result.Success(qr);
        }

        public Result RemoveQr(Guid qrMasterId)
        {
            var qr = SelectedQrs.SingleOrDefault(q => q.QrMasterId == qrMasterId);
            if (qr is null)
                return Result.Failure("QR not found.");

            SelectedQrs.Remove(qr);
            DMatrix.RemoveAll(e => e.FromQrId == qr.Id || e.ToQrId == qr.Id);

            //AddDomainEvent(new QrRemovedDomainEvent(Id, qrMasterId));
            return Result.Success();
        }

        public Result SetEdge(Guid fromQrId, Guid toQrId, EdgeEffect effect)
        {
            if (fromQrId == toQrId)
                return Result.Failure("Self-edges are not allowed.");

            var edge = DMatrix.SingleOrDefault(e => e.FromQrId == fromQrId && e.ToQrId == toQrId);

            if (edge is null)
                return Result.Failure("Edge not found.");

            edge.ChangeEffect(effect);
            return Result.Success();
        }

    }
}

