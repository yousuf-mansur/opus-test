using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Data;
using ProductsManagement.Models;

namespace ProductsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _db;

        public ProductController(ProductDbContext db)
        {
            _db = db;
        }

      
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        
        [HttpGet("productId")]        
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var productList = await _db.Products.FindAsync(productId);

            if (productList == null)
            {
                return NotFound("No Product Id is Found!!");
            }

            return Ok(productList);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product is null)
            {
                return BadRequest("Product Data is Invalid!!!!");

            }
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(new { success = true, message = "Product is added Successfully", product});
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> PutProduct(int productId, Product product)
        {
            if (productId != product.Id)
            {
                return BadRequest();
            }

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(productId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { success = true, message = "Product is Updated Successfully", product });
        }

        
        

        
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Id is not Found!!!");
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok(new { success=true, Message= "Product is Deleted" });
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Any(e => e.Id == id);
        }
    }
}
