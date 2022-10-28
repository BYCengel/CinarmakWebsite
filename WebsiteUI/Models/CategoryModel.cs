namespace WebsiteUI.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
        public int ViewCount { get; set; } = 0;
        public string Name { get; set; }
		public string Url { get; set; } = "";
	}
}
