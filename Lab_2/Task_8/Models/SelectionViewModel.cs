namespace Task_8.Models
{
    public class SelectionViewModel
    {
        public string City { get; set; }
        public string Transport { get; set; }
        public bool ShowSelection { get; set; }

        // Конструктор за замовчуванням
        public SelectionViewModel()
        {
            City = "Київ";
            Transport = "Літак";
            ShowSelection = false;
        }
    }
}