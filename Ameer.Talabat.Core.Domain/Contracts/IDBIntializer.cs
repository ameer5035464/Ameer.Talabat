using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Domain.Contracts
{
	public interface IDBIntializer
	{
		Task IntializeAsync();
		Task SeedAsync();
	}
}
