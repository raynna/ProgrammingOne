

using System.Text.Json.Serialization;

namespace ProgrammeringOne.user {
    [Serializable]
    internal class User(string username) {


        public string Username { get; set; } = username;

        [JsonPropertyName("backPackItems")]
        public List<string> BackPackItems { get; set; } = [];

        public void AddItem(string item) {
            BackPackItems.Add(item);
        }

        public void ClearItems() {
            BackPackItems.Clear();
        }

        public void SaveUser() {
            UserSaving.SaveUser(this);
        }

        public User? LoadUser(string username) {
            return UserSaving.LoadUser(username);
        }
    }
}
