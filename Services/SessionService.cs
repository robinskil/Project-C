using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ProjectC_v2.Helpers;
using ProjectC_v2.Models;

namespace ProjectC_v2.Services
{
    public static class SessionService
    {
        private static readonly HashSet<Session> OpenSessions = new HashSet<Session>(new SessionComparer());

        public static bool Login(User user, ConnectionInfo connectionInfo , out Guid sessionId)
        {
            var session = new Session(user , connectionInfo);
            sessionId = session.SessionId;
            //if there is no session then add one
            if (!OpenSessions.Any(u => u.User.UserId == user.UserId))
            {
                OpenSessions.Add(session);
            }
            //else kick out the open session first and then create a new one.
            else
            {
                OpenSessions.RemoveWhere(s => s.User.UserId == user.UserId);
                OpenSessions.Add(session);
            }
            return true;
        }
        public static bool Logout(Guid sessionId)
        {
            OpenSessions.RemoveWhere(s => s.SessionId == sessionId);
            return true;
        }
        public static User GetUserFromSession(Guid sessionId){
            return OpenSessions.FirstOrDefault(s => s.SessionId == sessionId).User;
        }
        /// <summary>
        /// This method should be called using a timer to allow updating session towards the database
        /// Exceptions: ??????TO BE DEFINED , could be SQL EXCEPTION
        /// BE AWARE : USE ANONYMOUS OBJECTS TO HANDLE STACK OVERFLOW EXCEPTION WHEN ROWS > 16MB STACK FRAME
        /// </summary>
        public static void InProcUpdateSession(){
            try{

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return;
        }
    }
}