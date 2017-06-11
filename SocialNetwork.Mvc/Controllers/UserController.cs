using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Mvc.Models;
using System.Web.Security;
using SocialNetwork.Mvc.Providers;
using SocialNetwork.Mvc.Infrastructure;
using System.Web;

namespace SocialNetwork.Mvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        // GET: User
        public ActionResult Index(int? id)
        {
            PersonViewModel person;
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            if (id.HasValue)
            {
                person = personService.GetById(id.Value).ToMvcPerson();
                if (person == null)
                    throw new HttpException(404, "Page not found");
                ViewBag.Email = userService.GetUser(id.Value).EMail;
            }
            else
            {
                ViewBag.Email = User.Identity.Name;
                person = personService.GetById(userService.GetUserByEMail(User.Identity.Name).Id).ToMvcPerson();

            }
            return View(person);
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var person = userService.GetUserByEMail(User.Identity.Name).Person;

            return View(person.ToMvcPerson());
        }
        [HttpPost]
        public ActionResult Edit(PersonViewModel model)
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            personService.UpdateEntity(model.ToBllPerson());
            return RedirectToAction(nameof(Index));
        }
    }
}