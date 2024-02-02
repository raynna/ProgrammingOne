
using Programmering_1.course.courses;
using ProgrammeringOne.course;
using ProgrammeringOne.user;

namespace ProgrammeringOne {


    internal class Program {

        public static string username = "andreas";

        public static void Main(string[] args) {
            User? user = UserSaving.LoadUser(username);
            if (user == null) {
                user = new User(username);
                UserSaving.SaveUser(user);
            }
            if (user == null)
                return;
            Course course = new Menu(user);
            course.Start();
        }
    }
}
