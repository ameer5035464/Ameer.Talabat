﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.ExceptionsHandlers
{
	public class UnsupportedMediaTypException : Exception
	{
        public UnsupportedMediaTypException(string? message):base(message)
        {
            
        }
    }
}