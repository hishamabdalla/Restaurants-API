﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.RestaurantDtos;
using System.Security.Claims;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var restaurants= await _mediator.Send(new GetAllRestaurantsQuery());
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(restaurants==null)
                return NotFound();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
        {
            var restaurant=await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand Command )
        {

            var id = await _mediator.Send(Command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id,[FromBody] UpdateRestaurantCommand Command)
        {
            Command.Id = id;
            var isUpdated = await _mediator.Send(Command);
            if (isUpdated)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

            if (isDeleted)
                return NoContent();

            return NotFound();
        }
    }
}
