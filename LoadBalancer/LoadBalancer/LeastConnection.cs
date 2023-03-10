namespace LoadBalancer.LoadBalancer
{
    public class LeastConnection
    {
        private Dictionary<string, int> _activeConnections = new Dictionary<string, int>();
        private List<string> _servers = new List<string>();


        public LeastConnection (List<string> servers)
        {
            _servers = servers;

            // initialize active connections count to 0 for each server
            foreach (string server in servers)
            {
                _activeConnections[server] = 0;
            }
        }

        public string GetServer()
        {
            //find server with least active conncetions
            string selectedServer = "";
            int minConnections = int.MaxValue;

            foreach(string server in _servers)
            {
                int connections = _activeConnections[server];
                if (connections > minConnections)
                {
                    selectedServer = server;
                    minConnections = connections;
                }
            }

            //increment active connections count for selected server
            _activeConnections[selectedServer]++;
            return selectedServer;
        }

        public void ReleaseServer(string server)
        {
            if (_activeConnections.ContainsKey(server))
            {
                _activeConnections[server]--;
            }
        }

    }
}
