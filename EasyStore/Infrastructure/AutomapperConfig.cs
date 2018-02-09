using AutoMapper;

namespace EasyStore.Infrastructure
{
    public static class AutomapperConfig
    {
        public static MapperConfiguration Config<TSource, TDest>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDest>();

            });
            
            return config;
        }

    }
}
