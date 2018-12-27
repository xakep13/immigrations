namespace Bissoft.CouncilCMS.Core
{
    public interface IService
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
