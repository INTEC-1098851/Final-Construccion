namespace BlazorAppOtus.Shared.Models
{
    public class Category
    {
        public Category(int id,string key,string name)
        {
            this.Id = id; 
            this.Key = key;
            this.Name = name;
        }
        public Category(string key, string name)
        {
            this.Key=key;
            this.Name = name;
        }
        public Category()
        {

        }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
 
    }
}
