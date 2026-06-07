using Main.Common.Enums;

namespace Main.WebAppCore.ViewCompont;

public class AllowedValueModel
{        
    public AllowedValueModel() { }
    
    public AllowedValueModel(string text, EnumAllowedVariable variable)
    {
        if(string.IsNullOrEmpty(text))
            throw new ArgumentException("Text not provided.");
        Text = text;
        Variable = variable;
    }
    
    public long ValueID { get; set; }
    
    public string Text { get; set; }
    
    public EnumAllowedVariable Variable { get; set; }

    public long ParentValueID { get; set; }

    public string IconLink { get; set; }

    public List<AllowedValueModel> ChildAValueList { get; set; }

    public List<AllowedValueModel> GetAValueListByParentId(List<AllowedValueModel> subcategoryList, long parentId)
    {
        return subcategoryList.Where(a => a.ParentValueID == parentId).ToList();
    }
}
