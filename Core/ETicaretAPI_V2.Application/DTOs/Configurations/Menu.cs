namespace ETicaretAPI_V2.Application.DTOs.Configurations
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; } = new();
    }
}
