using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Mvc.Models;
using System.Web.Security;
using SocialNetwork.Mvc.Providers;
using SocialNetwork.Mvc.Infrastructure;
using System.Web;
using System.IO;
using System.Linq;

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

        [HttpGet]
        public ActionResult Search(int? page, string searchString)
        {
            ViewBag.SearchString = searchString;
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            var result = personService.FindByFirstName(searchString).Select(m => m.ToMvcPerson());
            if (searchString == null)
                result = personService.GetAllEntities().Select(m => m.ToMvcPerson());
            var resultPaged = new PagedList<PersonViewModel>(result, 2, page ?? 1);
            if (Request.IsAjaxRequest())
            {
                return PartialView("SearchResults", resultPaged);
            }
            return View(resultPaged);
        }

        [ChildActionOnly]
        public ActionResult GetFriendRequestButton(int personId)
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            int currentPersonId = userService.GetUserByEMail(User.Identity.Name).Id;
            if (HasRequestedFriendShip(personId, currentPersonId))
                return PartialView("AcceptFriendRequest", personId);
            else if (HasRequestedFriendShip(currentPersonId, personId))
                return PartialView("RequestSendedPartial");
            else if (!AreFriends(personId, currentPersonId) && personId != currentPersonId)
                return PartialView("AddFriendPartial", personId);
            return null;
        }


        private bool AreFriends(int person1Id, int person2Id)
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            return personService.GetFriends(person1Id).Select(m => m.Id).Contains(person2Id);
        }

        private bool HasRequestedFriendShip(int senderId, int receiverId)
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            return personService.GetFriendRequestReceivers(senderId).Select(m => m.Id).Contains(receiverId);
        }
    }
}