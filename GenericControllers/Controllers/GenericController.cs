// TCDev.de 2022/03/31
// GenericControllers.GenericController.cs
// https://www.github.com/deejaytc/dotnet-utils

using GenericControllers.Data;
using Microsoft.AspNetCore.Mvc;

namespace GenericControllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class GenericController<T, TEntityId> : Controller
   where T : class,
   IObjectBase
{
   private readonly GenericDbContext db;

   public GenericController(GenericDbContext context)
   {
      this.db = context;
   }

   [HttpGet]
   public IQueryable<T> Get()
   {
      return this.db.Set<T>();
   }

   [HttpGet("{id}")]
   public T GetById(TEntityId id)
   {
      return Get()
         .SingleOrDefault(e => e.Id.ToString() == id.ToString());
   }


   [HttpPost]
   public async Task<IActionResult> Create([FromBody] T record)
   {
      try
      {
         // Check if payload is valid
         if (!this.ModelState.IsValid)
            return BadRequest();

         // Create the new entry
         this.db.Add(record);
         await this.db.SaveChangesAsync();

         // respond with the newly created record
         return CreatedAtAction("GetById", new
         {
            id = record.Id
         }, record);
      }
      catch (Exception ex)
      {
         return BadRequest(ex);
      }
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> Update(TEntityId id, [FromBody] T record)
   {
      try
      {
         if (!this.ModelState.IsValid)
            return BadRequest();

         this.db.Set<T>()
            .Attach(record);
         await this.db.SaveChangesAsync();

         return Ok(record);
      }
      catch (Exception ex)
      {
         return BadRequest(ex.Message);
      }
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(TEntityId id)
   {
      try
      {
         if (!this.ModelState.IsValid)
            return BadRequest();
         var record = GetById(id);
         this.db.Remove(record);
         await this.db.SaveChangesAsync();
         return Ok("true!");
      }
      catch (Exception ex)
      {
         return BadRequest(ex.Message);
      }
   }
}
