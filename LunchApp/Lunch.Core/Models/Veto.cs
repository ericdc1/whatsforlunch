namespace Lunch.Core.Models
{
    public class Veto : Database.Veto
    {
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}