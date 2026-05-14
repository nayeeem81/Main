using SkiaSharp;

namespace Main.Common
{
    public class ImageHelper
    {
        public ImageHelper() { }


        public static (int width,int height) GetImageDimensions ( byte[] imageData )
        {
            using var stream = new MemoryStream ( imageData );

            using ( var codec = SKCodec.Create ( stream ) )
            {
                return codec == null ? throw new Exception ( "Invalid image data" ) : (codec.Info.Width, codec.Info.Height);
            }
        }
    }
}



