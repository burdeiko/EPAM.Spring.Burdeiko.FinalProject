using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core;
using System.Web.Mvc;

namespace SocialNetwork.Mvc.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private readonly IRoleService roleService = (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
        private readonly IUserService userService = (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        public override string ApplicationName
        {
            get; set;
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return false;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return false;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return 5;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return 0;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return 5;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return 1;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Hashed;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return false;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return true;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                User user = userService.GetUserByEMail(username);
                user.PasswordHash = Crypto.HashPassword(newPassword);
                userService.UpdateEntity(user);
                return true;
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string email, string password, string firstName, string lastName)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }

            var user = new User
            {
                EMail = email,
                PasswordHash = Crypto.HashPassword(password),
                //Registration = DateTime.Now
            };
            var role = roleService.FindByName("User");
            if (role != null)
            {
                user.RoleId = role.id;
            }
            user.Person = new Person()
            {
                FirstName = firstName,
                LastName = lastName
            };
            userService.CreateEntity(user);
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = userService.GetUserByEMail(username);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.EMail,
                null, null, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = userService.GetUserByEMail(username);
            if (user != null && Crypto.VerifyHashedPassword(user.PasswordHash, password))
            {
                return true;
            }
            return false;
        }
    }
}