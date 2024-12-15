using CRUD_Using_Repository.Models;
using CRUD_Using_Repository.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Using_Repository.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser userRepository;
        public UserController(IUser userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> GetUsersList()
        {
            var data = await userRepository.GetUsers();
            return View(data);
        }

        public async Task<IActionResult> AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                else
                {
                    await userRepository.AddUser(user);
                    if(user.UserId==0)
                    {
                        TempData["userError"] = "Record not saved!";
                    }
                    else
                    {
                        TempData["userSuccess"] = "Record successfully Saved";
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
            return RedirectToAction("GetUsersList");
        }

    }
}
