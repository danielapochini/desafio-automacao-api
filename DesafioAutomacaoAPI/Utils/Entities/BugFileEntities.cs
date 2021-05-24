namespace DesafioAutomacaoAPI.Utils.Entities
{
    public class BugFileEntities
    {
		public int Id { get; set; }
		public int BugId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Diskfile { get; set; }
		public string Filename { get; set; }
		public string Folder { get; set; }
		public int Filesize { get; set; }
		public string FileType { get; set; }
		public byte[] Content { get; set; }
		public int DateAdded { get; set; }
		public int UserId { get; set; }
		public int BugnoteId { get; set; }
	}
}
