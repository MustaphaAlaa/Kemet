using System.Net;
using Entities;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/Category")]
[ApiController]
public class CategoryController : ControllerBase
{   
    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        this.categoryService = categoryService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<CategoryController> _logger;
    ICategoryService categoryService;

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        try
        {
            _logger.LogInformation($"CategoryController => Index()");
            var categories = await categoryService.RetrieveAllAsync();
            _response.Result = categories;
            _response.IsSuccess = true;
            _response.StatusCode = categories.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpGet("index/{CategoryId:int}")]
    public async Task<IActionResult> CategoryById(int CategoryId)
    {
        try
        {
            _logger.LogInformation($"CategoryController => Index()");
            var category = await categoryService.GetById(CategoryId);
            _response.Result = category;
            _response.IsSuccess = true;
            _response.StatusCode = category is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }
}
