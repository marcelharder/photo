namespace api.Controllers;

public class CategoryController : BaseApiController
{
    private readonly IDapperCategoryService _cat;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _ht;

    public CategoryController(IDapperCategoryService cat, UserManager<AppUser> userManager, IHttpContextAccessor ht)
    {
        _cat = cat;
        _userManager = userManager;
        _ht = ht;
    }

    [Authorize]
    [HttpGet("getAllCategories")]
    public async Task<IActionResult> Categories([FromQuery] CategoryParams cp)
    {
        var result = await _cat.GetAllCategories(cp);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("getAllowedCategories")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> AllowedCategories([FromQuery] CategoryParams cp)
    {
        // get the allowed list from the server based on who is loggedin
        List<CategoryDto> _result = new();
        // get the user that is loggedIn
       var currentUser = await _userManager.FindByNameAsync(_ht.HttpContext.User.Identity.Name);
       var loggedinUser = await _userManager.FindByIdAsync(cp.userId.ToString());

       if (loggedinUser != null && currentUser == loggedinUser)
        {
            if (loggedinUser.AllowedToSee != null)
            {
               /*  List<int> catArray = loggedinUser.AllowedToSee.Split(',')
                 .Select(t => int.Parse(t))
                 .ToList(); */

                var result = await _cat.GetAllowedCategories(loggedinUser.AllowedToSee.ToList(),cp);
                var test = new PaginationHeader(result!.CurrentPage, result!.PageSize, result!.TotalCount, result!.TotalPages);
                Response.AddPaginationHeader(test);
                return Ok(result);
            }
        }
        return BadRequest("");
    }

   
    [HttpGet("getDescription/{category}")]
    public async Task<IActionResult> GetDescription(int category)
    {
        var result = await _cat.GetSpecificCategory(category);
        if(result == null){return BadRequest("");}

        return Ok(result.Description);
    }
}
