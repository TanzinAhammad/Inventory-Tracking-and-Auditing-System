﻿using CRUD_Using_Repository.Models;
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
                    if(user.SKU==0)
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
                    TempData["PrevStock"] = user.Stock;
                    if (user==null)
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
                    int prevStock = Convert.ToInt32(TempData["PrevStock"]);

                    AuditLogs auditlogs = new AuditLogs();
                    //auditlogs.AuditId = 1;
                    auditlogs.SKU = user.SKU;
                    auditlogs.Product_Name = user.Product_Name;
                    auditlogs.TimeStamp = DateTime.Now;

                    int quantity = (prevStock - user.Stock);
                    string type;
                    if(quantity>0)
                    {
                        type = "Deduction";
                    }
                    else
                    {
                        type = "Addition";
                        quantity = -quantity;
                    }

                    auditlogs.ChangeType = type;
                    auditlogs.Quantity = quantity;
                    auditlogs.UserName = "AdminUser";

                    bool status = false;
                    if (user.Stock >= 0)
                    {
                        status = await userRepository.UpdateRecord(user);
                    }
                    if(status)
                    {
                        if (quantity > 0)
                        {
                            await userRepository.UpdateAuditLogs(auditlogs);
                        }
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

        public async Task<IActionResult> AuditLogs()
        {
            var dataAudit = await userRepository.Audits();
            return View(dataAudit);
        }

        public async Task<IActionResult> Filter()
        {
            return View();
        }

        public async Task<IActionResult> ProductCategory()
        {
            var data = await userRepository.GetUsers();
            return View(data);
        }

        public async Task<IActionResult> ProductPrice()
        {
            var data = await userRepository.GetUsers();
            return View(data);
        }

        public async Task<IActionResult> ProductStock()
        {
            var data = await userRepository.GetUsers();
            return View(data);
        }


        public async Task<IActionResult> ProductReport()
        {
            var users = await userRepository.GetUsers();

            // Total Stock Value
            decimal totalStockValue = users.Sum(u => u.Stock * u.Price);

            // Most Frequently Updated Products
            var auditLogs = await userRepository.Audits(); // Fetch audit logs
            var mostUpdatedProducts = auditLogs
                .GroupBy(a => a.SKU) // Group by SKU
                .Select(g => new
                {
                    SKU = g.Key,
                    UpdatesCount = g.Count(),
                    ProductName = g.First().Product_Name
                })
                .OrderByDescending(g => g.UpdatesCount)
                .Take(5) // Top 5 most updated products
                .ToList();

            // Pass data to the view using ViewBag or ViewModel
            ViewBag.TotalStockValue = totalStockValue;
            ViewBag.MostUpdatedProducts = mostUpdatedProducts;

            return View(users); // Return product list as well
        }

        




    }
}


