using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [KnownType(typeof(Review))]
    [CollectionDataContract(Name = "Reviews", ItemName = "Review")]
    public class Reviews : List<Review>
    {
        public Reviews()
        {
        }

        public Reviews(IEnumerable<Review> reviews) : base(reviews)
        {
        }
    }
}