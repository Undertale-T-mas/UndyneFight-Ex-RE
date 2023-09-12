using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UndyneFight_Ex.Server
{
    public static class UserLibrary
    {
        private class UserBufferData
        {
            public Dictionary<string, long> UUIDList { get; set; } = new();
            public Dictionary<long, string> PlayerNames { get; set; } = new();

            public long UserCount => PlayerNames.Count;

            public void Save()
            {
                FileStream stream = new("Data/User/\\data", FileMode.OpenOrCreate, FileAccess.Write);
                stream.SetLength(0);
                stream.Write(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(this)));
                stream.Flush();
                stream.Dispose();
            }
        }
        private static UserBufferData userData;

        static UserLibrary()
        {
            if (!File.Exists("Data/User/\\data"))
            {
                userData = new();
                return;
            }
            FileStream stream = new("Data/User/\\data", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader streamReader = new(stream, Encoding.ASCII);
            userData = JsonSerializer.Deserialize<UserBufferData>(streamReader.ReadToEnd()) ?? throw new FileLoadException();
        }

        private static string SHA512Encode(string source)
        {
            string result = "";
            byte[] buffer = Encoding.ASCII.GetBytes(source); 
             
            SHA512 sha512 = SHA512.Create();
            byte[] h5 = sha512.ComputeHash(buffer);

            result = BitConverter.ToString(h5).Replace("-", string.Empty);

            return result.ToLower();
        }

        private static HashSet<User> onlineUsers = new();
        private static void InnerUpdate(User user)
        {
            if(!userData.UUIDList.ContainsKey(user.Name)) 
                userData.UUIDList.Add(user.Name, user.UUID);
            else if (userData.UUIDList[user.Name] != user.UUID) userData.UUIDList[user.Name] = user.UUID;
            
            if (!userData.PlayerNames.ContainsKey(user.UUID))
                userData.PlayerNames.Add(user.UUID, user.Name);
            else if (userData.PlayerNames[user.UUID] != user.Name) userData.PlayerNames[user.UUID] = user.Name;
        }

        internal static void Refresh(User user)
        {
            if(!onlineUsers.Contains(user)) onlineUsers.Add(user);
        }

        public static long UUIDOf(string name) => userData.UUIDList[name];
        public static Tuple<string, User?> Auth(string name, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || name.Contains('/') || name.Contains('\\')) return new("F name format incorrect", null);
                if (!File.Exists("Data/User/" + name)) return new("F user not exist", null);
                FileStream stream = new("Data/User/" + name, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new(stream);
                string result = streamReader.ReadToEnd();
                User? user = JsonSerializer.Deserialize<User>(result);
                if (user == null) return new("E deserialize failure", null);
                string hash = SHA512Encode(password);

                streamReader.Close();
                stream.Close();

                if (hash == user.PasswordHash)
                {
                    InnerUpdate(user);
                    return new("S success login", user);
                }
                else return new("F wrong password", null);
            }
            catch(Exception ex) 
            {
                UFConsole.WriteLine(ex.ToString());
                return new("E an exception was thrown", null);
            }
        }
        private static bool _onRegister = false;
        public static Tuple<string, User?> Register(string name, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || name.Contains('/') || name.Contains('\\')) return new("F name format incorrect", null);
                if (_onRegister) return new("F the server is busy, try again later", null);
                if (File.Exists("Data/User/" + name)) return new("F the name already exists", null);
                _onRegister = true;

                User user = new();
                user.Name = name;
                user.PasswordHash = SHA512Encode(password);
                user.UUID = userData.UserCount + 1;

                string result = JsonSerializer.Serialize(user);
                byte[] bytes = Encoding.ASCII.GetBytes(result);

                FileStream stream = new("Data/User/" + name, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();

                _onRegister = false;
                InnerUpdate(user);
                return new("S register complete", user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new("E an exception was thrown", null);
            }
        }

        internal static void UserExit(User bindUser)
        {
            onlineUsers.Remove(bindUser);
        }

        internal static string GenerateRSA(Client client)
        { 
            string publicKey;
            using (var rsa = new RSACryptoServiceProvider())
            { 
                try
                {
                    // 获取私钥和公钥。
                    publicKey = rsa.ToXmlString(false);
                    var privateKey = rsa.ToXmlString(true);
                    client.RSABuffer = privateKey; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception was thrown: " + ex.Message);
                    return "E an exception was thrown";
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }  
            return "S " + publicKey;
        }

        internal static void SaveAll()
        {
            foreach (var user in onlineUsers)
            {
                UFConsole.WriteLine("Saving the data of " + user.Name);
                user.Save();
            }
            userData.Save();
        }

        internal static string NameOf(long uuid)
        {
            return userData.PlayerNames[uuid];
        }

        internal static string? Check(long uuid)
        {
            if (!userData.PlayerNames.ContainsKey(uuid)) return null;
            try
            {
                FileStream stream = new("Data/User/" + userData.PlayerNames[uuid], FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new(stream);
                string result = streamReader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                UFConsole.WriteLine($"An exception occured when checking uuid {uuid}: " + ex.Message);
                return null;
            }
        }
    }
}