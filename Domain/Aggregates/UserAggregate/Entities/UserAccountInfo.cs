using Domain.Aggregates.UserAggregate.Value_Objects;

namespace Domain.Aggregates.UserAggregate.Entities
{
    public class UserAccountInfo
    {
        public Guid Id { get; set; }
        public DateOnly DateCreated { get; }
        public string Alias { get; set; }
        public string Avatar { get; set; }

        public UserAccountInfo(string alias)
        {    
            this.Alias = alias;
            this.DateCreated = DateOnly.FromDateTime(DateTime.Now);
            this.Avatar = "default_avatar.jpg";

        }
        public UserAccountInfo()
        {
            this.Alias = "None";
            this.DateCreated = DateOnly.FromDateTime(DateTime.Now);
            this.Avatar = "default_avatar.jpg";
        }
    }
}
