namespace BootlegEmagService.Models
{
    internal class updateProductDTO : Product.Models.updateProductDTO
    {
        public updateProductDTO(string id, string name, string category, string price, string image)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Image = image;
        }
    }
}