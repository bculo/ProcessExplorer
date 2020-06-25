using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.SignalR
{
    public class ConnectionMapper<T> : IConnectionMapper<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections;

        public ConnectionMapper()
        {
            _connections = new Dictionary<T, HashSet<string>>();
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections) //lock dictionary
            {
                HashSet<string> connections;

                //if key doesnt exists in dictionary create new hashset for that key
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                //add new connection to hashset
                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            //get all conections for specified key
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            //return empty list
            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                //check if hashset exists for given key
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    //remove connection for given key
                    connections.Remove(connectionId);

                    //if hashset empty remove key from dictionary
                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}
