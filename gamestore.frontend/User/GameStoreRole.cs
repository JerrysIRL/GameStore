// -------------------------------------------------------------------
//   Copyright (c) Axis Communications AB, SWEDEN. All rights reserved.
//  -------------------------------------------------------------------
using Microsoft.AspNetCore.Identity;

namespace GameStore.Frontend.User;

public class GameStoreRole : IdentityRole
{
	public RoleType RoleType { get; set; }
}

public enum RoleType
{
	Default,
	External,
	Internal,
}
