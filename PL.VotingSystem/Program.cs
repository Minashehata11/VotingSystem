using BLL.VotingSystem.Interfaces;
using BLL.VotingSystem.Repository;
using BLL.VotingSystem.Services;
using DAL.VotingSystem.Context;
using DAL.VotingSystem.Entities.UserIdentity;
using LearnApi.HelperServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PL.VotingSystem.Extentions;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PL.VotingSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
           /*
           
           * create a xml file in the same directory
           
           XmlSerializer xmlSerializer = new XmlSerializer(typeof(VoterTest1));
           FileStream stream = File.OpenWrite("myfilterL.xml");
           
           *abbend data in it based on VoterTest1 model 
           xmlSerializer.Serialize(stream, new VoterTest1() { Name = "ali", NationalNumber = "72727383839" });
           
           stream.Dispose();
           */
           
           /* testing the function , should return true 
           var ans = StringInXml("myfilter.xml","20205020");
           Console.WriteLine(ans);
           */

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<string>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            })
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddSignInManager<SignInManager<ApplicationUser>>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidateAudience = false,
                        
                    };
                });

            //  builder.Services.addIdentityServices(builder.Configuration);
            builder.Services.AddServiceExtintions();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddScoped<ICandidateRepository, CandidateRepository>(); // wiring interface with repo


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        // searching function return boolean , true if national number exists in xml file 
        public static bool StringInXml(string xmlFilePath, string searchString)
        {
            
            if (string.IsNullOrEmpty(xmlFilePath) || string.IsNullOrEmpty(searchString))
            {
                
                return false; // Handle empty inputs 
            }

            try
            {
                // Use XDocument for clean and efficient XML handling
                var doc = XDocument.Load(xmlFilePath);
                return doc.ToString().Contains(searchString); // return true if find or false if not or through exceptions
            }
            catch (Exception ex)
            {
                // Handle potential exceptions (e.g., file not found, invalid XML)
                Console.WriteLine($"Error: {ex.Message}");
               
                return false;
            }
            
        }
    }

}