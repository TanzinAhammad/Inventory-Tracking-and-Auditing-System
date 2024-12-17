using Inventory.Migrations;
using Inventory.Models;
using Inventory.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IProduct productRepository;
        public ProductController(IProduct productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IActionResult> GetProductsList()
        {
            var data = await productRepository.GetProducts();
            return View(data);
        }

        

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    await productRepository.AddProduct(product);
                    if(product.ProductId==0)
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
            return RedirectToAction("GetProductsList");
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            Product product = new Product();
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    product = await productRepository.GetProductById(id);
                    TempData["PrevStock"] = product.Stock;
                    if (product==null)
                    {
                        return NotFound();
                    }

                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return View(product);
        }
        [HttpPost]

        public async Task<IActionResult>Edit(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    int prevStock = Convert.ToInt32(TempData["PrevStock"]);

                    AuditLogs auditlogs = new AuditLogs();
                    //auditlogs.AuditId = 1;
                    auditlogs.ProductId = product.ProductId;
                    auditlogs.ProductName = product.ProductName;
                    auditlogs.TimeStamp = DateTime.Now;

                    int quantity = (prevStock - product.Stock);
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

                    if (product.Stock >= 0)
                    {
                        status = await productRepository.UpdateRecord(product);
                    }
                    if(status)
                    {
                        if (quantity > 0)
                        {
                            await productRepository.UpdateAuditLogs(auditlogs);
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
            return RedirectToAction("GetProductsList");
        }

        public async Task<IActionResult> DeleteRecord(int id)
        {
            Product product = new Product();
            try
            {
                if(id==0)
                {
                    return BadRequest();
                }
                else
                {
                    product = await productRepository.GetProductById(id);

                    int prevStock = product.Stock;

                    AuditLogs auditlogs = new AuditLogs();
                    //auditlogs.AuditId = 1;
                    auditlogs.ProductId = product.ProductId;
                    auditlogs.ProductName = product.ProductName;
                    auditlogs.TimeStamp = DateTime.Now;
                    int quantity = product.Stock;
                    auditlogs.ChangeType = "Deduction";
                    auditlogs.Quantity = quantity;
                    auditlogs.UserName = "AdminUser"; 

                       
                    if (quantity > 0)
                    {
                        await productRepository.UpdateAuditLogs(auditlogs);
                    }



                    bool status = await productRepository.DeleteRecord(id);
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
            return RedirectToAction("GetProductsList");
        }

        public async Task<IActionResult> AuditLogs()
        {
            var dataAudit = await productRepository.Audits();
            return View(dataAudit);
        }

        public async Task<IActionResult> Filter()
        {
            return View();
        }

        public async Task<IActionResult> ProductCategory()
        {
            var data = await productRepository.GetProducts();
            return View(data);
        }

        public async Task<IActionResult> ProductPrice()
        {
            var data = await productRepository.GetProducts();
            return View(data);
        }

        public async Task<IActionResult> ProductStock()
        {
            var data = await productRepository.GetProducts();
            return View(data);
        }


        public async Task<IActionResult> ProductReport()
        {
            var products = await productRepository.GetProducts();

            // Total Stock Value
            decimal totalStockValue = products.Sum(u => u.Stock * u.Price);

            // Most Frequently Updated Products
            var auditLogs = await productRepository.Audits(); // Fetch audit logs
            var mostUpdatedProducts = auditLogs
                .GroupBy(a => a.ProductId) // Group by ProductId
                .Select(g => new
                {
                    ProductId = g.Key,
                    UpdatesCount = g.Count(),
                    ProductName = g.First().ProductName
                })
                .OrderByDescending(g => g.UpdatesCount)
                .Take(5) // Top 5 most updated products
                .ToList();

            // Pass data to the view using ViewBag or ViewModel
            ViewBag.TotalStockValue = totalStockValue;
            ViewBag.MostUpdatedProducts = mostUpdatedProducts;

            return View(products); // Return product list as well
        }

        




    }
}


