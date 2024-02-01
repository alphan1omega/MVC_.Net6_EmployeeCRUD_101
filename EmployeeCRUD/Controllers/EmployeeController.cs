using AutoMapper;
using EmployeeCRUD.AppData;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeCrud101Context _context;
        private readonly IMapper _mapper;
        public EmployeeController(EmployeeCrud101Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var objCatlist = _context.Employees;
            return View(_mapper.Map<IEnumerable<EmpViewModel>>(objCatlist));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmpViewModel empobj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cdate = DateTime.Now;
                    empobj.RecordCreatedOn = cdate;
                    empobj.Id = Guid.NewGuid();

                    var emp1 = _mapper.Map<Employee>(empobj);

                    _context.Employees.Add(emp1);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Record Added Successfully !";
                    return RedirectToAction("Index");
                }

                return View(empobj);
            }
            catch(Exception ex) 
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EmpViewModel>(empfromdb));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmpViewModel empobj)
        {
            if (ModelState.IsValid)
            {
                var emp = _mapper.Map<Employee>(empobj);
                _context.Employees.Update(emp);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);
         
            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EmpViewModel>(empfromdb));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(Guid? id)
        {
            var deleterecord = _context.Employees.Find(id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index");
        }


    }
}
