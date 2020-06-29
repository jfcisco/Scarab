namespace Player
{
    public interface IPlayer
    {
        void ViewAnime(string relativeUrl, int episodeNumber = 1);
        void Play();
        void ToggleFullScreen();
    }
}