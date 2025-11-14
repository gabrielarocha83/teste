namespace Yara.WebApi.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfile(typeof(AppService.Mappings.MappingProfile));
            });
        }
    }
}