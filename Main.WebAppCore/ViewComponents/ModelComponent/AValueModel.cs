using Main.Common.Enums;

namespace Main.WebAppCore;

public class AValueModel
{        
    public AValueModel() { }
    
    public AValueModel(string text, EnumAllowedVariable variable)
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

    public List<AValueModel> ChildAValueList { get; set; }

    public List<AValueModel> GetAValueListByParentId(List<AValueModel> subcategoryList, long parentId)
    {
        return subcategoryList.Where(a => a.ParentValueID == parentId).ToList();
    }
}
