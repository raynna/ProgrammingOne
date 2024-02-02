using ProgrammeringOne.course;
using ProgrammeringOne.user;

namespace Programmering_1.course.courses {


    internal class Menu(User user) : Course(user) {

        public override void Start() {
            Clear();
            WriteLine("Vilken övning vill du starta?");
            NewLine();
            Option[] options = {
                new(1, "Ryggsäck", new BackPack(user)),
                new(2, "Maze", new BackPack(user)),
                new(9, "Avsluta", () => Stop())
            };
            Option.ShowOptions(options);
            NewLine();
            Write("Välj: ");
            char keyPress = ReadKey().KeyChar;
            if (!char.IsDigit(keyPress)) {
                Restart();
                return;
            }
            Course? selectedCourse = null;
            foreach (Option option in options) {
                if (option.Id.ToString() == keyPress.ToString()) {
                    if (option.Action != null) {
                        option.Action();
                        break;
                    }
                    selectedCourse = option.Course;
                    break;
                }
            }
            if (selectedCourse == null) {
                Restart();
                return;
            }
            selectedCourse.Start();
        }

        public override void Stop() {
            base.Stop();
            return;
        }
    }
}
