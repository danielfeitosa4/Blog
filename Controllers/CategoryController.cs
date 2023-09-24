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
          // Get
          [HttpGet("v1/categories")] // localhost:PORT/v1/categories
          public async Task<IActionResult> GetAsync(BlogDataContext context)
          {
               try
               {
                    var categories = await context.Categories.ToListAsync();
                    return Ok(new ResultViewModel<List<Category>>(categories));
               }
               catch
               {
                    return StatusCode(500, new ResultViewModel<List<Category>>("05XE04 - Falha interna no servidor!"));
               }

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
               try
               {
                    var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                    if (category == null)
                         return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado!"));

                    return Ok(new ResultViewModel<Category>(category));
               }
               catch
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE05 - Falha interna no Servidor"));
               }
          }
          
          // Post
          [HttpPost("v1/categories")]
          public async Task<IActionResult> PostAsync(BlogDataContext context, EditorCategoryViewModel model)
          {
               if(!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

               try
               {
                    var category = new Category
                    {
                         Id = 0,
                         Name = model.Name,
                         Slug = model.Slug.ToLower()
                    };

                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();

                    return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
               }
               catch(DbUpdateException)
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE06 - Não foi possível incluir a categoria!"));
               }
               catch
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE07 - Falha interna no servidor!"));
               }
          }

          // Put
          [HttpPut("v1/categories/{id}")]
          public async Task<IActionResult> PutAsync(BlogDataContext context, EditorCategoryViewModel model, int id)
          {
               try
               {
                    var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                    if (category == null)
                         return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                    category.Name = model.Name;
                    category.Slug = model.Slug;

                    context.Categories.Update(category);
                    await context.SaveChangesAsync();

                    return Ok(new ResultViewModel<Category>(category));
               }
               catch (DbUpdateException ex)
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE08 - Falha interna no servidor!"));
               }
               catch
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE09 - Falha interna no servidor!"));
               }
          }

          // Delete
          [HttpDelete("v1/categories/{id}")]
          public async Task<IActionResult> DeleteAsync(BlogDataContext context, int id)
          {
               try
               {
                    var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                    if (category == null)
                         return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));

                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();

                    return Ok(new ResultViewModel<Category>(category));
               }
               catch (DbUpdateException ex)
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE10 - Falha interna no servidor!"));
               }
               catch
               {
                    return StatusCode(500, new ResultViewModel<Category>("05XE11 - Falha interna no servidor!"));
               }
          }
     }
}