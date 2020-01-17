namespace TraderForPoe.Classes
{
    public class Item : ItemBase
    {
        public Item(string itemArg, decimal amountArg = 0) : base(itemArg, amountArg)
        {
            //TODO
            //throw new NotImplementedException();

        }

        public ItemBase Price { get; set; }

        public decimal Ratio
        {
            //TODO Implement ratio
            get;
            set;
        }
    }
}
