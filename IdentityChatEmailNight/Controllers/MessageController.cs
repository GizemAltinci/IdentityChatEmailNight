using IdentityChatEmailNight.Context;
using IdentityChatEmailNight.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmailNight.Controllers
{
    public class MessageController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize] //Bu durumda methoda sadece login yapmış kullanıcılar girebilir.
        public async Task<IActionResult>  Inbox()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name); //Burada geçen "Name"ler aslında userName'e karşılık geliyo
            ViewBag.email = values.Email;
            ViewBag.v1=values.Name+" "+ values.Surname;

            var values2=_context.Messages.Where(x=>x.ReceiverEmail==values.Email).ToList();

            return View(values2);
        }
        public IActionResult Sendbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.IsRead = false;
            message.SendDate = DateTime.Now;
            //bu iki kod sadece backenden manuel gidecek
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("Sendbox");
           
        }
    }
}
