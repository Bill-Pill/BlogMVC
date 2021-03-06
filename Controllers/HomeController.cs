using BlogMVC.Data;
using BlogMVC.Models;
using BlogMVC.Services;
using BlogMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BlogMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            //var blogs = _context.Blogs.Where(
            //    b => b.Posts.Any(p => p.ReadyStatus == Enums.ReadyStatus.ProductionReady))
            //    .Include(b => b.BlogUser)
            //    .OrderByDescending(b => b.Created)
            //    .ToPagedListAsync(pageNumber, pageSize);

            var blogs = _context.Blogs
                .Include(b => b.BlogUser)
                .OrderByDescending(b => b.Created)
                .ToPagedListAsync(pageNumber, pageSize);

            ViewData["HeaderImage"] = "/images/home-bg.png";
            ViewData["MainText"] = "Code of Billy";
            ViewData["SubText"] = "Thoughts and musings related to programming.";

            return View(await blogs);
        }


        public IActionResult About()
        {
            ViewData["HeaderImage"] = "/images/home-bg.png";
            ViewData["MainText"] = "Code of Billy";
            ViewData["SubText"] = "About";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["HeaderImage"] = "/images/home-bg.png";
            ViewData["MainText"] = "Code of Billy";
            ViewData["SubText"] = "Contact";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            // Where we'll be emailing
            model.Message = $"{model.Message} <hr/> Phone: {model.Phone}";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}