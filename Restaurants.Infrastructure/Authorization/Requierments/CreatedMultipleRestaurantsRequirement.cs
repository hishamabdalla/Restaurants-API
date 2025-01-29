﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requierments
{
    public class CreatedMultipleRestaurantsRequirement:IAuthorizationRequirement
    {
        public CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated=minimumRestaurantsCreated;
        }
        public int MinimumRestaurantsCreated { get; set; }
    }
}
