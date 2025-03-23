namespace Task_6.Models
{
    public class SurveyModel
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string VisitDate { get; set; } = string.Empty;
        public int Age { get; set; }
        public string FavoriteCuisine { get; set; } = string.Empty;
        public string DesiredDishes { get; set; } = string.Empty;
        public string ReasonForChoosing { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
    }
}