using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //Implement the method from the interface, and here I can add logic to whichever methods I want 
        //to add logic to
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
           
            return await _employeeRepository.GetEmployeesAsync();
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database

       /* Not allowing total salaries added to exceed $1,000,000
Not allowing a single salary to exceed $100,000
Not allowing more than 5 Employees to be added with the Position "Engineer"
Not allowing more than 5 Employees to be added with the Position "Manager"
Not allowing more than 1 Employee to be added with the Position "HR"*/
        public async Task AddEmployeeAsync(Employee employee)
        {
            
            var emps = await _employeeRepository.GetEmployeesAsync();

            
            var totalSalary = emps.Sum(e => e.Salary);


            //Writing logic for salarys & ids
            if (employee.Salary <= 100 && (totalSalary + employee.Salary < 1000000) && employee.Salary < 100000)
            {
                var engCount = emps.Count(e => e.Position == "Engineer");
                var manCount = emps.Count(e => e.Position == "Manager");
                var hrCount = emps.Count(e => e.Position == "HR");

                if (employee.Position == "Engineer" && engCount <= 5) {

                    await _employeeRepository.AddEmployeeAsync(employee);

                } else if (employee.Position == "Manager" && engCount <= 5) {

                    await _employeeRepository.AddEmployeeAsync(employee);

                } else if (employee.Position == "HR" && engCount <= 1) {

                    await _employeeRepository.AddEmployeeAsync(employee);

                }

               
            }
            
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        //going to the DAL, and getting the employee from the employees DbSet
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task GetDetailsAsync(Employee employee)
        {
            await _employeeRepository.GetDetailsAsync(employee);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var emps = await _employeeRepository.GetEmployeesAsync();


            var totalSalary = emps.Sum(e => e.Salary);


            //Writing logic for salarys & ids
            if (employee.Salary <= 100 && (totalSalary + employee.Salary < 1000000) && employee.Salary < 100000)
            {
                var engCount = emps.Count(e => e.Position == "Engineer");
                var manCount = emps.Count(e => e.Position == "Manager");
                var hrCount = emps.Count(e => e.Position == "HR");

                if (employee.Position == "Engineer" && engCount <= 5)
                {

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                }
                else if (employee.Position == "Manager" && engCount <= 5)
                {

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                }
                else if (employee.Position == "HR" && engCount <= 1)
                {

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                }


            }
        }

    }
}
