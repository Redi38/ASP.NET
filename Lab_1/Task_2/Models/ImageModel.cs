public class ImageModel
{
    public int Id { get; set; }        // Ідентифікатор
    public required string Url { get; set; }    // Посилання на зображення
    public int Width { get; set; }     // Ширина зображення
    public int Height { get; set; }    // Висота зображення
}
