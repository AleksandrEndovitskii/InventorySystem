namespace Models.InventoryItems
{
    public class InventoryItemModel
    {
        public int Id;
        public string Name;
        public string Type;
        public float Weight;

        public InventoryItemModel(int id, string name, string type, float weight)
        {
            Id = id;
            Name = name;
            Type = type;
            Weight = weight;
        }
    }
}
