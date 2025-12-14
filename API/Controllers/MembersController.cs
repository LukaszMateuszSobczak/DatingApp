using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")] // localhost/5001/api/members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
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
            if(member == null)
            {
                return NotFound();
            }
            return member;
        }
    }
}
