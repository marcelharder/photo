using AutoMapper.QueryableExtensions;
using fotoservice.api.data.interfaces;

namespace api.data.implementations
{
    public class ImageImplementation : IImage
    {
        private ApplicationDbContext _context;
        private DapperContext _dap;
        private UserManager<AppUser> _userManager;
        private IHttpContextAccessor _ht;
        private readonly IMapper _mapper;
        private readonly IUsers _user;

        private readonly IConfiguration _conf;

        public ImageImplementation(
            IConfiguration conf,
            DapperContext dap,
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<AppUser> userManager,
            IHttpContextAccessor ht,
            IUsers user
        )
        {
            _user = user;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _ht = ht;
            _dap = dap;
            _conf = conf;
        }

        public async Task<PagedList<ImageDto>> getImages(ImageParams imgP)
        {
            IQueryable<ImageDto> images;

            if (imgP.Category == 1)
            {
                images = _context
                    .Images.ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }
            else
            {
                images = _context
                    .Images.Where(x => x.Category == imgP.Category)
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }

            return PagedList<ImageDto>.CreateAsync(images, imgP.PageNumber, imgP.PageSize);
        }

        public async Task<ImageDto> findImage(int Id)
        {
            var selectedImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<ImageDto>(selectedImage);
        }

        public async Task<ActionResult<List<ImageDto>>> findImagesByUser(string email)
        {
            // get the categories that his user can see
            string[] cararray = { };
            IQueryable<ImageDto> images;
            var l = new List<ImageDto>();

            var selectedUser = await _user.GetUserByMail(email);

            if (selectedUser != null)
            {
                var categories = selectedUser.AllowedToSee;
                if (categories != null)
                {
                    cararray = categories.Split(",");
                }
            }
            foreach (string s in cararray)
            {
                images = _context
                    .Images.Where(x => x.Category == Convert.ToInt32(s))
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();

                l.AddRange(await images.ToListAsync());
            }
            return l;
        }

      /*   public async Task<ActionResult<List<ImageDto>>> GetImagesByCategory(ImageParams ip)
        {
            string[] cararray = { };
            var l = new List<ImageDto>();
            // get all the images from this category
            var images = await _context.Images.Where(x => x.Category == ip.Id).ToArrayAsync();
            // return image DTO
            foreach (fotoservice.data.models.Image im in images)
            {
                l.Add(_mapper.Map<ImageDto>(im));
            }

            return l;
        } */

        public async Task<int> deleteImage(int id)
        {
            var selectedImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (selectedImage != null)
            {
                _context.Images.Remove(selectedImage);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> updateImage(ImageDto imagedto)
        {
            await Task.Run(() =>
            {
                _context.Images.Update(_mapper.Map<fotoservice.data.models.Image>(imagedto));
            });

            return 1;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> addImage(ImageDto imDto)
        {
            var result = 1;
            var img = _mapper.Map<fotoservice.data.models.Image>(imDto);
            var help = _context.Images.Add(img);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ActionResult<CarouselDto>> getCarouselData(int id)
        {
            var response = new CarouselDto();
            var selectedImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (selectedImage != null)
            {
                var images = await _context
                    .Images.Where(x => x.Category == selectedImage.Category)
                    .ToArrayAsync();
                var test = images.ToList();
                var numberOfImages = test.Count();

                if (numberOfImages == 1)
                {
                    response.numberOfImages = 1;
                    response.ShowL = false;
                    response.ShowR = false;
                    response.nextImageIdL = null;
                    response.nextImageIdR = null;
                }
                else
                {
                    var lastImage = test.LastOrDefault();
                    var firstImage = test.FirstOrDefault();
                    int imagelocation = test.FindIndex(x => x == selectedImage);

                    if (imagelocation == 0) // dit is het eerste item
                    {
                        response.numberOfImages = test.Count();
                        response.ShowL = false;
                        response.ShowR = true;
                        response.nextImageIdR = test[imagelocation + 1].Id;
                    }
                    else
                    {
                        if (imagelocation == (numberOfImages - 1)) // dit is het laatste item
                        {
                            response.numberOfImages = test.Count();
                            response.ShowR = false;
                            response.ShowL = true;
                            response.nextImageIdL = test[imagelocation - 1].Id;
                        }
                        else
                        {
                            response.numberOfImages = test.Count();
                            response.ShowL = true;
                            response.ShowR = true;
                            response.nextImageIdL = test[imagelocation - 1].Id;
                            response.nextImageIdR = test[imagelocation + 1].Id;
                        }
                    }

                    response.category = selectedImage.Category;
                    return response;
                }
            }
            return null;
        }

        public async Task<List<Category>> getCategories()
        {
            return _context.Categories.AsList();
        }

        public async Task SeedImages()
        {
            var counter = 0;
            var catList = _context.Categories.AsList();
            ImageDto test;
            if (catList != null)
            {
                for (int x = 0; x < catList.Count; x++)
                {
                    if (catList[x].Number_of_images != 0)
                    {
                        counter += (int)catList[x].Number_of_images;

                        for (int y = 0; y < counter; y++)
                        {
                            string? url = catList[x].Name + "/" + x.ToString() + ".jpg";
                            test = new ImageDto
                            {
                                Id = counter.ToString(),
                                ImageUrl = url,
                                YearTaken = 1995,
                                Location = "",
                                Familie = "",
                                Category = catList[x].Id,
                                Series = "",
                                Spare1 = "",
                                Spare2 = "",
                                Spare3 = "",
                            };
                            await addImage(test);
                        }
                    }

                    //addImage(catList[x],counter,url,_dapper);
                }
            }

            return;
        }
    
        public async Task UpdateCategories(){

        // fill a list with the id of the first image of each category
        List<int> listOfIds = new List<int>();
        var counter = 0;

        var allCategories = await _context.Categories.ToListAsync();
        foreach(Category cat in allCategories){
            var images = _context.Images.Where(global => global.Category == cat.Id).ToList();
            listOfIds.Add(images[0].Id);
        }

       // with this list update the Categories
         foreach(Category cat in allCategories){
            cat.MainPhoto = listOfIds[counter];
            _context.Categories.Update(cat);
            await _context.SaveChangesAsync();
            counter++;
         }
       //  var allImages = await context.Images.ToListAsync();
    }

       
    }
}
