using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface ISubTypeRepo : IAsyncRepository<CorrespondenceSubType>
    {
    }
}
