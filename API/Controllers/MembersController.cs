using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MembersController(AppDbContext context) : BaseApiController
    {
        // private readonly AppDbContext _context;
        // public MembersController(AppDbContext context)
        // {
        //     _context = context;
        // }

        //* ActionResult pozwala nam na zwrócenie Http responses
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;
        }

        [HttpGet("{id}")] // localhost/5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMemberById(string id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMemberById(string id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            context.Users.Remove(member);
            await context.SaveChangesAsync();
            return Ok();
        }


    }
}
