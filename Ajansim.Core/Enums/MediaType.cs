using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajansim.Core.Enums
{
    public enum MediaType
    {
        Image = 1,     // jpg, png, webp, gif
        Video = 2,     // mp4, mov, avi
        Document = 3,  // pdf, docx, txt, pptx
        Audio = 4,     // mp3, wav
        Other = 99     // tanımsız veya özel türler
    }
}
