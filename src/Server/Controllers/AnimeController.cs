using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Player;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeContext _context;
        private readonly IPlayer _player;

        public AnimeController(AnimeContext context, IPlayer player)
        {
            _context = context;
            _player = player;
        }

        // GET: api/Anime
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimeList()
        {
            return await _context.AnimeList.ToListAsync();
        }

        // GET: api/Anime/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnime(long id)
        {
            var anime = await _context.AnimeList.FindAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            _player.ViewAnime(anime.RelativeUrl);
            
            return anime;
        }

        // GET: api/Anime/5/1
        [HttpGet("{id}/{ep}")]
        public async Task<ActionResult<Anime>> GetAnime(long id, int ep)
        {
            Anime anime = await _context.AnimeList.FindAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            _player.ViewAnime(anime.RelativeUrl, ep);
            
            return anime;
        }

        // PUT: api/Anime/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnime(long id, Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest();
            }

            _context.Entry(anime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Anime
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Anime>> PostAnime(Anime anime)
        {
            _context.AnimeList.Add(anime);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnime), new { id = anime.Id }, anime);
        }

        // POST: api/Anime/player
        // Handle commands to the player
        [HttpPost("player")]
        public async Task<IActionResult> PostPlayer([FromBody] string command)
        {
            await Task.Run(() => {
                switch (command.ToUpper())
                {
                    case "PLAY":
                        _player.Play();
                        break;
                    
                    case "FULLSCREEN":
                        _player.ToggleFullScreen();
                        break;
                }
            });

            return NoContent();
        }

        // DELETE: api/Anime/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Anime>> DeleteAnime(long id)
        {
            var anime = await _context.AnimeList.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }

            _context.AnimeList.Remove(anime);
            await _context.SaveChangesAsync();

            return anime;
        }

        private bool AnimeExists(long id)
        {
            return _context.AnimeList.Any(e => e.Id == id);
        }
    }
}
