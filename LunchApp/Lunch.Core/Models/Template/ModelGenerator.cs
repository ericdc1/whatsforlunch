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
        	public int PreferredDayOfWeek { get; set; }
        	public int RestaurantTypeID { get; set; }
        }
      
        public class RestaurantType
		{
        	public int Id { get; set; }
        	public string TypeName { get; set; }
        }
      
        public class Users
		{
        	public int Id { get; set; }
        	public string FullName { get; set; }
        	public string Email { get; set; }
        	public bool SendWhereWeAreGoingEmailFlg { get; set; }
        	public string GUID { get; set; }
        	public bool myflg { get; set; }
        }
      
        public class webpages_Membership
		{
        	public int UserId { get; set; }
        	public DateTime CreateDate { get; set; }
        	public string ConfirmationToken { get; set; }
        	public bool IsConfirmed { get; set; }
        	public DateTime LastPasswordFailureDate { get; set; }
        	public int PasswordFailuresSinceLastSuccess { get; set; }
        	public string Password { get; set; }
        	public DateTime PasswordChangedDate { get; set; }
        	public string PasswordSalt { get; set; }
        	public string PasswordVerificationToken { get; set; }
        	public DateTime PasswordVerificationTokenExpirationDate { get; set; }
        }
      
        public class webpages_Roles
		{
        	public int RoleId { get; set; }
        	public string RoleName { get; set; }
        }
      
        public class webpages_UsersInRoles
		{
        	public int UserId { get; set; }
        	public int RoleId { get; set; }
        }
      
}

