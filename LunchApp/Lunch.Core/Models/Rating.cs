namespace Lunch.Core.Models
{
    public class Rating
    {
        public virtual int Id { get; protected set; }
        public virtual decimal Score { get; set; }
        public virtual int UserID { get; set; }
        public virtual int RestaurantID { get; set; }
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}