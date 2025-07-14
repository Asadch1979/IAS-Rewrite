using System;

namespace AIS.Models
    {
    public class CircularDocumentModel
        {
        public int DocId { get; set; }
        public int CircularId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public byte[] FileBlob { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        }

    }
