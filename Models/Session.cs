using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ProjectC_v2.Models
{
    public class Session{
        public Guid SessionId{get;}
        public User User{get;}
        public ConnectionInfo Connection{get;}
        public UserState UserState{get;set;}
        public Session(User user , ConnectionInfo connectionInfo){
            this.SessionId = Guid.NewGuid();
            this.User = user;
            this.Connection = connectionInfo;
            this.UserState = UserState.Online;
        }
    }
}