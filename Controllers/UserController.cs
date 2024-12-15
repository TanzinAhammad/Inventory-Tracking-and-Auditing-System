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
                        TempData["userSuccess"] = "Record successfully Saved!";
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
            return RedirectToAction("GetUsersList");
        }

        public async Task<IActionResult> Edit(int id)
        {
            User user = new User();
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    user = await userRepository.GetUserById(id);
                    if(user==null)
                    {
                        return NotFound();
                    }

                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return View(user);
        }
        [HttpPost]

        public async Task<IActionResult>Edit(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                else
                {
                    bool status=await userRepository.UpdateRecord(user);
                    if(status)
                    {
                        TempData["userSuccess"] = "Your Record has been successfully updated!";
                    }
                    else 
                    {
                        TempData["userError"] = "Record has not been updated!";
                    }
                }
            }
            catch(Exception) 
            {

                throw;
            }
            return RedirectToAction("GetUsersList");
        }

        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                if(id==0)
                {
                    return BadRequest();
                }
                else
                {
                    bool status = await userRepository.DeleteRecord(id);
                    if(status)
                    {
                        TempData["userSuccess"] = "Your Record has been Successfully Deleted!";
                    }
                    else
                    {
                        TempData["userError"] = "Record not Deleted!";
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
