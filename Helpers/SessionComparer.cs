using System.Collections;
using System.Collections.Generic;
using ProjectC_v2.Models;

namespace ProjectC_v2.Helpers
{
    public sealed class SessionComparer : IEqualityComparer<Session>
    {
        public bool Equals(Session x, Session y)
        {
            return x.SessionId == y.SessionId;
        }

        public int GetHashCode(Session obj)
        {
            return obj.GetHashCode();
        }
    }
}