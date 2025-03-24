namespace _4ThWallCafe.MVC.Models
{
    public class MenuViewModel
    {
        public string TimeOfDayName { get; set; }
        public List<string> Categories { get; set; }
        public Dictionary<string, List<MenuItem>> ItemsByCategory { get; set; }
    }
}
