namespace Main.Common.Model;

public class ParentChildVriableModel
{
    public ParentChildVriableModel() { }

    public int ValueID 
    { 
        get; set; 
    }

    public int ParentValueID
    {
        get; set;
    }

    public EnumAllowedVariable Variable
    {
        get; set;
    }

    public string Text
    {
        get; set;
    }

    public string IconLink
    {
        get; set;
    }
}
