using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opus_Fahim.Models;
using Opus_Fahim.Repositories.interfaces;
using Opus_Fahim.ViewModels;

namespace Opus_Fahim.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<Employee> repo;



        public EmployeesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<Employee>();
        }
        public async Task<IActionResult> Index()
        {
            var data = await this.repo.GetAllAsync(x => x.Include(y => y.Departments));
            return View(data);
        }
        public IActionResult Create()
        {
            EmployeeInputModel db = new EmployeeInputModel();
            db.Departments.Add(new Department { });
            return View(db);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeInputModel model, string act = "")
        {
            if (act == "add")
            {
                model.Departments.Add(new Department { });
            }
            if (act.StartsWith("remove"))
            {
                var index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Departments.RemoveAt(index);

                foreach (var v in ModelState.Values)
                {
                    v.RawValue = null;
                    v.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    Employee e = new Employee
                    {
                        Name = model.Name,

                        Gender = model.Gender,

                        Salary = model.Salary,
                        DoB = model.DoB,
                        IsActive = model.IsActive
                    };
                    foreach (var p in model.Departments)
                    {
                        e.Departments.Add(p);
                        //{
                        //    DepartmentName= p.DepartmentName,
                        //    DepartmentMember =p.DepartmentMember
                        //});
                    }
                    await this.repo.InsertAsync(e);
                    bool saved = await this.unitOfWork.SaveAsync();
                    if (saved)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to Saved Data");
                    }
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var e = await repo.GetByIdAsync(x => x.EmployeeId == id, x => x.Include(y => y.Departments));

            if (e == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditModel
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Gender = e.Gender,
                Salary = e.Salary,
                DoB = e.DoB,
                IsActive = e.IsActive
            };
            foreach (var p in e.Departments)
            {
                model.Departments.Add(new Department
                {
                    DepartmentId = p.DepartmentId,
                    DepartmentName = p.DepartmentName,
                    DepartmentMember = p.DepartmentMember
                });

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditModel model, string act = "")
        {
            if (act == "add")
            {
                model.Departments.Add(new Department());
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Departments.RemoveAt(index);

                foreach (var v in ModelState.Values)
                {
                    v.RawValue = null;
                    v.Errors.Clear();
                }
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var EmployeeRepo = unitOfWork.GetRepo<Employee>();
                    var a = await EmployeeRepo.GetByIdAsync(x => x.EmployeeId == model.EmployeeId, x => x.Include(y => y.Departments));
                    if (a == null)
                    {
                        return NotFound();
                    }
                    a.Name = model.Name;
                    a.Gender = model.Gender;
                    a.Salary = model.Salary;
                    a.DoB = model.DoB;
                    a.IsActive = model.IsActive;

                    var DepartmentRepo = unitOfWork.GetRepo<Department>();
                    a.Departments.Clear();

                    await DepartmentRepo.DeleteAsync(q => q.EmployeeId == model.EmployeeId);
                    //
                    //db.Departments.RemoveRange(a.Departments.ToList());

                    foreach (var x in model.Departments)
                    {
                        a.Departments.Add(new Department { DepartmentName = x.DepartmentName, DepartmentMember = x.DepartmentMember });
                    }
                    await EmployeeRepo.UpdateAsync(a);
                    bool saved = await unitOfWork.SaveAsync();

                    if (saved)
                    {
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to save data");

                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var EmployeeRepo = unitOfWork.GetRepo<Employee>();

            await EmployeeRepo.DeleteAsync(x => x.EmployeeId == id);
            bool saved = await unitOfWork.SaveAsync();
            //return RedirectToAction("Index");
            return Json(new { success = true, id });
        }

    }
}

