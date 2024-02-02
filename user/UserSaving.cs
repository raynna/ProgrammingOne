
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgrammeringOne.user {
    internal class UserSaving {

        static string GetPath(string username) => "C:/Users/andre/Documents/GitHub/ProgrammingOne/user/users/" + username + ".json";


        public static void SaveUser(User user) {
            try {
                JsonSerializerOptions options = new JsonSerializerOptions {
                    WriteIndented = true,
                    Converters = { new ListStringConverter() }
                };
                string json = JsonSerializer.Serialize(user);
                string path = GetPath(user.Username);
                File.WriteAllText(path, json);
                Console.WriteLine("Saved user: " + user.Username + ", backPack" + user.BackPackItems.ToString());
            } catch (Exception error) {
                Console.WriteLine(error);
            }
        }

        public static User? LoadUser(string username) {
            try {
                JsonSerializerOptions options = new JsonSerializerOptions {
                    Converters = { new ListStringConverter() }
                };

                string path = GetPath(username);
                if (!File.Exists(path)) {
                    Console.WriteLine("User " + username + " not found in folder users.");
                    return null;
                }
                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json)) {
                    Console.WriteLine("Userfile: " + username + ".json is empty.");
                    return null;
                }
                User? user = JsonSerializer.Deserialize<User>(json);
                if (user == null) {
                    Console.WriteLine("Failed to load user: " + username);
                    return null;
                }
                Console.WriteLine("Loaded user: " + user.Username + ", backPack" + user.BackPackItems.ToString());
                return user;

            } catch (Exception error) {
                    Console.WriteLine(error.Message);
                    return null;
                } 
            }

        internal class ListStringConverter : JsonConverter<List<string>> {
            public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                if (reader.TokenType == JsonTokenType.StartArray) {
                    List<string> result = new List<string>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray) {
                        if (reader.TokenType == JsonTokenType.String) {
                            result.Add(reader.GetString() ?? string.Empty);
                        }
                    }
                    return result;
                }
                return [];
            }

            public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options) {
                writer.WriteStartArray();
                foreach (string item in value) {
                    writer.WriteStringValue(item);
                }
                writer.WriteEndArray();
            }
        }
    }
}
