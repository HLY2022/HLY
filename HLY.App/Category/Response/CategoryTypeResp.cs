using HLY.Repository.Domain;

namespace HLY.App.Response
{
    public class CategoryTypeResp : CategoryType
    {
        public string ParentId { get; set; }
        
    }
}