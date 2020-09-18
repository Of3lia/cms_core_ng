﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FunctionalService;

namespace DataService
{
    public static class DbContextInitializer
    {
        public static async Task Initialize(DataProtectionKeysContext dataProtectionKeysContext, 
            ApplicationDbContext applicationDbContext, IFunctionalSvc functionalSvc)
        {
            // Check, if db DataProtectionKeysContext is created
            // Check, if db ApplicationDbContext is created
            await dataProtectionKeysContext.Database.EnsureCreatedAsync();
            await applicationDbContext.Database.EnsureCreatedAsync();

            //Check, if db contains any users. If db is not empty, then db has been already seeded
            if (applicationDbContext.ApplicationUsers.Any() )
            {
                return;
            }

            // If empty create Admin User and App User
            await functionalSvc.CreateDefaultAdminUser();
            await functionalSvc.CreateDefaultUser();

        }
    }
}