namespace DocumentRepository.Models
{
    /// <summary>
    /// The model of the data to be stored. 
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Id used by entity framework as key.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name of the file, taken from file metadata
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Contents of the file passed into the service
        /// </summary>
        public byte[] Contents { get; set; }
    }
}
