using fotoservice.api.data.interfaces;

namespace fotoservice.data.implementations;
public class UserRepo : IUsers
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;

    public UserRepo(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext context)
    {
        _userManager = userManager;
        _mapper = mapper;
        _context = context;

    }

    public async Task<int> Delete(int id)
    {


        var selectedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (selectedUser != null)
        {
            _context.Users.Remove(selectedUser);
            return 1;
        }
        else
        {
            return 0;
        }



    }

    public Task<AppUser> GetChefsByHospital(int center_id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserForReturnDto> GetUser(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        //var dto = new UserForReturnDto();


        if (user != null) { return _mapper.Map<UserForReturnDto>(user); }
        else {return null;}

    }

    public async Task<AppUser> GetUserByMail(string email)
    {
        if (email != null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null) { return user; }
        }

        return null;
    }

    public Task<bool> GetUserLtk(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var dtolist = new List<UserDto>();
        var fullUsers = await _userManager.Users.ToListAsync();

        foreach (AppUser u in fullUsers)
        {

            dtolist.Add(_mapper.Map<UserDto>(u));
        }




        return dtolist;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }



    public Task<bool> UpdatePayment(DateTime d, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Update(UserForUpdateDto p)
    {
        var old_user = await _userManager.FindByIdAsync(p.UserId.ToString());
        if (old_user != null)
        {
           var new_user = _mapper.Map<UserForUpdateDto, AppUser>(p, old_user);
           await _userManager.UpdateAsync(new_user);
           return 1;
        }
        else { return 0; }
    }
}