using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectC_v2.Models.ViewModels
{
    public sealed class ProductViewModel : Product
    {
        public Inventory ActiveInventory { get; set; } = null;
        public ProductViewModel(Product p)
        {
            this.Description = p.Description;
            this.GameType = p.GameType;
            this.GameTypeId = p.GameTypeId;
            this.Inventories = p.Inventories;
            this.Name = p.Name;
            this.PGRating = p.PGRating;
            this.PGRatingId = p.PGRatingId;
            this.ProductFeatures = p.ProductFeatures;
            this.ProductId = p.ProductId;
            this.ProductImages = p.ProductImages;
            this.ProductVideos = p.ProductVideos;
            this.Publisher = p.Publisher;
            this.PublisherId = p.PublisherId;
            this.ReleaseDate = p.ReleaseDate;
        }
    }
}
