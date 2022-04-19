using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Models.UserAccount
{
    public class PasswordChangeConfirmation
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
