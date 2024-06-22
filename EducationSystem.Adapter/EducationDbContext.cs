using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Files;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.AssessmentModels;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Domain.Other;
using EducationSystem.Domain.Relationships;
using EducationSystem.Domain.Role;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter
{
    public class EducationDbContext : DbContext
		{
			public EducationDbContext(DbContextOptions options) : base(options)
			{
				Database.EnsureCreated();
			}

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
			modelBuilder.Entity<ItemInCurriculum>().HasKey(u => new { u.ItemId, u.CurriculumId});// комбинированный ключ
			modelBuilder.Entity<StudentInClass>().HasKey(u => new { u.StudentId, u.ClassId });// комбинированный ключ
            modelBuilder.Entity<ParentsOfStudents>().HasKey(u => new { u.StudentId, u.ParentId });// комбинированный ключ
            modelBuilder.Entity<User>().HasIndex(l => l.Login).IsUnique(); // уникальный ключ
			modelBuilder.Entity<SchoolClass>().HasIndex(k => new {k.Number,k.Letter,k.YearFormation,k.IsHidden}).IsUnique(); // уникальный ключ
			modelBuilder.Entity<Classroom>().HasKey(n => n.Number); // уникальный ключ
            modelBuilder.Entity<LessonTime>().HasKey(n => n.Number); // уникальный ключ
            modelBuilder.Entity<StudyDay>().HasKey(d => d.Id); // уникальный ключ
            //modelBuilder.Entity<LessonInSсhedule>().HasKey(u => new { u.Time,u.Day}); // уникальный ключ

            //modelBuilder.Entity<Curriculum>().ToTable(t => t.HasCheckConstraint("YearFormation", "YearFormation>0"));
        }


        //modelBuilder.Entity<Curriculum>().ToTable(t => t.HasCheckConstraint("Age", "Age > 0 AND Age < 120"));
        //public DbSet<User> UserData { get; set; }

        //AuthModels
        public DbSet<User> Users { get; set; }

        //Models
        public DbSet<Person> Persons { get; set; }
		public DbSet<Curriculum> Curriculums { get; set; }
		public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<StudyDay> StudyDays { get; set; }
        public DbSet<ClassSchedule> Shedules { get; set; }
        public DbSet<LessonInSсhedule> LessonsInShedule { get; set; }
        public DbSet<FinalAssessment> FinalAssessments{ get; set; }
        public DbSet<LessonAssessment> LessonAssessments { get; set; }
        public DbSet<FilePath> FilesPaths { get; set; }
        public DbSet<NewsData> News { get; set; }

        //roles
        public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<PersonRole> PersonRoles { get; set; }

		//relationships
		public DbSet<ItemInCurriculum> RelationItemsInCurriculums { get; set; }
		public DbSet<StudentInClass> RelationStudentsInClasses { get; set; }
        public DbSet<ParentsOfStudents> RelationParentsOfStudents { get; set; }


    }
}
