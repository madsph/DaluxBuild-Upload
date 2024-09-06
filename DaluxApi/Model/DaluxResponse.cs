using System.Collections.Generic;

namespace DaluxApi.Model
{
    public class DaluxResponse<T>
    {
        public List<Link> Links { get; set; }
        public T Data { get; set; }
    }
}