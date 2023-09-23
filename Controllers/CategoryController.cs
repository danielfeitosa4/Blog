using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
       // Get
       [HttpGet("v1/categories")] // localhost:PORT/v1/categories
       public async Task<IActionResult> GetAsync(BlogDataContext context)
       {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
       }

        // [HttpGet("v2/categories")] // localhost:PORT/v2/categories
        // public IActionResult Get2(BlogDataContext context)
        // {
        //     var categories = context.Categories.ToList();
        //     return Ok(categories);
        // }

       // GetById
       [HttpGet("v1/categories/{id}")]
       public async Task<IActionResult> GetByIdAsync(BlogDataContext context, int id)
       {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound();

            return Ok(category);
       }

       // Post
       [HttpPost("v1/categories")]
       public async Task<IActionResult> PostAsync(BlogDataContext context, Category model)
       {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/categories/{model.Id}", model);
       }

       // Put
       [HttpPut("v1/categories/{id}")]
       public async Task<IActionResult> PutAsync(BlogDataContext context, Category model, int id)
       {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(model);
       }

       // Delete
       [HttpDelete("v1/categories/{id}")]
       public async Task<IActionResult> DeleteAsync(BlogDataContext context, int id)
       {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category == null)
                return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
       }
    }
}