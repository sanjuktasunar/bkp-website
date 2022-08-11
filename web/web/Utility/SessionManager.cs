using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Entity.Dto;

namespace web.Utility
{
    public class SessionManager 
    {
        private readonly Security security=new Security();
        public void SetToSessionCookie(UsersDto dto)
        {
            HttpContext.Current.Session[SessionCodes.SITE_USER_LOGIN_ID] = security.EncryptText(dto.UserId.ToString());
            HttpContext.Current.Session[SessionCodes.SITE_FULL_NAME] = security.EncryptText(dto.FullName?.ToString());
            HttpContext.Current.Session[SessionCodes.SITE_ROLENAME] = security.EncryptText(dto.RoleName?.ToString());
        }

        public void SetToSessionCookie(MenuAccessPermissionDto dto)
        {
            HttpContext.Current.Session[SessionCodes.SITE_USER_LOGIN_ID] = security.EncryptText(dto.UserId.ToString());
            HttpContext.Current.Session[SessionCodes.SITE_FULL_NAME] = security.EncryptText(dto.FullName.ToString());
            HttpContext.Current.Session[SessionCodes.SITE_ROLENAME] = security.EncryptText(dto.RoleName.ToString());
        }

        public UsersDto ReadFromSessionCookie()
        {
            var usersDto = new UsersDto();
            if (HttpContext.Current.Session.Count == 0)
                return null;

            string session_user_id = HttpContext.Current.Session[SessionCodes.SITE_USER_LOGIN_ID].ToString();
            string session_full_name = HttpContext.Current.Session[SessionCodes.SITE_FULL_NAME].ToString();
            string session_role_name = HttpContext.Current.Session[SessionCodes.SITE_ROLENAME].ToString();

            if (string.IsNullOrEmpty(session_user_id))
                return null;
            usersDto.UserId = Convert.ToInt32(security.DecryptText(session_user_id));
            if (!string.IsNullOrEmpty(session_full_name))
                usersDto.FullName = security.DecryptText(session_full_name);

            if (!string.IsNullOrEmpty(session_role_name))
                usersDto.RoleName = security.DecryptText(session_role_name);

            return usersDto;
        }

        public void ClearAllSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }

        public void RemoveSessionByKey(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }
    }
}