using AIS.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AIS
    {

    public class EmailCredentails
        {
        private readonly IConfiguration _configuration;

        public EmailCredentails(IConfiguration? configuration = null)
            {
            _configuration = configuration ?? new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .Build();
            }

        public EmailCredentailsModel GetEmailCredentails()
            {
            EmailCredentailsModel em = new EmailCredentailsModel();
            em.EMAIL = _configuration["Email:address"] ?? string.Empty;
            em.PASSWORD = _configuration["Email:PASSWORD"] ?? string.Empty;
            em.Host = _configuration["Email:Host"] ?? string.Empty;
            em.Port = int.TryParse(_configuration["Email:Port"], out int port) ? port : 0;
            return em;

            }

        }
    }
