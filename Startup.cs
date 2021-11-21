using InterviewTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterviewTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            PrepareDB();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllersWithViews().AddNewtonsoftJson();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<InterviewTestDbContext>();

            services.AddScoped<IListService, ListService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void PrepareDB()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder() { DataSource = "./SqliteDB.db" };
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Employees";
                delTableCmd.ExecuteNonQuery();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Employees(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name VARCHAR(50), Value INT)";
                createTableCmd.ExecuteNonQuery();

                //Fill with data
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = @"INSERT INTO Employees VALUES
                        (1,'Abul', 1357),
                        (2,'Adolfo', 1224),
                        (3,'Alexander', 2296),
                        (4,'Amber', 1145),
                        (5,'Amy', 4359),
                        (6,'Andy', 1966),
                        (7,'Anna', 4040),
                        (8,'Antony', 449),
                        (9,'Ashley', 8151),
                        (10,'Borja', 9428),
                        (11,'Cecilia', 2136),
                        (12,'Christopher', 9035),
                        (13,'Dan', 1475),
                        (14,'Dario', 284),
                        (15,'David', 948),
                        (16,'Elike', 1860),
                        (17,'Ella', 4549),
                        (18,'Ellie', 5736),
                        (19,'Elliot', 1020),
                        (20,'Emily', 7658),
                        (21,'Faye', 7399),
                        (22,'Fern', 1422),
                        (23,'Francisco', 5028),
                        (24,'Frank', 3281),
                        (25,'Gary', 9190),
                        (26,'Germaine', 6437),
                        (27,'Greg', 5929),
                        (28,'Harvey', 8471),
                        (29,'Helen', 963),
                        (30,'Huzairi', 9491),
                        (31,'Izmi', 8324),
                        (32,'James', 6994),
                        (33,'Jarek', 6581),
                        (34,'Jim', 202),
                        (35,'John', 261),
                        (36,'Jose', 1605),
                        (37,'Josef', 3714),
                        (38,'Karthik', 4828),
                        (39,'Katrin', 5393),
                        (40,'Lee', 269),
                        (41,'Luke', 5926),
                        (42,'Madiha', 2329),
                        (43,'Marc', 3651),
                        (44,'Marina', 6903),
                        (45,'Mark', 3368),
                        (46,'Marzena', 7515),
                        (47,'Mohamed', 1080),
                        (48,'Nichole', 1221),
                        (49,'Nikita', 8520),
                        (50,'Oliver', 2868),
                        (51,'Patryk', 1418),
                        (52,'Paul', 4332),
                        (53,'Ralph', 1581),
                        (54,'Raymond', 7393),
                        (55,'Roman', 4056),
                        (56,'Ryan', 252),
                        (57,'Sara', 2618),
                        (58,'Sean', 691),
                        (59,'Seb', 5395),
                        (60,'Sergey', 8282),
                        (61,'Shaheen', 3721),
                        (62,'Sharni', 7737),
                        (63,'Sinu', 3349),
                        (64,'Stephen', 8105),
                        (65,'Tim', 8386),
                        (66,'Tina', 5133),
                        (67,'Tom', 7553),
                        (68,'Tony', 4432),
                        (69,'Tracy', 1771),
                        (70,'Tristan', 2030),
                        (71,'Victor', 1046),
                        (72,'Yury', 1854)";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }
    }
}
