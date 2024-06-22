using EducationSystem.Adapter;
using EducationSystem.Adapter.Repositories.Auth;
using EducationSystem.Adapter.Repositories.Generic;
using EducationSystem.Adapter.Repositories.Models;
using EducationSystem.Adapter.Repositories.Models.AssessmentRepository;
using EducationSystem.Adapter.Repositories.Models.ClassRepository;
using EducationSystem.Adapter.Repositories.Models.RelationshipsRepositories;
using EducationSystem.Adapter.Repositories.Models.SheduleRepositories;
using EducationSystem.App.Interactor.AssessmentInteractor;
using EducationSystem.App.Interactor.AuthInteractors;
using EducationSystem.App.Interactor.FileInteractors;
using EducationSystem.App.Interactor.ModelsInteractors;
using EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors;
using EducationSystem.App.Interactor.ModelsInteractors.S�heduleInteractors;
using EducationSystem.App.Interactor.OtherInteractor;
using EducationSystem.App.Interactor.RelationshipsInteractors;
using EducationSystem.App.Interactor.RoleInteractors;
using EducationSystem.App.Storage.AuthInterfaces;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.ClassInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces;
using EducationSystem.Provider.Authentication;
using EducationSystem.Provider.AuthenticationToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//swagger http://localhost:9730/swagger/index.html

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var configuration = builder.Configuration;
		var connectionMessenger = configuration.GetConnectionString("MyConnection");

		// �����������
		builder.Services.AddTransient<IUnitWork, UnitWork>();
		builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		builder.Services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));
		builder.Services.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));
        builder.Services.AddScoped(typeof(ISchoolClassRepository), typeof(SchoolClassRepository));
        builder.Services.AddScoped(typeof(IStudentInClassRepository), typeof(StudentInClassRepository));
        builder.Services.AddScoped(typeof(IItemInCurriculumRepository), typeof(ItemInCurriculumRepository));
        builder.Services.AddScoped(typeof(IClassSheduleRepository), typeof(ClassSheduleRepository));
        builder.Services.AddScoped(typeof(ILessonInSheduleRepository), typeof(LessonInSheduleRepository));
        builder.Services.AddScoped(typeof(IFinalAssessmentRepository), typeof(FinalAssessmentRepository));
        builder.Services.AddScoped(typeof(ILessonAssessmentRepository), typeof(LessonAssessmentRepository));
        builder.Services.AddScoped(typeof(IParentOfStudentRepository), typeof(ParentOfStudentRepository));

        // ����������� �������-������
        builder.Services.AddScoped<PersonInteractor>();
		builder.Services.AddScoped<AuthInteractor>();
        builder.Services.AddScoped<CurriculumInteractor>();
        builder.Services.AddScoped<SchoolClassInteractor>();
        builder.Services.AddScoped<StudentInClassInteractor>();
        builder.Services.AddScoped<ItemInteractor>();
        builder.Services.AddScoped<ItemInCurriculumInteractor>();
        builder.Services.AddScoped<ClassroomInteractor>();
        builder.Services.AddScoped<LessonTimeInteractor>();
        builder.Services.AddScoped<StudyDayInteractor>();
        builder.Services.AddScoped<ClassScheduleInteractor>();
        builder.Services.AddScoped<LessonInS�heduleInteractor>();
        builder.Services.AddScoped<FinalAssessmentInteractor>();
        builder.Services.AddScoped<ParentOfStudentInteractor>();
        builder.Services.AddScoped<FileInteractor>();
        builder.Services.AddScoped<NewsInteractor>();

        // ����
        builder.Services.AddScoped<UserRoleInteractor>();
        builder.Services.AddScoped<PersonRoleInteractor>();

			// ���������

        builder.Services.AddSingleton(provider => GetAuthenticationOptions(configuration));

		builder.Services.AddDbContext<EducationDbContext>
			(options => options.UseSqlite(connectionMessenger, b=>b.MigrationsAssembly("EducationSystem.Api")));

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddRazorPages();

		var authOptions = GetAuthenticationOptions(configuration);

		builder.Services.AddSingleton<IAuthenticationService,AuthService>();

		builder.Services.AddAuthorization();
		builder.Services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = authOptions.Issuer,

					ValidateAudience = true,
					ValidAudience = authOptions.Audience,

					ValidateLifetime = true,

					ValidateIssuerSigningKey = true,

					IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
				};
			});


		builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
			policybuilder =>
			{
				policybuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
			}
			));


		/* Setup authentication end */

		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "EducationSystem", Version = "v1" });

			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: Bearer 1safsfsdfdfd",
			});

					options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
				}
			});

		});
		

		var app = builder.Build();

		//builder.Services.AddScoped<AccountInteractor>();
		//builder.Services.AddScoped<IAccountRepository, AccountRepository>();

		//builder.Services.AddScoped(typeof(SchoolInteractor));
		//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
			app.UseSwaggerUI();
		}

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseCors("CorsPolicy");

		app.MapRazorPages();
		app.MapControllers();
		app.MapFallbackToFile("index.html");


		app.Run();


	}
	public static AuthOptions GetAuthenticationOptions(IConfiguration config) =>
			config.GetSection("AuthenticationOptions").Get<AuthOptions>()!;
}

