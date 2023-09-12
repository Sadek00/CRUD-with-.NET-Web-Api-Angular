using Task_Gtr.Models;
using Task_Gtr.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_Gtr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("GetEmployee")]
        public IEnumerable<Employee> GetEmployee()
        {
            return _unitOfWork.Repository<Employee>().Get();
        }

    }
}
