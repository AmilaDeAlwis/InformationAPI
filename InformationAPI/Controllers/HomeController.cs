using InformationAPI.interfaces;
using InformationAPI.Misc;
using Microsoft.AspNetCore.Mvc;

namespace InformationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IManageService _manageService;

        public HomeController(IManageService manageService)
        {
            _manageService = manageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInformation([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var (posts, total) = await _manageService.GetAllInformationAsync(page, pageSize);
                var result = new
                {
                    Data = posts.Select(p => new { p.Title, p.Body }),
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = total
                };
                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInformaionById(int id)
        {
            try
            {
                var post = await _manageService.GetInformationByIdAsync(id);
                if (post == null) return NotFound();
                return Ok(post);
            }
            catch (ServiceException ex)
            {
                // Log ex here if desired
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
