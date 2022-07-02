using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController()
        {
            
        }

        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices]BlogDataContext context
        ) 
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("58X1 - Houve uma falha interna no servidor."));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices]BlogDataContext context,
            [FromRoute]int id
        ) 
        {
            try
            {
                var category = await context.
                Categories
                .FirstOrDefaultAsync(p => p.Id == id);
            
                if(category is null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("73X1 - Houve uma falha interna no servidor."));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromServices]BlogDataContext context,
            [FromBody]CreateCategoryViewModel category
        ) 
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                Category model = new(category.Name, category.Slug);
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", new ResultViewModel<Category>(model));
            }
            catch (System.Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("Um erro interno aconteceu"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PuTAsync(
            [FromRoute]int id,
            [FromServices]BlogDataContext context,
            [FromBody]UpdateCategoryViewModel category
        ) 
        {
            try
            {
                var model = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

                if(model is null )
                    return NotFound();

                model.Name = category.Name;
                model.Slug = category.Slug;

                context.Categories.Update(model);
                await context.SaveChangesAsync();
                return Ok($"v1/categories/{model.Id}");
            }
            catch (System.Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("Um erro interno aconteceu."));
            }
            
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute]int id,
            [FromServices]BlogDataContext context
        ) 
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(p => p.Id == id);

                if(category is null )
                    return NotFound();
                
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok($"v1/categories/{category.Id}");
            }
            catch (System.Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("Um erro interno aconteceu."));
            }
        }
    }
}