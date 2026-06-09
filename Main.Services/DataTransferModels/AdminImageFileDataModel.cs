namespace DataTransferModel;

public class ImageFile
{
    public ImageFile ( )
    {
    }

    public ImageFile ( byte[] fileContent)
    {
        FileContent = fileContent;
        IsNew = true;
    }

    public ImageFile ( byte[] fileContent, int? postId, int fileId )
    {
        FileContent = fileContent;
        IsNew = false;
        FileID = fileId;
        PostID = postId;
    }

    public int FileID { get; set; } 
   
    public byte[] FileContent { get; set; }
   
    public int? PostID { get; set; }

    public bool IsNew { get; set; }
}
