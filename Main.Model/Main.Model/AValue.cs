using System.ComponentModel.DataAnnotations;

using Main.Common.EnumClasses;

namespace Main.Model
{
    public class AValue : BaseEntity
    {
        public AValue()
        {
        }

        public AValue(EnumCountry country, string text, EnumAllowedVariable variable)
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
        public EnumAllowedVariable Variable { get; set; }

       
        public long ParentValueId { get; set; }        
    }
}
