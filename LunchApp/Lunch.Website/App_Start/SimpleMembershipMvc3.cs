using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Security;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;
using WebMatrix.WebData;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Lunch.Website.App_Start.SimpleMembershipMvc3), "Start")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(Lunch.Website.App_Start.SimpleMembershipMvc3), "Initialize")]

namespace Lunch.Website.App_Start
{
	public static class SimpleMembershipMvc3
	{
		public static readonly string EnableSimpleMembershipKey = "enableSimpleMembership";

		public static bool SimpleMembershipEnabled
		{
			get { return IsSimpleMembershipEnabled(); }
		}
		
		public static void Initialize()
		{
		    if (!MvcApplication.SetupComplete)
		        return;

			// Modify the settings below as appropriate for your application
            //WebSecurity.InitializeDatabaseConnection(connectionStringName: "AzureSQL", userTableName: "User", userIdColumn: "Id", userNameColumn: "Email", autoCreateTables: true);
			
			// Comment the line above and uncomment these lines to use the IWebSecurityService abstraction
		    var webSecurityService = StructureMap.ObjectFactory.GetInstance<IWebSecurityService>();
            webSecurityService.InitializeDatabaseConnection(connectionStringName: "AzureSQL", userTableName: "Users", userIdColumn: "Id", userNameColumn: "Email", autoCreateTables: true);

            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");
            
            if (!WebSecurity.UserExists("admin@lunch.com"))
            {
                WebSecurity.CreateUserAndAccount("admin@lunch.com", "admin", new { FullName = "Lunch Admin", SendMail1 = false, SendMail2 = false, SendMail3 = false, SendMail4 = false, GUID = Guid.NewGuid() });
                Roles.AddUserToRole("admin@lunch.com", LunchRoles.Administrator.ToString());
                Roles.AddUserToRole("admin@lunch.com", "User");
            }
              
		}

		public static void Start()
		{
			if (SimpleMembershipEnabled && MvcApplication.SetupComplete)
			{
				MembershipProvider provider = Membership.Providers["AspNetSqlMembershipProvider"];
				if (provider != null)
				{
					MembershipProvider currentDefault = provider;
					SimpleMembershipProvider provider2 = CreateDefaultSimpleMembershipProvider("AspNetSqlMembershipProvider", currentDefault);
					Membership.Providers.Remove("AspNetSqlMembershipProvider");
					Membership.Providers.Add(provider2);
				}
				Roles.Enabled = true;
				RoleProvider provider3 = Roles.Providers["AspNetSqlRoleProvider"];
				if (provider3 != null)
				{
					RoleProvider provider6 = provider3;
					SimpleRoleProvider provider4 = CreateDefaultSimpleRoleProvider("AspNetSqlRoleProvider", provider6);
					Roles.Providers.Remove("AspNetSqlRoleProvider");
					Roles.Providers.Add(provider4);
				}
			}
		}

		#region : Private Methods :

		private static bool IsSimpleMembershipEnabled()
		{
			bool flag;
			string str = ConfigurationManager.AppSettings[EnableSimpleMembershipKey];
			if (!string.IsNullOrEmpty(str) && bool.TryParse(str, out flag))
			{
				return flag;
			}
			return true;
		}

		private static SimpleMembershipProvider CreateDefaultSimpleMembershipProvider(string name, MembershipProvider currentDefault)
		{
			MembershipProvider previousProvider = currentDefault;
			SimpleMembershipProvider provider = new SimpleMembershipProvider(previousProvider);
			NameValueCollection config = new NameValueCollection();
			provider.Initialize(name, config);
			return provider;
		}

		private static SimpleRoleProvider CreateDefaultSimpleRoleProvider(string name, RoleProvider currentDefault)
		{
			RoleProvider previousProvider = currentDefault;
			SimpleRoleProvider provider = new SimpleRoleProvider(previousProvider);
			NameValueCollection config = new NameValueCollection();
			provider.Initialize(name, config);
			return provider;
		}

		#endregion

	}
}
