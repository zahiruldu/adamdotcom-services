using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [KnownType(typeof(Product))]
    [CollectionDataContract(Name = "WishList", ItemName = "Product")]
    public class Wishlist : List<Product>
    {
        public Wishlist()
        {
        }

        public Wishlist(IEnumerable<Product> wishlist) : base(wishlist)
        {
        }
    }
}