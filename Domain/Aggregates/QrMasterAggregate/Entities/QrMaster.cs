using Domain.Aggregates.Value_Objects;

namespace Domain.Aggregates.QrMasterAggregate.Entities
{
    public class QrMaster : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public SusteinabilityDimension Dimension { get; set; }
        public QrMaster() { }

        public QrMaster(string name, SusteinabilityDimension dimension)
        {
           
            Name = name;
            Dimension = dimension;
        }
    }
}
