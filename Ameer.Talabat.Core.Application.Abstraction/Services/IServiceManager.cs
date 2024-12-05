using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
	public interface IServiceManager
	{
        public IProductServices ProductServices { get;}
        public IBasketService BasketServices { get; }
        public IAuthService AuthServices { get; }
    }
}
