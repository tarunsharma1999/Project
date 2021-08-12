using System.Collections.Generic;

namespace PensionerDetail.Interface
{
    public interface ICsvManager
    {
        List<PensionerDetailModel> loadData();
    }
}
