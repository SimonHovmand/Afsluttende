namespace APITest
{
    public class Item
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Item(string Name, int ID)
        {
            this.Name = Name;
            this.ID = ID;
        }
    }
}
