using System.Globalization;
using api.helpers;
using fotoservice.api.helpers;

namespace fotoservice.data.interfaces;

public interface IImage
{

    Task<ActionResult<CarouselDto>> getCarouselData(int id);
    Task<PagedList<ImageDto>> getImages(ImageParams imgP);
    Task<List<Category>> getCategories();
    Task<int> addImage(ImageDto imdto);
    Task<int> deleteImage(int id);
    Task SeedImages();
    Task<int> updateImage(ImageDto image);
    Task<ImageDto> findImage(int Id);
    Task<ActionResult<List<ImageDto>>> findImagesByUser(string email);
    Task<bool> SaveChangesAsync();
    Task UpdateCategories();
}
