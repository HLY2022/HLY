namespace HLY.App
{
    public interface IForm
    {
        string GetSql(Repository.Domain.Form form, string dbType);
    }
}