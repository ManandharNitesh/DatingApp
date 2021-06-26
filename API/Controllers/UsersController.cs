using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[Controller]")]
    //not needed as attribute are inherited from BaseApiController
    public class UsersController : BaseApiController  //ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;

        }

        /*

        Synchronus
          [HttpGet]
          public ActionResult<IEnumerable<AppUser>> GetUsers(){
              // public ActionResult<List<AppUser>> GetUsers(){
              return   _context.Users.ToList();

          }

    //api/users/3
           [HttpGet("{id}")]
          public ActionResult<AppUser> GetUser(int id){
              // public ActionResult<List<AppUser>> GetUsers(){
                  return   _context.Users.Find(id);//Finds Primary key


          }
  */
        //Asynchronus
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            // public ActionResult<List<AppUser>> GetUsers(){
            return await _context.Users.ToListAsync();


        }

        //api/users/3
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            // public ActionResult<List<AppUser>> GetUsers(){
            return await _context.Users.FindAsync(id);//Finds Primary key


        }

    }
}