
using Microsoft.Net.Http.Headers;

namespace DocumentRepository.Models
{
    public class Document
    {
        public long Id{get; set ;}
        public string Name { get; set; }
        public byte[] Contents { get; set; }
    }
}
