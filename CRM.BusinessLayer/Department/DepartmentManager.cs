using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRM.BusinessLayer.InputModel;
using CRM.DataAccess;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CRM.BusinessLayer.Department
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly CRMContext _context = null;
        public DepartmentManager(IOptions<Settings> settings)
        {
            _context = new CRMContext(settings);
        }
        //Finding all Departments inside the DB
        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            try
            {
                return await _context.Department.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get All Users inside a Department
        public async Task<IEnumerable<Users>> GetAllUsers(string departmentName)
        {
            try
            {
                var department = await _context.Department.Find(dep => dep.departmentName == departmentName).FirstOrDefaultAsync();
                var users = await _context.Users.Find(user => user.departmentId == department.internalId).ToListAsync();
                return users;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddDepartment(DepartmentIM input)
        {
            try
            {
                var manager = await _context.Users.Find(user => user.userName == input.managerUserName).FirstOrDefaultAsync();
                Departments dep = new Departments {
                    departmentName = input.departmentName,
                    managerName = manager.firstName + " " + manager.lastName,
                    managerId = manager.internalId,
                    numberofEmployees = 0
                };
                await _context.Department.InsertOneAsync(dep);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> getNumberOfEmployees(DepartmentIM input)
        {
            try
            {
                var department = await _context.Department.Find(dep => dep.departmentName == input.departmentName).FirstOrDefaultAsync();
                var employees = await _context.Users.Find(user => user.departmentId == department.internalId).ToListAsync();
                //update the number of Employees in the DB
                department.numberofEmployees = employees.Count;
                await _context.Department.ReplaceOneAsync( dep => dep.internalId.Equals(department.internalId), department, new UpdateOptions { IsUpsert = true });
                return employees.Count;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> setManager(DepartmentIM input)
        {
            try
            {
                var department = await _context.Department.Find(dep => dep.departmentName == input.departmentName).FirstOrDefaultAsync();
                if (department != null)
                {
                    var manager = await _context.Users.Find(user => user.userName == input.managerUserName).FirstOrDefaultAsync();
                    department.managerName = manager.firstName + " " + manager.lastName;
                    department.managerId = manager.internalId;
                    ReplaceOneResult actionResult = await _context.Department.ReplaceOneAsync(dep => dep.internalId.Equals(department.internalId), department, new UpdateOptions { IsUpsert = true });
                    return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Departments> getDepartment(string departmentName)
        {
            try
            {
                var department = await _context.Department.Find(dep => dep.departmentName == departmentName).FirstOrDefaultAsync();
                return department;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
