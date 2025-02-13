using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.Queries.GetAll;
using Restaurants.Application.Dishes.Queries.GetDishById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurant/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DishesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId,CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
          var DishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishByIdForRestaurant),new {restaurantId, DishId },null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDishesForRestaurant([FromRoute]int restaurantId )
        {
            var dishes=await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            if (dishes.Count()>0)
                 return Ok(dishes);
            return NotFound();
        }
        [HttpGet("{DishId}")]
        public async Task<IActionResult> GetDishByIdForRestaurant([FromRoute] int restaurantId,int DishId)
        {
            var dishes = await mediator.Send(new GetDishForRestaurantQuery(restaurantId,DishId));
            if (dishes == null)
                return NotFound();
            return Ok(dishes);
        }

        [HttpDelete("{DishId}")]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId, int DishId)
        {
            var dishes = await mediator.Send(new DeleteDishCommand(restaurantId, DishId));
            if (dishes)
                return NoContent();
            return BadRequest();
               
        }

        [HttpPut("{DishId}")]
        public async Task<IActionResult> UpdateDishForRestaurant([FromRoute] int restaurantId, int DishId, [FromBody]UpdateDishCommand command)
        {
            command.RestaurantId= restaurantId;
            command.DishId= DishId;
            var dishes = await mediator.Send(command);
            if (dishes)
                return NoContent();
            return BadRequest();

        }
    }
}
