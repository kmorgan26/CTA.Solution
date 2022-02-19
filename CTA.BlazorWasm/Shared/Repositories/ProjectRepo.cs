﻿using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContractsTracker.Persistence.Repositories
{
    public class ProjectRepo : BaseRepository<Project>, IProjectRepo
    {
        public ProjectRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext){ }
    }
}