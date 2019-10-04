using System.Collections.Generic;

namespace Application.ContentBasedRouter
{
    public interface IContentBasedRouter
    {
        IEnumerable<string> ComputeAdditionalRoutes(string message);
    }
}