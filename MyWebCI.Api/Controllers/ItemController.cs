using Microsoft.AspNetCore.Mvc;
using MyWebCI.Api.Dto.Response;

namespace MyWebCI.Api.Controllers
{
    [Route("[controller]")]
    public class ItemController(IConfiguration config) : BaseApiController
    {
        [HttpGet]
        public ActionResult<ItemResponseDto> Get()
        {
            return Ok(new ItemResponseDto() { Id = int.Parse(config["ProductId"]), Name = "Item01" });
        }

        [HttpGet("{id}")]
        public ActionResult<ItemResponseDto> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            return Ok(new ItemResponseDto() { Id = id, Name = "Item" + id });
        }


        [HttpGet("category")]
        public ActionResult<ItemResponseDto> GetCategory()
        {
            return Ok(new ItemResponseDto() { Id = 1, Name = "Categorym01" });
        }

        [HttpGet("category/{id}")]
        public ActionResult<ItemResponseDto> GetCategoryById(int id)
        {
            return Ok(new ItemResponseDto() { Id = id, Name = "Categorym" + id });
        }
    }
}
