using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
	public class TodoItem
	{
		[Key]
		public long Id { get; set; }
		public string? Description { get; set; }
		public bool IsComplted { get; set; }
	}
}

