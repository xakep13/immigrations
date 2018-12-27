using System;

namespace Bissoft.CouncilCMS.Core
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool Deleted { get; set; }
    }
}
