using DietitianConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietitianConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly DietitianContext _dietitianContext;
        public ArticleController(DietitianContext dietitianContext)
        {
            _dietitianContext = dietitianContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            if (_dietitianContext.Articles == null)
            {
                return NotFound();
            }
            return await _dietitianContext.Articles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticles(int id)
        {
            if (_dietitianContext.Articles == null)
            {
                return NotFound();
            }
            var article = await _dietitianContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _dietitianContext.Articles.Add(article);
            await _dietitianContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticles), new { id = article.ArticleID }, article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutArticle(int id, Article article)
        {
            if (id != article.ArticleID)
            {
                return BadRequest();
            }
            _dietitianContext.Entry(article).State = EntityState.Modified;
            try
            {
                await _dietitianContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            if (_dietitianContext.Articles == null)
            {
                return NotFound();
            }
            var article = await _dietitianContext.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            _dietitianContext.Articles.Remove(article);
            await _dietitianContext.SaveChangesAsync();

            return Ok();
        }
    }
}
