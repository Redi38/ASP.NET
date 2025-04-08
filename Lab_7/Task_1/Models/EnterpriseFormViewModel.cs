namespace Task_1.Models
{
    public class EnterpriseFormViewModel
    {
        public AgriculturalEnterprise NewEnterprise { get; set; } = new();
        public List<AgriculturalEnterprise> Enterprises { get; set; } = new();
    }
}
