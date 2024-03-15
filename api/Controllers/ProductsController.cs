using Application.Context;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController, Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet, Route("/api/"), Route("/api/products"), Authorize]
        public IActionResult GetAll()
        {
            //if (HttpContext.User.Identity.IsAuthenticated == false) return Ok("Not Authenticated");

            var ele = context.prodtuct.ToList();
            return ele is null ? Ok("NotFound") : Ok(ele);
        }

        [HttpGet, Route("{id:long}")]
        public IActionResult Get(int id)
        {

            var ele = context.prodtuct.FirstOrDefault(x => id == x.id);
            return ele is null ? Ok("NotFound") : Ok(ele);
        }

        [HttpGet, Route("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            var ele = context.prodtuct.FirstOrDefault(x => x.name.Contains(name));
            return ele is null ? Ok("NotFound") : Ok(ele);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.AddAsync(product);
                context.SaveChanges();
                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/");
                var uri = location.AbsoluteUri;
                return Created(uri + product.id, "Created");
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                context.prodtuct.Update(product);
                context.SaveChanges();
                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/");
                var uri = location.AbsoluteUri;
                return Ok($"{uri + product.id} Updated");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("invalid id");
            var prd = context.prodtuct.FirstOrDefault(p => p.id == id);
            if (prd is null) return BadRequest("ID doesn't exist");
            context.prodtuct.Remove(prd);
            context.SaveChanges();
            return Ok($"{id} deleted");
        }
    }
}
