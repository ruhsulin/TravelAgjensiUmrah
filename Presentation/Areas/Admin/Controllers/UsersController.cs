using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models.UserViewModels;
using System.Data;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Entities;
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
        private readonly ISelectListService _selectListService;


        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IRolesRepository rolesRepository, ILogger<UsersController> logger, ISelectListService selectListService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.rolesRepository = rolesRepository;
            _logger = logger;
            _selectListService = selectListService;
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
            model.Roles = new SelectList(_selectListService.GetRolesKeysValues(), "SKey", "Value", model.RoleId);
            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            AspNetUser? user = _userRepository.GetByStringId(id);
            if (user != null)
            {
                var model = new UserViewModel()
                {
                    Id = id,
                    Password = "",
                    ConfirmPassword = "",
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed!,
                    Name = user.Name!,
                    PhoneNumber = user.PhoneNumber!,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed!,
                    // RoleId = rolesRepository.GetByUserId(user.Id)!.Id,
                    Surname = user.Surname!,
                };
                model.Roles = new SelectList(_selectListService.GetRolesKeysValues(), "SKey", "Value", model.RoleId);

                return View("Add", model);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> AddAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Id)) // If there's an ID, it's an edit operation
                {
                    var existingUser = await _userManager.FindByIdAsync(model.Id);

                    if (existingUser == null)
                    {
                        return NotFound(); // Handle if user is not found
                    }

                    existingUser.Name = model.Name;
                    existingUser.Surname = model.Surname;
                    existingUser.Email = model.Email;
                    existingUser.EmailConfirmed = model.EmailConfirmed;
                    existingUser.PhoneNumber = model.PhoneNumber;

                    // Update other properties as needed

                    var result = await _userManager.UpdateAsync(existingUser);

                    if (result.Succeeded)
                    {
                        // Fetch existing roles of the user
                        var userRoles = await _userManager.GetRolesAsync(existingUser);

                        // Remove existing roles
                        var removeRolesResult = await _userManager.RemoveFromRolesAsync(existingUser, userRoles);

                        if (!removeRolesResult.Succeeded)
                        {
                            foreach (var error in removeRolesResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View("Add", model);
                        }

                        // Assign the new role
                        var selectedRole = rolesRepository.GetByStringId(model.RoleId);

                        if (selectedRole != null)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(existingUser, selectedRole.Name);

                            if (roleResult.Succeeded)
                            {
                                _logger.LogInformation($"User with ID {model.Id} updated with new role: {selectedRole.Name}");
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                _logger.LogError($"Failed to assign role {selectedRole.Name} to the user.");
                                ModelState.AddModelError(string.Empty, "Failed to assign user role. Please try again.");
                                return View("Add", model);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Selected role not found.");
                            return View("Add", model);
                        }
                    }
                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(existingUser);

                        _logger.LogInformation($"User with ID {model.Id} updated.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("Indexs", model);
                    }
                }
                else // It's a new user being added
                {
                    var user = new ApplicationUser
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        UserName = model.Email,
                        Email = model.Email,
                        EmailConfirmed = model.EmailConfirmed,
                        PhoneNumber = model.PhoneNumber,
                        PhoneNumberConfirmed = model.PhoneNumberConfirmed
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var selectedRole = rolesRepository.GetByStringId(model.RoleId);

                        if (selectedRole != null)
                        {
                            var roleResult = await _userManager.AddToRoleAsync(user, selectedRole.Name);

                            if (roleResult.Succeeded)
                            {
                                _logger.LogInformation($"User created with role {selectedRole.Name}");
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                _logger.LogError($"Failed to assign role {selectedRole.Name} to the user.");
                                await _userManager.DeleteAsync(user);
                                ModelState.AddModelError(string.Empty, "Failed to assign user role. Please try again.");
                                return View("Add", model);
                            }
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View("Add", model);
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("Add", model);
                    }
                }
            }

            return View("Add", model);
        }



        [HttpGet]
        public IActionResult GetUsersJson()
        {
            var users = _userRepository.GetAll();

            var result = users.Select(async x =>
            {
                var user = await _userManager.FindByIdAsync(x.Id);
                var roles = user != null ? string.Join(", ", await _userManager.GetRolesAsync(user)) : string.Empty;

                return new
                {
                    id = user?.Id,
                    name = user?.Name,
                    surname = user?.Surname,
                    roles,
                    email = user?.Email,
                    emailConfirmed = user?.EmailConfirmed
                };
            }).Select(task => task.Result); // Ensure async execution completes synchronously

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
