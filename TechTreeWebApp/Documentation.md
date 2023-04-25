
![Tech Tree Home Page](wwwroot/Images/TechTreeHomePage.png)
# I. Overview
## Techtree is built under the Asp.Net Core MVC and covers the following concepts:
1. NET 6
2. MVC Framework
3. Entity Framework Core 
4. LINQ, jQuery, AJAX
5. SQL Server
6. ASP .Net Core Identity
7. JavaScript, HTML 5, Bootstrap 5, Razor

# II - IV
	Add Nuget: bootstrap
	appsettings.json
		M: "Server=DESKTOP-5I15SPG\\SQLEXPRESS;Database=TechTreeWebApp

## D: class ApplicationUser
	We extend IdentityUser by including ApplicationUser
		M: IdentityUser -> ApplicationUser
```cs
 public class ApplicationUser : IdentityUser {...}
```

[Application Db Context](Data/ApplicationDbContext.cs)

# V. Add-Migration
	-> Tools -> nugget package manager
		Add-Migration
		update-database
![Database Design](wwwroot/Images/Database%20Design.png)
# VI. Custom Tables
	D: Entities i.e class: Category, CategoryItem, Content, etc.
	D: Referencing Integrity
			one-to-many:[Foreign Key()]
			one-to-one: Reference

	M&D: ApplicationDbContext
		 DbSet<> 
# VII. 
	Admin Menu (drop-down bar in header)
	D: Razor View Empty "_AdminMenuPartial"
	-> Layout.cshtml
		D: partial name in ul class="navbar-nav
# VIII. 
	sol=> D:MVC Area "Admin"
	->ScaffoldingReadMe.txt
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
			name: "Admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
			});
			copy to Program.cs

	Controller => MVC Controller with views using Entity Framework
	Copy .cshtml files to Shared in Admin
	M: _AdminMenuPartial
# IX - X.
	Crud Operations (create read update delete)

[Category Item](Entities/CategoryItem.cs)

	The RAzor view provides an API for the Administrator to Add/Delete/etc the CategoryItem

	Mediatype is a foreign key -- add appropriate data type to mediatype database table before we add data to categoryitem db table

	D:IPrimaryInterface
	D: ConvertExtentions

	M: CategoryItem INdex() Create() Index.cshtml Create.cshtml
	M: Edit() Edit.cshtml
# XI.
	M: <a Back to List> in Create, Edit, Delete, Detail.cshtml in CategoryItems to return to Index properly
		D: asp-controller, route
	C: ContentController
	R: Detail, Delete, Index becuz we don't need it
	D: content in CategoryItem.Index //kinda confusing 
	-> CategoryItem.Index
		@if (item.ContentId != 0)
                    {
                        <a>Edit Content</a>
                        <!-- passing the categoryItemId and CategoryId -->
                    }
                    else
                    {
                        <a>Add Content </a>
                    }
	M: Create() and Edit() in ContentController to correspond to the request
# XII - XIV
	C: DeleteCall(int categoryId)
			for Deleting the corresponding db  // violates DRY but f*ck it
	D: jsquery for DAteTime picker
	Further UI refinements for the Category, 'Items, Content
	• Field Validation : Providing more human-readable label
		N: [Required(ErrorMessage = "You F*ck!"], [StringLength]
		[Display(Name = "")]
# XV.
	ASPUsers ---< UserRoles >--- NEtRoles
	We'll be defining the Roles
	We'll be incorporating ef core
	We'll also D: 2 GUIDs : for User PrimaryKey and for PrimaryKey value representing Admin Role

	N: All Controllers inside Admin D: attribute:
		[Area("Admin")]
		[Authorize(Roles = "Admin")]

	Refer: Migration "forContteeeeeeent"
# XVI.
	Test I. Implementing Html and Bootstrap code for the Model Form
	Test II. Displaying Dialog Box when Login is clicked
	Test III.  Login Related Data between user and UI's password and email 
	Test IV. Wiring up fields to login Model
	Test V. Authenticating User's credentials, submit to server, using SignInManager
	Test VI. For interacting server-side code :: using the power of AJAX to send data containing the user's credentials to the server

	Refer: UserLogin.js  Index.cshtml   _LoginPartial.cshtml   LoginModel.cs  _UserLoginPartial.cshtml  UserAuthController.cs

# XVII.
	M: Register.js and Login to display error messages on Modal dialog
	R: On-Blurry Event Handler (UserOnBlurry.js)

	Login and Register is fully ()nable at this point
	Admin has control of MediaTypes, Content, Categories, and CategoryList

	Refer: BootstrapAlert.js  UserOnBlurryEvent.js (used a .on("change") event handler) 
# XIX.
	The administrator can add and match content for multiple users in corresponding category
	The Usercategory joins custom tables with the identity table

	But how do we compare one userModel object to another whilst maintaining Single Resonsibility? ->Comparer.CompareUsers.cs


	Refer: _UsersListViewPartial.cshtml   Index.cshtml in Admin

	We Defined UserComparer : IEqualityComparer<UserModel> in order for us to incorporate .contains() in _UsersListViewPartial.cshtml

	Refer: UserComparer.cs
# XXII Took care of the:
	Loading Dialog  Ref: site.css   UserRegister.js

	D: Admin: UserModel and UserCategoryListModel
	C: UserCategory.Index 
		Ref: UserModel.cs   
				UserCategoryListModel.cs   
				UserToCategory.js		 
				UserCategoryController.cs
		> In order to Get and Display the UserModels which we retrieved from a Query
		> The Admin can choose User's designated courses 
		> These courses (UserCategory) are then saved, containing the User's UserId for retrieval

	C: Models for Registered User: User To Category to display the collapsable list of CategoryItems 
		Ref: CategoryDetailsModel.cs  
				CategoryItemDetailsModel.cs  
				GroupedcAtegoryItemsByCategory.cs  
				HomeController.cs  
				Home.Index.cshtml

	Users To Category ()nality in Admin Area 
		Ref: UserCategory.cs    
				UserCategory.Index.cshtml

# XXIII. Designing the Web Page of Home.Index

	R: site.css  _DefaultHomePageContentPartial.cshtml   
		
	D: the Header Control Buttons i.e "Courses Offered" , "About Us" etc 
		R: _Layout.cshtml

	N: All @using is located in the _ViewImports.cshtml

# XXIV. Code Reusability in EF Core && Adding Icons
	
	Download fontawesome web-version --> paste to wwwroot
		<link all.min.css  to both Layout.cshtml>

	in Admin "Create" Button
		MVC knows that the Create() that is called is the one related to the relevant controller
		R: _CreateButtonPartial.cshtml

	Code Reusability in CRUD in Index of Category, CategoryItem, Content
		R: _CRUDButtonsPartial.cshtml

# XXV. Controller for Registered Users to choose their courses

	We'll be implementing code to add our custom class with the .Net Core Dependency Injection 
	so that we can reuse code within the Registered type throughout multiple control classes

	Thus: DataFunctions.cs && IDataFunctions.cs

			public async Task UpdataUserCategoryEntityAsync(List<UserCategory> userCategoriesToAdd, List<UserCategory> userCategoriesToDelete)

	N: -->Program.cs   builder.Services.AddScoped<IDataFunctions, DataFunctions>();

	C:  Controller: CategoriesToUserController
	C:	View:		CategoriesToUser

		EF Core will automatically associate the two
		
	See the differences of UserCategoryController._UsersListViewPartial.cshtml
					and	   CategoriesToUser.Index.cshtml

# XXVI. 
	When an <a> tag's class is defined e.g "RegisterLink", both .js and css can reference that obj