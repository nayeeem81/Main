namespace Main.Common
{
    public class CartSingleItemModel
    {
        public int SlNo { get; set; }

        public int PostId { get; set; }

        public string? ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitDiscountedPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalUnitsPrice => UnitDiscountedPrice * Quantity;
    }
}
