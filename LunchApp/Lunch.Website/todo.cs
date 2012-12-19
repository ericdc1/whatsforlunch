//todo: admit that we failed at nhibernate - EC/JD/MM/LD
//todo: bring in the UI - SRK
//todo: ui for editing lists and search/filter lists - TY/SRK
//todo: ui for email messages - TY
//todo: CRUD for restaurants - MM
//add stats columns - last visited, last up for vote, times in last 90 days, average rating based on weighted averages of attendees
//todo: user voting for restaurants

//todo: possibly pull from some external data source - MM -- http://api.citygridmedia.com/content/places/v2/search/latlon?lat=39.653319&lon=-79.95735&radius=1&type=restaurant&publisher=10000004484&rpp=50
//todo: create remaining jobs 

//todo: views for jobs and job logs (filter and date range) - MM

//todo: CRUD for users - JD 
//todo: User logins - allow login with username/password and with guid - JD
//todo: Allow users to edit profile including mail preferences - JD
//todo: make holidays tables and logic

//todo: Generate 4 restaurants method MM/JD
//should be random weighting based on users restaurant ratings
//ratings should be diluted by the number of times the person went to lunch 
//Slot 1/2 should have restaurants from the top 10
//Slot 3 and possibly 4 should contain random restaurants not in the top 10 that haven't been recently visited
//Slot 4 should contain a restaurant that is set for the preferred day of week
//write to selections table one row per restaurant- 4 per day - this data can be used to help pick restaurants because you can get a history of choices and winning locations
//ID, JobID, RestaurantID, EventDate, WasSelectedFLG (maybe time out / time in)


//todo: CR for voting -
//ID, RestaurantID, UserID, EventDate, (maybe IsOverrideFLG?)

//todo: try knockout or backbone for voting and restaurants to vote for

//todo: Email templates ED/LD 
//http://kazimanzurrashid.com/posts/use-razor-for-email-template-outside-asp-dot-net-mvc
//https://github.com/smsohan/MvcMailer/wiki/MvcMailer-Step-by-Step-Guide

//todo: webapi methods for voting, restaurant list EC/LD
// GetTodaysRestaurant, //Vote

//todo: make a setup wizard and build database from scripts via web
//http://umbraco.codeplex.com/SourceControl/changeset/view/570e4433bee6#src/umbraco.datalayer/Utility/Installer/DefaultInstallerUtility.cs
//http://msdn.microsoft.com/en-us/library/49b92ztk(v=vs.100).aspx
 

//todo: setup stackexchange exceptional

//todo: make stats pages
//top restaurants, etc

//todo: phonegap
//todo: windows store app



