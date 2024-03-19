using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Services;
using DTOs.Product;

namespace api.Controllers
{
    /* 
    infrastructure should handle if database,fields,records dont not exist.
    and other database exceptions.to solve this i use try, catch all database exception and
    in this approach i used null as indicator of error
     */
    [ApiController, Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        const string errdb = "Database doesn't Exist";
        private readonly IProductService productService;

        public ProductController(IProductService _ps)
        {
            this.productService = _ps;
        }

        [HttpGet, Route("/api/Products")]
        public async Task<IActionResult> GetAll()
        {
            //if (HttpContext.User.Identity.IsAuthenticated == false) return Ok("Not Authenticated");
            List<CreateOrUpdateProductDTO>? ele = null;
            try
            {

                ele = await productService.GetAll();
            }
            catch
            {
                return Ok(errdb);
            }

            return ele is null ? Ok("Records NotFound") : Ok(ele);
        }

        [HttpGet, Route("{id:long}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            CreateOrUpdateProductDTO? ele = null;
            try
            {
                ele = await productService.GetOne(id);
            }
            catch
            {
                return Ok(errdb);
            }

            return ele is null ? Ok("product NotFound") : Ok(ele);
        }

        /* no one asked for such a funtion */
        //[HttpGet, Route("{name:alpha}")]
        //public async Task<IActionResult> GetByName([FromRoute] string name)
        //{
        //    CreateOrUpdateProductDTO? ele = null;
        //    try
        //    {
        //        var all = await productService.GetAll();
        //        ele = all.FirstOrDefault(x => x.Name.Contains(name));
        //    }
        //    catch
        //    {
        //        return Ok(errdb);
        //    }

        //    return ele is null ? Ok("product NotFound") : Ok(ele);
        //}

        /* in design */
        //public async Task<IActionResult> AddToCart([FromRoute] string name)
        //{
        // return Ok("ok");
        //}

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var ooo = product;
                var ele = await productService.Create(product);

                if (ele is null) return NotFound("A weird error happened during creation");

                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/");
                var uri = location.AbsoluteUri;
                /* this needs to change, waiting they make up thier mind and choose a model to follow */
                return Created(uri + ele.Entity.name, " Created");
            }
            return BadRequest(ModelState);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var ele = await productService.Update(product);

                if (ele is null) return NotFound("A weird error happened during update");

                var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/");
                var uri = location.AbsoluteUri;
                return Ok($"{uri + product.name} is Updated");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Delete"), Authorize]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            if (id <= 0) return BadRequest("invalid id");
            CreateOrUpdateProductDTO? prd = null;
            try
            {
                prd = await productService.GetOne(id);
            }
            catch
            {
                return Ok(errdb);
            }

            if (prd is null) return BadRequest("ID doesn't exist");

            var ele = await productService.Delete(prd);

            return Ok($"{ele.Entity.name} is deleted");
        }
    }
}
