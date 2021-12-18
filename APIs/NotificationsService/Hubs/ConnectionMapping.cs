using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NotificationsService.Hubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<KeyValuePair<string, ClaimsPrincipal>>> _connections =
            new Dictionary<T, HashSet<KeyValuePair<string, ClaimsPrincipal>>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId, ClaimsPrincipal userClaims)
        {
            lock (_connections)
            {
                HashSet<KeyValuePair<string, ClaimsPrincipal>> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<KeyValuePair<string, ClaimsPrincipal>>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(new KeyValuePair<string, ClaimsPrincipal>(connectionId, userClaims));
                }
            }
        }

        public IEnumerable<KeyValuePair<string, ClaimsPrincipal>> GetConnections(T userIdentifier)
        {
            HashSet<KeyValuePair<string, ClaimsPrincipal>> connections;
            if (_connections.TryGetValue(userIdentifier, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<KeyValuePair<string, ClaimsPrincipal>>();
        }

        public Dictionary<T, HashSet<KeyValuePair<string, ClaimsPrincipal>>> GetConnections()
        {
            return _connections;
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<KeyValuePair<string, ClaimsPrincipal>> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.RemoveWhere(x => x.Key == connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
