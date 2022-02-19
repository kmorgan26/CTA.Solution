﻿using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContractsTracker.Persistence.Repositories
{
    public class StatusRepo : BaseRepository<Status>, IStatusRepo
    {
        public StatusRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext) { }
    }
}