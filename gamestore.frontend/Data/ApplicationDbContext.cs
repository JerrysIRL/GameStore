// -------------------------------------------------------------------
//   Copyright (c) Axis Communications AB, SWEDEN. All rights reserved.
//  -------------------------------------------------------------------
using GameStore.Frontend.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Frontend.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<GameStoreUser>(options)
{
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		List<GameStoreRole> initialRoles = new List<GameStoreRole>()
		{
			new GameStoreRole()
			{
				Name = "User",
				NormalizedName = "USER",
				RoleType = RoleType.Default,
			},
			new GameStoreRole()
			{
				Name = "Admin",
				NormalizedName = "ADMIN",
				RoleType = RoleType.Internal,
			},
		};

		builder.Entity<GameStoreRole>().HasData(initialRoles);
	}

}
