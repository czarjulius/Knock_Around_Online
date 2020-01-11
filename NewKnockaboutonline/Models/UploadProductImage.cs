using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewKnockaboutonline.Models
{
    public class UploadProductImage
    {
    }
    [MetadataType(typeof(UploadProductImage))]

    public partial class PRODUCT
    {
        public HttpPostedFileWrapper ImageBgFile { get; set; }
        public HttpPostedFileWrapper ImageSm1File { get; set; }
        public HttpPostedFileWrapper ImageSm2File { get; set; }
        public HttpPostedFileWrapper ImageSm3File { get; set; }
        public HttpPostedFileWrapper ImageSm4File { get; set; }

    }
}