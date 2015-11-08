using System.Collections.Generic;

namespace ReadlingList.Domain
{
    public interface IReadingListQuery
    {
        IEnumerable<ItemToRead> GetAllItemsToRead();
        ItemToRead GetItemToRead(string id);
        IEnumerable<ItemToRead> GetItemsToReadByTag(string tag);
    }
}
