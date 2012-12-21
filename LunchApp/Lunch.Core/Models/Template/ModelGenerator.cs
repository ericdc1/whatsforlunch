using System;
namespace Lunch.Core.Models.Template
{ 
        public class Job
		{
        	public int Id { get; set; }
        	public string MethodName { get; set; }
        	public string ParametersJson { get; set; }
        	public DateTime RunDate { get; set; }
        	public DateTime CreatedDate { get; set; }
        	public bool HasRun { get; set; }
        }
      
        public class JobLog
		{
        	public int Id { get; set; }
        	public string Category { get; set; }
        	public string Message { get; set; }
        	public DateTime LogDTM { get; set; }
        	public int JobID { get; set; }
        }
      
        public class Restaurant
		{
        	public int Id { get; set; }
        	public string RestaurantName { get; set; }
        	public string PreferredDayOfWeek { get; set; }
        	public int RestaurantTypeID { get; set; }
        }
      
        public class RestaurantType
		{
        	public int Id { get; set; }
        	public string TypeName { get; set; }
        }
      
        public class User
		{
        	public int Id { get; set; }
        	public string FullName { get; set; }
        	public string Email { get; set; }
        	public string Password { get; set; }
        	public bool SendMorningEmailFlg  { get; set; }
        	public bool SendVotingIsOverEmailFlg  { get; set; }
        	public bool SendWhereWeAreGoingEmailFlg { get; set; }
        	public bool IsAdministrator  { get; set; }
        	public string GUID { get; set; }
        }
      
}

