using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolt.Sample.WebApi.Features.Shared.Ports
{
    public interface IUniqueId
    {
        Guid New { get; }
    }
}
