using System;
using Xunit;
using Player;

namespace Player.Tests
{
    public class WebPlayerTests : IDisposable
    {
        private WebPlayer player;
    
        // Constructor for setup
        public WebPlayerTests()
        {
            player = new WebPlayer();
        }

        [Fact]
        public void WebPlayerCanPlayBerserk()
        {
            player.Play("berserk");
        }

        [Fact]
        public void WebPlayerCanGoToAnEpisode()
        {

        }

        public void Dispose()
        {
            player.Dispose();
        }
    }
}
