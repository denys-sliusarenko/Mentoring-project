using AutoMapper;
using MentoringProject.Mapper;

namespace MentoringProject.TestDataConfiguration
{
    public static class MapperConfig
    {
        private static IMapper _mapper;

        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            return _mapper;
        }
    }
}
