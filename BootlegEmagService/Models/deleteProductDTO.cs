namespace BootlegEmagService.Models
{
    internal class deleteProductDTO : Product.Models.deleteProductDTO
    {
        public deleteProductDTO(string id, string name, string category, string price, string image)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Image = image;
        }
    }
}