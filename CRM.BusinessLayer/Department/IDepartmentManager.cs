using CRM.BusinessLayer.InputModel;
using CRM.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BusinessLayer.Department
{
    public interface IDepartmentManager
    {

        Task<IEnumerable<Departments>> GetAllDepartments();
        Task<IEnumerable<Users>> GetAllUsers(string departmentName);
        Task AddDepartment(DepartmentIM input);
        //Task<int> getNumberOfEmployees(DepartmentIM input);
        Task<bool> setManager(DepartmentIM input);
        Task<Departments> getDepartment(string departmentName);
    }
}
