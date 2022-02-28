using AutoMapper;
using CTA.BlazorWasm.Client.ViewModels.Shared;
using CTA.BlazorWasm.Client.ViewModels.Poc;
using CTA.BlazorWasm.Client.ViewModels.Project;
using CTA.BlazorWasm.Client.ViewModels.Tracking;
using CTA.BlazorWasm.Client.ViewModels.Threads;
using CTA.BlazorWasm.Shared.Models;


namespace CTA.BlazorWasm.Client.Services
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CorrespondenceType, GenericListVm>().ReverseMap();

            CreateMap<Project, ProjectListVm>().ReverseMap();

            CreateMap<Poc, PocListVm>().ReverseMap();
            CreateMap<Poc, GenericListVm>().ReverseMap();

            CreateMap<Status, GenericListVm>().ReverseMap();

            CreateMap<ToFrom, GenericListVm>().ReverseMap();

            CreateMap<TrackingThread, GenericListVm>().ReverseMap();
            CreateMap<TrackingThread, TrackingThreadUpdateVm>().ReverseMap();

            CreateMap<AddTrackingVm, Tracking>().ReverseMap();
            CreateMap<TrackingThread, TrackingThreadVm>()
                .ForMember(dest => dest.ProjectName,
                    opts => opts.MapFrom(src => src.Project.Name));

            CreateMap<EditTrackingVm, Tracking>().ReverseMap();
            CreateMap<Tracking, GenericListVm>().ReverseMap();
            CreateMap<Tracking, AddTrackingVm>().ReverseMap();

            CreateMap<Tracking, TrackingRowVm>()
                .ForMember(dest => dest.Status,
                    opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.ToFrom,
                    opts => opts.MapFrom(src => src.ToFrom.Name));

            CreateMap<Tracking, TrackingVm>()
                .ForMember(dest => dest.Status,
                    opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.ToFrom,
                    opts => opts.MapFrom(src => src.ToFrom.Name))
                .ForMember(dest => dest.CorrespondenceType,
                    opts => opts.MapFrom(src => src.CorrespondenceType.Name))
                .ForMember(dest => dest.Topic,
                    opts => opts.MapFrom(src => src.Thread.Name))

                .ForMember(dest => dest.Poc,
                    opts => opts.MapFrom(src => src.Poc.Name));

            CreateMap<Tracking, TrackingReportVm>()
                .ForMember(dest => dest.Status,
                    opts => opts.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.ToFromName,
                    opts => opts.MapFrom(src => src.ToFrom.Name))
                .ForMember(dest => dest.CorrespondenceType,
                    opts => opts.MapFrom(src => src.CorrespondenceType.Name))
                .ForMember(dest => dest.Poc,
                    opts => opts.MapFrom(src => src.Poc.Name))
                .ForMember(dest => dest.ProjectName,
                    opts => opts.MapFrom(src => src.Thread.Project.Name))
                .ForMember(dest => dest.TopicName,
                    opts => opts.MapFrom(src => src.Thread.Name))
                .ForMember(dest => dest.ProjectName,
                    opts => opts.MapFrom(src => src.Thread.Project.Name))
                ;
        }
    }
}
