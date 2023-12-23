using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.UserViewModels;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Identity;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolesRepository rolesRepository;
        private readonly ILogger _logger;


        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IRolesRepository rolesRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.rolesRepository = rolesRepository;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult Index()

        {
            // Call GetUsersJson action to retrieve users' JSON data
            IActionResult usersJsonResult = GetUsersJson();

            // Access the JSON data from the IActionResult
            JsonResult jsonResult = usersJsonResult as JsonResult;
            var usersData = jsonResult?.Value; // Contains the users' JSON data

            // Process 'usersData' as needed in your Index2 action

            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = new UserViewModel();
            model.Id = id;
            return View("Add", model);
        }

        [HttpPost]

        public async Task<IActionResult> AddAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = "";
                if (string.IsNullOrEmpty(model.Id))
                {
                    var user = new ApplicationUser { Name = model.Name, Surname = model.Surname, UserName = model.Email, Email = model.Email, EmailConfirmed = model.EmailConfirmed, PhoneNumber = model.PhoneNumber, PhoneNumberConfirmed = model.PhoneNumberConfirmed };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        userId = user.Id;
                        var selectedRole = rolesRepository.GetByStringId(model.RoleId);
                        if (selectedRole != null)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, selectedRole.Name);
                            if (roleResult.Succeeded)
                            {
                                _logger.LogInformation($"User created with role {selectedRole.Name}");
                            }
                        }

                        _logger.LogInformation("User created a new account with password.");
                    }
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        userId = user.Id;
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        //user.Email = model.Email;
                        user.EmailConfirmed = model.EmailConfirmed;
                        user.PhoneNumber = model.PhoneNumber;
                        user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;

                        var editResult = await _userManager.UpdateAsync(user);

                        if (editResult.Succeeded)
                        {
                            var currentRole = rolesRepository.GetByUserId(user.Id);
                            if (currentRole != null && currentRole.Id != model.RoleId)
                            {
                                var result = await _userManager.RemoveFromRoleAsync(user, currentRole.Name);
                                if (result.Succeeded)
                                {
                                    var selectedRole = rolesRepository.GetByStringId(model.RoleId);
                                    if (selectedRole != null)
                                    {
                                        await _userManager.AddToRoleAsync(user, selectedRole.Name);
                                    }
                                }
                            }
                        }


                    }
                }

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetUsersJson()
        {
            // GetAll() - metod qe e kemi brenda IUserRepository
            var users = _userRepository.GetAll();

            var result = users.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                surname = x.Surname,
                email = x.Email,
                emailConfirmed = x.EmailConfirmed
            });

            return new JsonResult(result);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
