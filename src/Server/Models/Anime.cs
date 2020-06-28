namespace Server.Models
{
    ///<summary>
    /// Class <c>Anime</c> models a TV series from Anime Twist.
    ///</summary>
    public class Anime
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RelativeUrl { get; set; }
    }
}