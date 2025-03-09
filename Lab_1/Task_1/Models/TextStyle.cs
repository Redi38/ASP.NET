namespace Task_1.Models
{
    public class TextStyle
    {
        public int Id { get; set; }
        public string BackgroundImage { get; set; } = ""; // Фонова картинка
        public string BlockWidth { get; set; } = "100%";  // Ширина блоку
        public string BlockMargin { get; set; } = "0px";  // Відступи блоку
    }
}
