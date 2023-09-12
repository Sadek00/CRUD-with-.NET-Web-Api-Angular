using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Gtr.Models;
using Task_Gtr.Repositories.UnitOfWork;

namespace Task_Gtr.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProduct()
        {
            var product =await _unitOfWork.Repository<Product>().GetAllAsync().ConfigureAwait(true);
            return Ok(product);
        }
        [HttpPost]
        [Route("AddProducts")]
        public async Task<IActionResult> AddProduct([FromBody]Product product)
        {
            _unitOfWork.Repository<Product>().Add(product);
            _unitOfWork.SaveChanges();
            return Ok(product);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> EditProduct([FromRoute] int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _unitOfWork.Repository<Product>().GetFirstAsync(s => s.Id == id).ConfigureAwait(true);
            if (product == null)
            {
               return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int? id, Product product)
        {
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(true);
            return Ok(product);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int? id)
        {
            var products = await _unitOfWork.Repository<Product>().GetFirstAsync(x => x.Id == id).ConfigureAwait(true);
            _unitOfWork.Repository<Product>().Remove(products);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(true);
            return Ok(products);
        }
    }
}
