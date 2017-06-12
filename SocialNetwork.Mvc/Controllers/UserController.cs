using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Mvc.Models;
using System.Web.Security;
using SocialNetwork.Mvc.Providers;
using SocialNetwork.Mvc.Infrastructure;
using System.Web;
using System.IO;

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
        [HttpGet]
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
            model.Avatar = personService.GetById(model.Id).Avatar;
            personService.UpdateEntity(model.ToBllPerson());
            return RedirectToAction(nameof(Index));
        }

        
        public ActionResult RenderAvatar(int id)
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            var image = personService.GetById(id).Avatar;
            if (image == null)
                return null;
            return File(image, "image/jpg");
        }

        [HttpPost]
        public ActionResult ChangeAvatar()
        {
            var file = Request.Files["avatar"];
            if (file != null)
            {
                IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
                var person = userService.GetUserByEMail(User.Identity.Name).Person;
                byte[] avatar = new byte[file.ContentLength];
                file.InputStream.Read(avatar, 0, file.ContentLength);
                person.Avatar = avatar;
                personService.UpdateEntity(person);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}