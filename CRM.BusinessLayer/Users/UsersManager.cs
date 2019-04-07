using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRM.BusinessLayer.OutputModel;
using CRM.DataAccess;
using CRM.DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CRM.BusinessLayer
{
    public class UsersManager: IUsersManager
    {
        private readonly CRMContext _context = null;
        public UsersManager(IOptions<Settings> settings)
        {
            _context = new CRMContext(settings);
        }
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Users> GetUser(string userName)
        {
            try
            {
                return await _context.Users.Find(user => user.userName == userName || user.email == userName).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
        //public async Task<Users> GetUser(string email, string userName)
        //{
        //    try
        //    {

        //        return await _context.Users.Find(User => User.email == email && User.userName == userName).FirstOrDefaultAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<CredentialExistOM> AddUser(Users item)
        {
            try
            {
                CredentialExistOM check = new CredentialExistOM();
                check = checkUserAvail(item, check).Result;
                check = checkEmailAvail(item, check).Result;
                if(!check.emailExist && !check.userNameExist)
                    await _context.Users.InsertOneAsync(item);
                return check;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //Checks if the userName already exists.
        private async Task<CredentialExistOM> checkUserAvail(Users item, CredentialExistOM check)
        {
            var result = await _context.Users.Find(u => u.userName == item.userName).ToListAsync();
            if (result.Count > 0)
            {
                check.userNameExist = true;
                check.message += "* UserName Already exist";
            }
            return check;
        }
        // Checks the email already exists.
        private async Task<CredentialExistOM> checkEmailAvail(Users item, CredentialExistOM check)
        {
            var result = await _context.Users.Find(u => u.email == item.email).ToListAsync();
            if(result.Count > 0)
            {
                check.emailExist = true;
                check.message += "* Email Already exist";
            }
            return check;
        }
        public async Task<bool> RemoveUser(string userName)
        {
            try
            {
                DeleteResult actionResult = await _context.Users.DeleteOneAsync(Builders<Users>.Filter.Eq("userName", userName));
                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateUser(string userName, Users item)
        {
         
            try
            {
                ReplaceOneResult actionResult
                = await _context.Users
                                .ReplaceOneAsync(n => n.userName.Equals(userName)
                                        , item
                                        , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> RemoveAllUsers()
        {
            try
            {
                DeleteResult actionResult
                = await _context.Users.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
