namespace BlazorAppOtus.Shared.Models
{
    public class Size
    {
        public Size(int id, string key, string name)
        {
            this.Id = id;
            this.Key = key;
            this.Name = name;
        }
        public Size(string key, string name)
        {
            this.Key = key;
            this.Name = name;
        }
        public Size()
        {

        }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
 
    }
}
