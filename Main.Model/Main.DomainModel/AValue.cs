using System.ComponentModel.DataAnnotations;
using Main.Common.Enums;

namespace Domain.Model;

public class AValue : BaseEntity
{
    public AValue()
    {
    }

    public AValue(EnumCountry country, string text, EnumTenantVariable variable)
    {
        if(string.IsNullOrEmpty(text))
            throw new ArgumentException("Text not provided.");
        Text = text;
        Variable = variable;
    }

    [Key]
    public long ValueID { get; set; }

    
    [Required]
    public string Text { get; set; }

    
    [Required]
    public EnumTenantVariable Variable { get; set; }

   
    public long ParentValueId { get; set; }        
}
