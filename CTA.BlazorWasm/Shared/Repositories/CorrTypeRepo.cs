using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Shared.Repositories
{
    public class CorrTypeRepo : BaseRepository<CorrespondenceType>, ICorrTypeRepo
    {
        public CorrTypeRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext) { }
    }
}
