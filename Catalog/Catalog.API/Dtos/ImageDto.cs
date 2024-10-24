using Catalog.Domain.Models;

namespace Catalog.API.Dtos;

public class ImageDto
{
    public ImageDto()
    {
    }

    public ImageDto(Image image)
    {
        Name = image.Name;
        Thumb = image.Thumb;
        Small = image.Small;
        Medium = image.Medium;
        Large = image.Large;
        Order = image.Order;
    }

    public string Name { get; set; }
    public string Thumb { get; set; }
    public string Small { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
    public int Order { get; set; }
}