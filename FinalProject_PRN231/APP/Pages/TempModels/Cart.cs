namespace APP.Pages.TempModels
{
    public class Cart
    {
        public string Email { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal TotalAmount { get; set; }

        public Item GetItemsById(int id)
        {
            return Items.FirstOrDefault(x => x.Product.ProductId == id);
        }
        public void AddItem(Item item)
        {
            var existItem = GetItemsById(item.Product.ProductId);
            if (existItem != null)
            {
                existItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public decimal GetTotalAmount()
        {
            foreach (var item in Items)
            {
                TotalAmount += (decimal)item.Product.UnitPrice * item.Quantity;
            }
            return TotalAmount;
        }

    }
}
