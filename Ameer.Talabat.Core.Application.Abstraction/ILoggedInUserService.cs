﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction
{
	public interface ILoggedInUserService
	{
		string? UserId { get; }
	}
}
