﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException( string message)
            : base( message)
        {
        }




    }
}
