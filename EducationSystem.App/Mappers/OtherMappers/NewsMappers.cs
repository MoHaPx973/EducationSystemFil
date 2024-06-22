using EducationSystem.Domain.Other;
using EducationSystem.Shared.Other;

namespace EducationSystem.App.Mappers.OtherMappers
{
    static public class NewsMappers
    {
        static public NewsDto? ToDto(this NewsData? item)
        {
            if (item != null)
            {
                return new NewsDto
                {
                   Id = item.Id,
                   Path = item.Path,
                   Text = item.Text,
                   Title = item.Title
                };
            }
            return null;
        }

        static public NewsData? ToEntity(this NewsDto? item)
        {
            if (item != null)
            {
                return new NewsData
                {
                    Id = item.Id,
                    Path = item.Path,
                    Text = item.Text,
                    Title = item.Title
                };
            }
            return null;
        }
    }
}
