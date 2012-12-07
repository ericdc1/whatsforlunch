namespace Lunch.Core.Models
{
    public class User
    {
        public virtual int Id { get; protected set; }
        public virtual string FullName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool SendMorningEmailFlg { get; set; }
        public virtual bool SendVotingIsOverEmailFlg { get; set; }
        public virtual bool SendWhereWeAreGoingEmailFlg { get; set; }
        public virtual bool IsAdministrator { get; set; }
    }
}