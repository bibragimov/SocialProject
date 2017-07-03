namespace SocialProject.BLL.Core.CQRS
{
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }

    public interface IQueryHandler<in TQuery>
        where TQuery : IQuery
    {
        void Handle(TQuery query);
    }
}